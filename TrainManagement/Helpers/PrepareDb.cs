using Microsoft.EntityFrameworkCore;
using TrainManagement.Data;
using TrainManagement.Managers;

namespace TrainManagement.Helpers;

public class PrepareDb
{
    public static async Task PreparePopulation(IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            AppDbContext? dbContext = serviceScope.ServiceProvider.GetService<AppDbContext>();
            ICarReception? carReceptionManager = serviceScope.ServiceProvider.GetService<ICarReception>();
            if (dbContext != null && carReceptionManager != null)
            {
                await SeedData(dbContext, carReceptionManager);
            }
        }
    }

    private static async Task SeedData(AppDbContext dbContext, ICarReception carReception)
    {
        Console.WriteLine("--> Applying migrations...");
        try
        {
            dbContext.Database.Migrate();
            await SeedThemeData(dbContext, carReception);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"--> Can't run migrations: {ex.Message}\n{ex.StackTrace}");
        }
    }

    private static async Task SeedThemeData(AppDbContext dbContext, ICarReception carReception)
    {
        if (!dbContext.Trains.Any())
        {
            Console.WriteLine("--> Seeding data...");

            using (var file = File.OpenRead(@"./InitialData/Data.xml"))
            {
                var cars = XMLHelper.ParseStream(file);
                Console.WriteLine($"--> cars: {cars.Count()}");
                await carReception.ReceiveRange(cars);
            }
        }
        else
        {
            Console.WriteLine("--> Database already populated");
        }
    }
}