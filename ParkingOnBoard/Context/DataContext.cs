using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ParkingOnBoard.Entities;

namespace ParkingOnBoard.Context;

public class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Street> Streets { get; set; }
    public DbSet<Slot> Slots { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
        IConfiguration configuration = configurationBuilder.Build();

        optionsBuilder.UseSqlServer(configuration.GetConnectionString("ConnectionString"));
    }
}
