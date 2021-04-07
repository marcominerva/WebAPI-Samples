using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CalendarApi.BusinessLayer.Extensions;
using CalendarApi.BusinessLayer.Settings;
using CalendarApi.DataAccessLayer;
using CalendarApi.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MimeMapping;
using Entities = CalendarApi.DataAccessLayer.Entities;

namespace CalendarApi.BusinessLayer.Services
{
    public class AttachmentService : IAttachmentService
    {
        private readonly CalendarDbContext dbContext;
        private readonly IMapper mapper;
        private readonly AppSettings appSettings;

        public AttachmentService(CalendarDbContext dbContext, IMapper mapper, IOptions<AppSettings> appSettingsOptions)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            appSettings = appSettingsOptions.Value;
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
                var fullPath = Path.Combine(appSettings.StorageFolder, path);

                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                await File.WriteAllBytesAsync(fullPath, content);

                @event.Attachments.Add(new Entities.Attachment { Path = path, Length = content.Length });
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<(byte[] Content, string ContentType)?> GetAsync(Guid attachmentId)
        {
            var attachment = await dbContext.FindAsync<Entities.Attachment>(attachmentId);
            if (attachment != null)
            {
                var fullPath = Path.Combine(appSettings.StorageFolder, attachment.Path);
                var content = await File.ReadAllBytesAsync(fullPath);

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

                var fullPath = Path.Combine(appSettings.StorageFolder, attachment.Path);
                File.Delete(fullPath);
            }
        }

        private string GetPath(Entities.Event @event, IFormFile file)
            => Path.Combine(@event.Date.Year.ToString(), @event.Id.ToString(), file.FileName);
    }
}
