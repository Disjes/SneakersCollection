using AutoMapper;
using SneakersCollection.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneakersCollection.Data
{
    public class SneakerMappingProfile : Profile
    {
        public SneakerMappingProfile()
        {
            CreateMap<Data.Entities.Sneaker, Domain.Entities.Sneaker>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => new Money(src.Price, "USD")))
                .ForMember(dest => dest.SizeUS, opt => opt.MapFrom(src => new Size(src.SizeUS, "US")));

            CreateMap<Domain.Entities.Sneaker, Data.Entities.Sneaker>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Amount))
                .ForMember(dest => dest.SizeUS, opt => opt.MapFrom(src => src.SizeUS.NumericSize));
        }
    }
}
