using AutoMapper;
using SneakersCollection.Api.ViewModels;

namespace SneakersCollection.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Data.Entities.Sneaker, Domain.Entities.Sneaker>().ReverseMap();
            CreateMap<Domain.Entities.Sneaker, SneakerViewModel>().ReverseMap();
        }
    }
}
