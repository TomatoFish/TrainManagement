using Microsoft.EntityFrameworkCore;
using TrainManagement.Models;

namespace TrainManagement.Data;

public class TrainRepo : ITrainRepo
{
    private readonly AppDbContext _context;

    public TrainRepo(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
        Console.WriteLine("--> SaveChangesAsync Completed");
    }
    
    public async Task CreateTrain(Train train)
    {
        await _context.AddAsync(train);
    }
    
    public async Task CreateTrains(IEnumerable<Train> trains)
    {
        await _context.AddRangeAsync(trains);
    }
    
    public async Task<IEnumerable<Train>> GetAllTrains()
    {
        return await _context.Trains.ToListAsync();
    }

    public async Task<Train?> GetTrainByNumber(int targetNumber)
    {
        return await _context.Trains.Include(t => t.PassedStops).ThenInclude(s => s.Cars.OrderBy(c => c.PositionInTrain)).FirstOrDefaultAsync(t => t.TrainNumber == targetNumber);
    }
}