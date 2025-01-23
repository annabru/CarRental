namespace CarRental.Models.Car;

public sealed class Van : Car
{

    // Right now I think this is good enough. It could also be static. 
    // In the future, maybe one would like to offer different subscription models, e.g. (higher base rent, free km) or
    // (lower base rent, cost per km), and then maybe this logic should be somewhere else. 
    public override double CalculateCost(int nbrOfDays, int nbrOfKm)
    {
        if (nbrOfDays < 1 || nbrOfKm < 0)
        {
            throw new ArgumentOutOfRangeException();
        }
        return BaseRent * nbrOfDays * 1.2 + BaseCostPerKilometer * nbrOfKm;
    }
}