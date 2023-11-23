using TrainManagement.Data;
using TrainManagement.Dtos;
using TrainManagement.Models;

namespace TrainManagement.Managers;

public class CarReceptionManager : ICarReception
{
    private readonly ITrainRepo _trainRepo;

    public CarReceptionManager(ITrainRepo trainRepo)
    {
        _trainRepo = trainRepo;
    }
    
    public async Task Receive(CarReceive receive)
    {
        var (haveTrain, train) = await TryGetTrainFromDB(receive, null);

        if (!haveTrain)
            await _trainRepo.CreateTrain(train);
        await _trainRepo.SaveChanges();
    }

    public async Task ReceiveRange(IEnumerable<CarReceive> carReceive)
    {
        var newTrains = new List<Train>();
        foreach (var receive in carReceive)
        {
            var train = newTrains.FirstOrDefault(t => t.TrainNumber == receive.TrainNumber);
            var (haveTrain, newTrain) = await TryGetTrainFromDB(receive, train);
            if (!haveTrain)
                newTrains.Add(newTrain);
        }
        await _trainRepo.CreateTrains(newTrains);
        await _trainRepo.SaveChanges();
    }

    private async Task<(bool haveTrain, Train train)> TryGetTrainFromDB(CarReceive receive, Train? train)
    {
        train ??= await _trainRepo.GetTrainByNumber(receive.TrainNumber);
        var haveTrain = train != null;
        if (!haveTrain)
        {
            train = new Train
            {
                TrainNumber = receive.TrainNumber, TrainIndexCombined = receive.TrainIndexCombined,
                TrainIndex = receive.TrainIndex, FromStationName = receive.FromStationName,
                ToStationName = receive.ToStationName
            };
        }

        var stop = train.PassedStops.LastOrDefault();
        if (stop == null || stop.StationName != receive.LastStationName)
        {
            stop = new Stop { StationName = receive.LastStationName };
            train.PassedStops.Add(stop);
        }
        train.PassedStops.Add(stop);

        var car = new Car
        {
            InvoiceNum = receive.InvoiceNum, PositionInTrain = receive.PositionInTrain,
            CarNumber = receive.CarNumber, WhenLastOperation = receive.WhenLastOperation,
            LastOperationName = receive.LastOperationName, FreightEtsngName = receive.FreightEtsngName,
            FreightTotalWeightKg = receive.FreightTotalWeightKg
        };
        stop.Cars.Add(car);

        return (haveTrain, train);
    }
}