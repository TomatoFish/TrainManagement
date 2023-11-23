using AutoMapper;
using TrainManagement.Dtos;
using TrainManagement.Models;

namespace TrainManagement.Profiles;

public class CarProfile : Profile
{
    public CarProfile()
    {
        CreateMap<Car, CarReport>()
            .ForMember(dst => dst.PositionInTrain, m => m.MapFrom(src => src.PositionInTrain))
            .ForMember(dst => dst.CarNumber, m => m.MapFrom(src => src.CarNumber))
            .ForMember(dst => dst.InvoiceNum, m => m.MapFrom(src => src.InvoiceNum))
            .ForMember(dst => dst.Date, m => m.MapFrom(src => src.WhenLastOperation))
            .ForMember(dst => dst.FreightEtsngName, m => m.MapFrom(src => src.FreightEtsngName))
            .ForMember(dst => dst.FreightTotalWeightTons, m => m.MapFrom(src => src.FreightTotalWeightTons))
            .ForMember(dst => dst.OperationName, m => m.MapFrom(src => src.LastOperationName));
    }
}