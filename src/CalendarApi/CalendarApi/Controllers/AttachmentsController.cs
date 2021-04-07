using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using CalendarApi.BusinessLayer.Services;
using CalendarApi.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CalendarApi.Controllers
{
    [Route("api/events")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class AttachmentsController : ControllerBase
    {
        private readonly IAttachmentService attachmentService;

        public AttachmentsController(IAttachmentService attachmentService)
        {
            this.attachmentService = attachmentService;
        }

        [HttpGet("{id:guid}/attachments")]
        [ProducesResponseType(typeof(IEnumerable<Attachment>), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetList(Guid id)
        {
            var attachments = await attachmentService.GetListAsync(id);
            return Ok(attachments);
        }

        [HttpPost("{id:guid}/attachments")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Save(Guid id, [BindRequired] IFormFile attachment)
        {
            await attachmentService.SaveAsync(id, attachment);
            return NoContent();
        }

        [HttpGet("{eventId:guid}/attachments/{attachmentId:guid}")]
        [Produces(contentType: MediaTypeNames.Application.Octet, MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Get(Guid eventId, Guid attachmentId)
        {
            var attachment = await attachmentService.GetAsync(attachmentId);
            if (attachment != null)
            {
                return File(attachment.Value.Content, attachment.Value.ContentType);
            }

            return NotFound();
        }

        [HttpDelete("{eventId:guid}/attachments/{attachmentId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(Guid eventId, Guid attachmentId)
        {
            await attachmentService.DeleteAsync(attachmentId);
            return NoContent();
        }
    }
}