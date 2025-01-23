namespace CarRental.Models.Car;

public sealed class SmallCar : Car
{
    public override double CalculateCost(int nbrOfDays, int nbrOfKm)
    {
        if (nbrOfDays < 1 || nbrOfKm < 0)
        {
            throw new ArgumentOutOfRangeException();
        }
        return BaseRent * nbrOfDays;
    }
}