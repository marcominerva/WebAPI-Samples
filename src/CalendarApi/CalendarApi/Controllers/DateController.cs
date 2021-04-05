using System;
using System.Net.Mime;
using CalendarApi.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CalendarApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class DateController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(DateInformation), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public IActionResult Get()
        {
            var dateInformation = new DateInformation
            {
                Date = DateTime.Now,
                TimeZone = TimeZoneInfo.Local.Id
            };

            return Ok(dateInformation);
        }
    }
}
