using Microsoft.EntityFrameworkCore;

namespace ParkingOnBoard.Context;

public static class AutoMigrateConfiguration
{
    public static async void AutoMigrateAndSeed()
    {
        try
        {
            using DataContext context = new();
            context.Database.Migrate();
            await Seed.SeedData(context);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
