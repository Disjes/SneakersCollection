namespace SneakersCollection.Tests
{
    using AutoMapper;
    using SneakersCollection.Api.ViewModels;
    using SneakersCollection.Domain.ValueObjects;

    public class AutoMapperTestConfig
    {
        public static IMapper Initialize()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Data.Entities.Sneaker, Domain.Entities.Sneaker>()
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => new Money(src.Price, "USD")))
                    .ForMember(dest => dest.SizeUS, opt => opt.MapFrom(src => new Size(src.SizeUS, "US"))).ReverseMap();

                cfg.CreateMap<Domain.Entities.Sneaker, Data.Entities.Sneaker>()
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Amount))
                    .ForMember(dest => dest.SizeUS, opt => opt.MapFrom(src => src.SizeUS.NumericSize)).ReverseMap();

                cfg.CreateMap<Domain.Entities.Sneaker, SneakerViewModel>()
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Amount))
                    .ForMember(dest => dest.SizeUS, opt => opt.MapFrom(src => src.SizeUS.NumericSize)).ReverseMap();

                // Additional mapping for Decimal to Money
                cfg.CreateMap<decimal, Money>()
                    .ConvertUsing(src => new Money(src, "USD"));

                // Additional mapping for Decimal to Size
                cfg.CreateMap<decimal, Size>()
                    .ConvertUsing(src => new Size(src, "US"));
            });

            return configuration.CreateMapper();
        }
    }
}
