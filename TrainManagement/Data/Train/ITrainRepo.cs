using TrainManagement.Models;

namespace TrainManagement.Data;

public interface ITrainRepo
{
    Task SaveChanges();
    Task CreateTrain(Train train);
    Task CreateTrains(IEnumerable<Train> trains);
    Task<IEnumerable<Train>> GetAllTrains();
    Task<Train?> GetTrainByNumber(int targetTrainNumber);
}