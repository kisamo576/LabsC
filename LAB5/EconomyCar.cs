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
        Console.WriteLine("============================================");
        Console.WriteLine("|              Эконом Класс                |");
        Console.WriteLine("============================================");
        Console.WriteLine($"Марка: {Brand}");
        Console.WriteLine($"Модель: {Model}");
        Console.WriteLine($"Номер: {LicensePlate}");
        Console.WriteLine($"Цена: {Price}");
        Console.WriteLine($"Скорость: {MaxSpeed} км/ч");
        Console.WriteLine($"Расход: {FuelFlow} л/100км");
        Console.WriteLine($"Год: {Year}");
        Console.WriteLine($"Цвет: {Color}");
        Console.WriteLine($"Кондиционер: {(HasAirConditioner ? "Есть" : "Нет")}");
        Console.WriteLine("=============================================");
    
    }
    
    public override string GetCarType() => "Эконом";
    
    
    public override string GetInfo()
    {
        return $"[Эконом] {Brand} {Model} ({LicensePlate}) - {Year}г, {Price}$";
    }
}