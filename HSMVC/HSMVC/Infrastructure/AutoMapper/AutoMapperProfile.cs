using AutoMapper;
using HSMVC.Controllers.Commands;
using HSMVC.Domain;

namespace HSMVC.Infrastructure.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Conference, ConferenceEditCommand>();
            CreateMap<ConferenceEditCommand, Conference>();
            CreateMap<ConferenceAddCommand, Conference>();
        }
    }
}