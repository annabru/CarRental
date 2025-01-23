using CarRental.Models;
using CarRental.Models.Car;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Data;

public class CarRentalContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Car> Cars { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>()
            .HasDiscriminator<string>("CarType") // Add a discriminator column
            .HasValue<Van>("Van");
    }
}