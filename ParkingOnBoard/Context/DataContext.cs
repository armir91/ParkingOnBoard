using Microsoft.EntityFrameworkCore;
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
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("Server=localhost;Database=POB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
    }
}
