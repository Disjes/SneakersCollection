using AutoMapper;
using SneakersCollection.Api.ViewModels;
using SneakersCollection.Domain.ValueObjects;

namespace SneakersCollection.Api
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

            CreateMap<Domain.Entities.Sneaker, SneakerViewModel>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Amount))
                .ForMember(dest => dest.SizeUS, opt => opt.MapFrom(src => src.SizeUS.NumericSize)).ReverseMap();

            CreateMap<SneakerViewModel, Domain.Entities.Sneaker>()
                .ForPath(src => src.Price.Amount, opt => opt.MapFrom(dest => dest.Price))
                .ForPath(src => src.SizeUS.NumericSize, opt => opt.MapFrom(dest => dest.SizeUS)).ReverseMap();

            // Additional mapping for Decimal to Money
            CreateMap<decimal, Money>()
                .ConvertUsing(src => new Money(src, "USD"));

            // Additional mapping for Decimal to Size
            CreateMap<decimal, Size>()
                .ConvertUsing(src => new Size(src, "US"));

            CreateMap<Data.Entities.Brand, Domain.Entities.Brand>().ReverseMap();

        }
    }
}
