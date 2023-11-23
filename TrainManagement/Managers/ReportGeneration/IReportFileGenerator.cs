using TrainManagement.Dtos;

namespace TrainManagement.Managers;

public interface IReportFileGenerator
{
    Task<byte[]> GetAsByteArray(TrainReport trainReport);
}