using CarRental.Data;
using CarRental.Models.Car;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Test;

/// <summary>
/// # PenVing Code Review
/// 
/// ## Analys
/// Jag saknar test f�r anv�ndningsfall "A2. Registrering av �terl�mning av bil" som anges f�r uppgiften.
/// Vad �r anledningen till att du valt att inte skriva test f�r detta?
/// ## Bet�nkande
/// 
/// ## F�rdjupande
/// 
/// </summary>

[TestClass]
public class BookingServiceTest
{
    /// <summary>
    /// # PenVing Code Review
    /// 
    /// ## Analys
    /// H�r f�rmodar jag att du av ser att bevisa anv�ndningsfall "A1.Registrering av uttag av bil" f�r uppgiften.
    /// Metoden f�r att registrera bokning <see cref="BookingService.CreateBookingAsync"/> avkr�ver mer information �n vad som anges i anv�ndningsfallet.
    /// Detta p� grund av att du har valt att introducera konceptet om att ett fordon "Car" �r en individ som b�r sin m�tarst�llning.
    /// 
    /// ## Bet�nkande
    /// Jag hade velat se en mer direkt namngiven koppling till anv�ndningsfallet i �vningen.
    /// ## F�rdjupande
    /// </summary>
    [TestMethod]
    public async Task CreateBooking__Success__BasicInformationAdded()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CarRentalContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        await using var context = new CarRentalContext(options);

        var bookingService = new BookingService(context);
        var van = new Van();
        context.Cars.Add(van);
        await context.SaveChangesAsync();

        // Act
        var booking = await bookingService.CreateBookingAsync(van.Id, 22);
        
        // Assert
        Assert.IsNotNull(booking.BookingNumber);
        Assert.AreEqual(22, booking.PersonalIdentificationNumber);
        Assert.AreEqual(van.Mileage, booking.MileagePickUp);
        Assert.AreEqual(0, (DateTime.Now - booking.PickUpTime).TotalSeconds, 1); // Pick-up time is now with a one-second tolerance.
        Assert.AreEqual(typeof(Van), booking.Car.GetType());
    }
    
    /// <summary>
    /// For this example I will not create more test-cases, but in real life scenario I would love to add more test,
    /// specially for things that can go wrong, like this one.
    /// </summary>
    [TestMethod]
    public void CreateBooking__Failed__CarIsNotRegistered()
    {
        Assert.IsFalse(true);
    }
    
    [TestMethod]
    public void Return()
    {
        Assert.IsFalse(true);
    }
}