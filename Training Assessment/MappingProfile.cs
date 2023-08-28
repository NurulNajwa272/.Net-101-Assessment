using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace Training_Assessment
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Event, EventDto>();
            CreateMap<Customer, CustomerDto>();
            CreateMap<EventForCreationDto, Event>();
            CreateMap<EventForUpdateDto, Event>();
        }
    }
}
