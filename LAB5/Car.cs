namespace LAB5;

public abstract class Car
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public string LicensePlate { get; set; }
    public double Price { get; set; }
    public int MaxSpeed { get; set; }
    public double FuelFlow { get; set; }
    public int Year { get; set; }
    public string Color { get; set; }

    protected Car(string brand, string model, string licensePlate, double price, int maxSpeed, double fuelFlow,
        int year, string color)
    {
        Brand = brand;
        Model = model;
        LicensePlate = licensePlate;
        Price = price;
        MaxSpeed = maxSpeed;
        FuelFlow = fuelFlow;
        Year = year;
        Color = color;
    }
    
    public abstract void DisplayInfo();
    public abstract string GetCarType();

    public  virtual string GetInfo()
    {
        return $"{Brand} {Model} ({LicensePlate}) - {Color}, {Year} год";
    }
}