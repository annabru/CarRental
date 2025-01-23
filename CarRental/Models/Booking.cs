using System.ComponentModel.DataAnnotations;

namespace CarRental.Models;

public sealed class Booking
{
    // Parameterless constructor for EF Core
    public Booking()
    {
    }
    public Booking(Car.Car car, int personalIdentificationNumber)
    {
        Car = car;
        PersonalIdentificationNumber = personalIdentificationNumber;
        MileagePickUp = Car.Mileage;
    }

    [Key] public Guid BookingNumber { get; init; }
    /// <summary>
    /// Simplified from spec. Would probably want some kind of customer model so that we can also have e.g. email, phone number and name. 
    /// </summary>
    public int PersonalIdentificationNumber { get; init; }
    public Car.Car Car { get; init; }
    public DateTime PickUpTime { get; } = DateTime.Now;
    public DateTime ReturnTime { get; set; }
    public int MileagePickUp { get; init; }
    public int MileageReturn { get; set; }
    public double Cost { get; private set; }

    public void Return()
    {
        ReturnTime = DateTime.Now;
        MileageReturn = Car.Mileage;
        Cost = Car.CalculateCost(4, MileageReturn - MileagePickUp);
    }
}