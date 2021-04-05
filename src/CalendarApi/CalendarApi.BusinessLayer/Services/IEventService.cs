using System.Collections.Generic;
using System.Threading.Tasks;
using CalendarApi.Shared.Models;

namespace CalendarApi.BusinessLayer.Services
{
    public interface IEventService
    {
        Task SaveASync(Event @event);

        Task<IEnumerable<Event>> GetAsync();
    }
}