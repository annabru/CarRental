using CarRental.Models.Car;

using System.ComponentModel.DataAnnotations;

namespace CarRental.Models;
/// <summary>
/// # PenVing Code Review
/// 
/// ## Analys
/// Här ansvarar klassen för både affärslogik och persistering på samma sätt som för <see cref="Car"/>
/// Affärslogik som potentiellt metoden <see cref="Return"/> borde hantera kan lätt sättas ur spel pga publikt tillgängliga property setters <see cref="MileageReturn"/> och <see cref="ReturnTime"/>
/// 
/// ## Betänkande
/// <see cref="PersonalIdentificationNumber"/> är av typen <cref="int"/> vilket kan vara en begränsning om vi vill ha stöd för internationella kunder och slutar att funka  
/// Int32.MaxValue = 2147483647, vilket gör att det är svårt att få plats med ett personnummer som är 12 siffror långt.

/// 
/// ## Fördjupande
/// </summary>
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