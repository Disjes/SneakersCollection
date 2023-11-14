using AutoMapper;
using SneakersCollection.Domain.ValueObjects;

namespace SneakersCollection.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Data.Entities.Sneaker, Domain.Entities.Sneaker>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => new Money(src.Price, "USD")))
                .ForMember(dest => dest.SizeUS, opt => opt.MapFrom(src => new Size(src.SizeUS, "US"))).ReverseMap();

            CreateMap<Domain.Entities.Sneaker, Data.Entities.Sneaker>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Amount))
                .ForMember(dest => dest.SizeUS, opt => opt.MapFrom(src => src.SizeUS.NumericSize)).ReverseMap();

            // Additional mapping for Decimal to Money
            CreateMap<decimal, Money>()
                .ConvertUsing(src => new Money(src, "USD"));

            // Additional mapping for Decimal to Size
            CreateMap<decimal, Size>()
                .ConvertUsing(src => new Size(src, "US"));

            CreateMap<Entities.Brand, Domain.Entities.Brand>().ReverseMap();
        }
    }
}
