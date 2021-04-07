using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CalendarApi.Shared.Models;
using Microsoft.AspNetCore.Http;

namespace CalendarApi.BusinessLayer.Services
{
    public interface IAttachmentService
    {
        Task<IEnumerable<Attachment>> GetListAsync(Guid eventId);

        Task<(byte[] Content, string ContentType)?> GetAsync(Guid attachmentId);

        Task SaveAsync(Guid eventId, IFormFile attachment);

        Task DeleteAsync(Guid attachmentId);
    }
}