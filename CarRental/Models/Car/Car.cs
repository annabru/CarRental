namespace CarRental.Models.Car;

public abstract class Car
{
    private protected const double BaseRent = 500;
    private protected const double BaseCostPerKilometer = 5;
    public int Mileage { get; set; }
    public Guid Id { get; init; } = Guid.NewGuid();

    public abstract double CalculateCost(int nbrOfDays, int nbrOfKm);
}