using AutoMapper;
using TrainManagement.Dtos;
using TrainManagement.Models;

namespace TrainManagement.Profiles;

public class TrainProfile : Profile
{
    public TrainProfile()
    {
        CreateMap<Train, TrainReport>()
            .ForMember(dst => dst.TrainNumber, m => m.MapFrom(src => src.TrainNumber))
            .ForMember(dst => dst.TrainIndex, m => m.MapFrom(src => src.TrainIndex))
            .ForMember(dst => dst.StationName, m => m.MapFrom(src => src.PassedStops.Last().StationName))
            .ForMember(dst => dst.Date, m => m.MapFrom(src => src.PassedStops.Last().Cars.Select(c => c.WhenLastOperation).Max()))
            .ForMember(dst => dst.CarsCount, m => m.MapFrom(src => src.PassedStops.Last().Cars.Count()))
            .ForMember(dst => dst.DifferentFreightCount, m => m.MapFrom(src => src.PassedStops.Last().Cars.Select(c => c.FreightEtsngName).Distinct().Count()))
            .ForMember(dst => dst.TotalWeightTons, m => m.MapFrom(src => src.PassedStops.Last().Cars.Select(c => c.FreightTotalWeightTons).Sum()))
            .ForMember(dst => dst.Cars, m => m.MapFrom(src => src.PassedStops.Last().Cars))
            .ForMember(dst => dst.Freights,
                m => m.MapFrom(src =>
                    src.PassedStops.Last().Cars.GroupBy(c => c.FreightEtsngName).ToDictionary(c => c.Key,
                        grp => new FreightReport { Count = grp.Count(), TotalWeightTons = grp.Sum(c => c.FreightTotalWeightTons) })));
    }
}