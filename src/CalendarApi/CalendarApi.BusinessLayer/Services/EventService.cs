using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CalendarApi.DataAccessLayer;
using CalendarApi.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Entities = CalendarApi.DataAccessLayer.Entities;

namespace CalendarApi.BusinessLayer.Services
{
    public class EventService : IEventService
    {
        private readonly CalendarDbContext dbContext;
        private readonly IMapper mapper;

        public EventService(CalendarDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<Event>> GetAsync()
        {
            var events = await dbContext.Events.AsNoTracking()
                .OrderBy(e => e.Date)
                .ProjectTo<Event>(mapper.ConfigurationProvider).ToListAsync();

            return events;
        }

        public async Task SaveAsync(EventRequest eventRequest)
        {
            var dbEvent = eventRequest.Id != Guid.Empty ? await dbContext.FindAsync<Entities.Event>(eventRequest.Id) : null;

            if (dbEvent == null)
            {
                dbEvent = mapper.Map<Entities.Event>(eventRequest);
                dbEvent.CreatedDate = DateTime.UtcNow;
                dbContext.Events.Add(dbEvent);
            }
            else
            {
                mapper.Map(eventRequest, dbEvent);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
