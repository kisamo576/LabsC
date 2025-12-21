namespace LAB5;

public class EconomyCar : Car
{
    public bool HasAirConditioner { get; set; }

    public EconomyCar(string brand, string model, string licensePlate, double price,
        int maxSpeed, double fuelFlow, int year, string color, bool hasAirConditioner) 
        : base(brand, model, licensePlate, price, maxSpeed, fuelFlow, year, color)
    {
        HasAirConditioner = hasAirConditioner;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"    [Эконом] {Brand}, {Model} ({LicensePlate})");
        Console.WriteLine($"    Год: {Year}, Цвет: {Color}, Цена: {Price}");
        Console.WriteLine($"    Максимальная скорость: {MaxSpeed}км/ч, Расход: {FuelFlow} л/100км");
        Console.WriteLine($"    Кондиционер: {(HasAirConditioner ? "Есть" : "Нет")}");
    }
    
    public override string GetCarType() => "Эконом";
}