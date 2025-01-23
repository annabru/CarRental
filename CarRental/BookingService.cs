using CarRental.Data;
using CarRental.Models;

namespace CarRental;

/// <summary>
/// This is a very simple Booking service and lacks functionality that I would deem necessary.
///
/// Eg, from the description a booking is never made in advanced, hence pick-up is done immediately. 
/// </summary>
public sealed class BookingService
{
    /// <summary>
    /// Here EF Core is used as my repository. It is a well-developed ORM and together with linq expression you get a
    /// high performance access-layer.
    ///
    /// Since EF-core also can be set up using in-memory database, I have never encountered issues with creating unit tests. 
    /// </summary>
    private readonly CarRentalContext _dbContext;
    public BookingService(CarRentalContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Booking> CreateBookingAsync(Guid carId, int personalIdentificationNumber)
    {
        var car = await _dbContext.Cars.FindAsync(carId);
        if (car is null)
        {
            throw new ArgumentException("Unknown car");
        }
        var booking = new Booking(car, personalIdentificationNumber);
        _dbContext.Bookings.Add(booking);

        await _dbContext.SaveChangesAsync();
        return booking;
    }

    public async Task Return(Booking booking)
    {
        var dbBooking = await _dbContext.Bookings.FindAsync(booking.BookingNumber);
        if (dbBooking is null)
        {
            throw new ArgumentException("Unknown booking");
        }

        // Here I would probably sync mileage with the actual car, if we do not get status updates regularly and this step can be skipped.

        dbBooking.Return();

        await _dbContext.SaveChangesAsync();
    }
}