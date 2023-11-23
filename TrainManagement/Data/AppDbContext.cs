using Microsoft.EntityFrameworkCore;
using TrainManagement.Models;

namespace TrainManagement.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Train> Trains { get; set; }
    public DbSet<Stop> Stops { get; set; }
    public DbSet<Car> Cars { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Train>().HasMany(e => e.PassedStops).WithOne(e => e.ParentTrain).HasForeignKey(e => e.ParentTrainId);
        modelBuilder.Entity<Stop>().HasMany(e => e.Cars).WithOne(e => e.ParentStop).HasForeignKey(e => e.ParentStopId);
    }
}