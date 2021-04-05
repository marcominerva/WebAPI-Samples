using AutoMapper;
using CalendarApi.Shared.Models;
using Entities = CalendarApi.DataAccessLayer.Entities;

namespace CalendarApi.BusinessLayer.Mappers
{
    public class EventMapperProfile : Profile
    {
        public EventMapperProfile()
        {
            CreateMap<Entities.Event, Event>().ReverseMap();
        }
    }
}
