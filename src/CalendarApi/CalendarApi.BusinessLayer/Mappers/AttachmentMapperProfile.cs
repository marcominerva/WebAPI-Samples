using System.IO;
using AutoMapper;
using CalendarApi.Shared.Models;
using Entities = CalendarApi.DataAccessLayer.Entities;

namespace CalendarApi.BusinessLayer.Mappers
{
    public class AttachmentMapperProfile : Profile
    {
        public AttachmentMapperProfile()
        {
            CreateMap<Entities.Attachment, Attachment>()
                .ForMember(dto => dto.FileName, opt => opt.MapFrom(source => Path.GetFileName(source.Path)));
        }
    }
}
