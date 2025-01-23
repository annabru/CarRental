using CarRental.Models.Car;

namespace CarRental.Test;

/// <summary>
/// For this simple demo I decided to have tests for all derived classes in the same file. If there were more methods for
/// each class I would have seperated it and only have test for common logic here.
/// </summary>
[TestClass]
public class CarTest
{
    [TestMethod]
    [DataRow(0,2)] // Must book for a minimum of 1 day
    [DataRow(-1,2)] // No negatives
    [DataRow(1,-2)] // No negatives
    public void CalculateCost_ArgumentOutOfRange(int nbrOfDays, int nbrOfKm)
    {
        var van = new Van();
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => van.CalculateCost(nbrOfDays, nbrOfKm));
        var car = new SmallCar();
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => car.CalculateCost(nbrOfDays, nbrOfKm));
        var bus = new MiniBus();
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => bus.CalculateCost(nbrOfDays, nbrOfKm));
        
    }
    
    [TestMethod]
    [DataRow(1,2,610)]
    [DataRow(2,20,1300)]
    [DataRow(2,50,1450)]
    public void CalculateCost_Van(int nbrOfDays, int nbrOfKm, double expectedCost)
    {
        var van = new Van();
        var cost = van.CalculateCost(nbrOfDays, nbrOfKm);
        
        Assert.AreEqual(expectedCost, cost);
    }
    
    [TestMethod]
    [DataRow(1,2,500)]
    [DataRow(2,20,1000)]
    [DataRow(2,50,1000)]
    public void CalculateCost_SmallCar(int nbrOfDays, int nbrOfKm, double expectedCost)
    {
        var smallCar = new SmallCar();
        var cost = smallCar.CalculateCost(nbrOfDays, nbrOfKm);
        
        Assert.AreEqual(expectedCost, cost);
    }
    
    [TestMethod]
    [DataRow(1,2,865)]
    [DataRow(2,20,1850)]
    [DataRow(2,50,2075)]
    public void CalculateCost_MiniBus(int nbrOfDays, int nbrOfKm, double expectedCost)
    {
        var miniBus = new MiniBus();
        var cost = miniBus.CalculateCost(nbrOfDays, nbrOfKm);
        
        Assert.AreEqual(expectedCost, cost);
    }
}