namespace CarRental.Models.Car;

public sealed class MiniBus : Car
{
    public override double CalculateCost(int nbrOfDays, int nbrOfKm)
    {
        if (nbrOfDays < 1 || nbrOfKm < 0)
        {
            throw new ArgumentOutOfRangeException();
        }
        return BaseRent * nbrOfDays * 1.7 + BaseCostPerKilometer * nbrOfKm * 1.5;
    }
}