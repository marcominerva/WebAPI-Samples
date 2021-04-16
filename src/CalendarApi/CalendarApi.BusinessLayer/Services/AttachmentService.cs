using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CalendarApi.BusinessLayer.Extensions;
using CalendarApi.BusinessLayer.Providers;
using CalendarApi.DataAccessLayer;
using CalendarApi.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MimeMapping;
using Entities = CalendarApi.DataAccessLayer.Entities;

namespace CalendarApi.BusinessLayer.Services
{
    public class AttachmentService : IAttachmentService
    {
        private readonly CalendarDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IStorageProvider storageProvider;

        public AttachmentService(CalendarDbContext dbContext, IMapper mapper, IStorageProvider storageProvider)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.storageProvider = storageProvider;
        }

        public async Task<IEnumerable<Attachment>> GetListAsync(Guid eventId)
        {
            var attachments = await dbContext.Attachments.AsNoTracking()
                .Where(a => a.EventId == eventId)
                .ProjectTo<Attachment>(mapper.ConfigurationProvider).ToListAsync();

            return attachments;
        }

        public async Task SaveAsync(Guid eventId, IFormFile attachment)
        {
            var @event = await dbContext.Events.Include(e => e.Attachments).FirstOrDefaultAsync(e => e.Id == eventId);
            if (@event != null)
            {
                var content = await attachment.GetContentAsByteArrayAsync();
                var path = GetPath(@event, attachment);

                await storageProvider.SaveAsync(path, content);

                @event.Attachments.Add(new Entities.Attachment { Path = path, Length = content.Length });
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<(byte[] Content, string ContentType)?> GetAsync(Guid attachmentId)
        {
            var attachment = await dbContext.FindAsync<Entities.Attachment>(attachmentId);
            if (attachment != null)
            {
                var content = await storageProvider.ReadAsync(attachment.Path);
                return (content, MimeUtility.GetMimeMapping(attachment.Path));
            }

            return null;
        }

        public async Task DeleteAsync(Guid attachmentId)
        {
            var attachment = await dbContext.FindAsync<Entities.Attachment>(attachmentId);
            if (attachment != null)
            {
                dbContext.Attachments.Remove(attachment);
                await dbContext.SaveChangesAsync();

                await storageProvider.DeleteAsync(attachment.Path);
            }
        }

        private static string GetPath(Entities.Event @event, IFormFile file)
            => Path.Combine(@event.Date.Year.ToString(), @event.Id.ToString(), file.FileName);
    }
}
