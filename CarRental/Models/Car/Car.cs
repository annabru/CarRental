using System;

namespace CarRental.Models.Car;

public abstract class Car
{
    /// <summary>
    /// # PengVin Code Review
    /// 
    /// ## Analys
    /// 
    /// För denna uppgift är konceptet Car som individ med identitet Id(GUID), VIN-nummer eller registreringsnummer inte efterfrågat men jag kan förstå att du leddes in i denna tanke.
    /// 
    /// Klassen blandar tre koncept
    /// 1.  Car som individuell individ av fordon, detta ges av <see cref="Id"/> och <see cref="Mileage"/> som här bär data för en specifik individ/instans av bil.
    /// 2.  Beräkningsmodell för kategorier av fordon, som här blir hårt kopplad till typen via denna konstruktion med arvsstruktur. <see cref="CalculateCost(int, int)"/>.
    /// 3.  Klassen utgör modell för persistering i databas, vilket ges av <see cref="Id"/> som är ett koncept som inte har mening i affärsmodellen för uthyrning men som ger avgörande mening i persisteringslagret för Entity Framework.
    ///     Detta är inte lika tydligt här som i metod för klass <see cref="Booking.BookingNumber"> [Key] public Guid BookingNumber { get; init; } </see> där ett attribut [Key] måste anges för att Entity Framework ska förstå att detta är en primärnyckel.
    /// 
    /// Med nuvarande val av konstruktion följer att: För var ny typ av fordon ELLER avvikande beräkningsmodell, krävs att ny klass skapas som ärver från Car.
    /// "ELLER" i meningen ovan är vad som indikerar att något är galet med konstruktionen.
    /// Klassen bryter mot två vedertagna designprinciper: Single Responsibility Principle (SRP) och Open/Closed Principle (OCP).
    /// SRP säger att en klass ska ha ett ansvar, och OCP säger att en klass ska vara öppen för utökningsmen stängd för ändring.
    /// 
    /// Persisteringskonceptet är inte en del av affärsmodellen för uthyrning, utan är en teknisk detalj för att lagra data i databas och skall hållas separat.
    /// Nuvarande konstruktion försvårar byte av datalager genom att hårt knyta an till Entity Framework.
    /// Uppgiften inleder med följande stycke:
    /// <para>
    /// Scenario 
    /// Du ingår i ett team på Acme AB som ska bygga ett system som ska hantera uthyrning av bilar.
    /// Systemet kommer att anpassas för flera olika kunder som alla har olika önskemål om hur data ska lagras och vilken typ av användargränssnitt som ska användas.
    /// </para>
    /// 
    /// ## Betänkande
    /// Ett sätt som gör modellen mer flexibel, lättare att underhålla och ändra framöver, är att dela på koncepten i flera klasser och göra en komposition, dvs att Car via konstruktor tar in en beräkningsmodell och en persisterinsgmodell.
    /// Exempel: <code> new Car(IRentalPriceModel beräkningsmodell, ICarData carData) </code>
    /// Att välja "Composition over Inheritance" ger en mer flexibel lösning.
    /// 
    /// Genom att dela dessa koncept respekteras SOLID och möjliggör att skapa nya beräkningsmodeller för fordon utan att behöva skapa nya klasser för varje ny beräkningsmodell.
    /// 
    /// I en vidare tanke som föreslagen refakturering öppnar för är att beräkningmodellen inte nödvändigtvis hör hemma i Car eller någon annan typ som ärver från Car, det kanske rent av är mer rimligt att beräkningmodellen är ett helt fristående koncept som kopplas till bokning.
    /// Jag kan tänka mig att en säljare som vill ge rabatt på en bokning inte vill skapa en ny typ av bil för att ge rabatt, utan att säljaren vill skapa/konfigurera en rabattmodell som kopplas till bokningen.
    /// 
    /// 
    /// ## Fördjupande
    /// <see href="https://cln.co/SRP"> Single Responsibility Principle </see>
    /// <see href="https://cln.co/OCP"> Open Closed Principle </see> 
    /// <see href="https://cln.co/CoI"> Composition over Inheritance </see>
    /// <see href="https://www.educative.io/blog/solid-principles-oop-c-sharp"> SOLID </see>
    /// <see href="https://medium.com/@chandrashekharsingh25/understanding-the-repository-pattern-in-c-net-with-examples-51f02c4074ba"> Understanding the Repository Pattern in C# .NET with Examples </see>
    /// <see href="https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design"> Infrastructure and persistence layer design </see>
    /// </summary>
    private protected const double BaseRent = 500;
    private protected const double BaseCostPerKilometer = 5;
    public int Mileage { get; set; }
    public Guid Id { get; init; } = Guid.NewGuid();

    public abstract double CalculateCost(int nbrOfDays, int nbrOfKm);
}