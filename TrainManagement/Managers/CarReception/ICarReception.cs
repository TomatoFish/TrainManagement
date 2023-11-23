using TrainManagement.Dtos;

namespace TrainManagement.Managers;

public interface ICarReception
{
    Task Receive(CarReceive carReceive);
    Task ReceiveRange(IEnumerable<CarReceive> carReceive);
}