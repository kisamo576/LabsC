namespace LAB5;

public class BusinessCar : Car
{
    public bool HasLeatherSeats { get; set; }
    public bool HasPanorama { get; set; }
    public bool HasMassage { get; set; }

    public BusinessCar(string brand, string model, string licensePlate, double price,
        int maxSpeed, double fuelFlow, int year, string color,
        bool hasLeatherSeats, bool hasPanorama, bool hasMassage) 
        : base(brand, model, licensePlate, price, maxSpeed,
        fuelFlow, year, color)
    {
        HasLeatherSeats = hasLeatherSeats;
        HasPanorama = hasPanorama;
        HasMassage = hasMassage;
    }

    public override void DisplayInfo()
    {
        {
            Console.WriteLine("============================================");
            Console.WriteLine("|       $        Бизнес Класс     $        |");
            Console.WriteLine("============================================");
            Console.WriteLine($"Марка: {Brand} ");
            Console.WriteLine($"Модель: {Model} ");
            Console.WriteLine($"Номер: {LicensePlate} ");
            Console.WriteLine($"Цена: {Price,12:C}");
            Console.WriteLine($"Скорость: {MaxSpeed} км/ч");
            Console.WriteLine($"Расход: {FuelFlow} л/100км");
            Console.WriteLine($"Год: {Year}");
            Console.WriteLine($"Цвет: {Color}");
            Console.WriteLine($"Кожаные сиденья: {(HasLeatherSeats ? "Есть" : "Нет")}");
            Console.WriteLine($"Панорамная крыша: {(HasPanorama ? "Есть" : "Нет")}");
            Console.WriteLine($"Массажные кресла: {(HasMassage ? "Есть" : "Нет")}");
            Console.WriteLine("=============================================");
            
        }
    }
    
    public override string GetCarType() => "Бизнес";

    public override string GetInfo()
    {
        return $"[Бизнес] {Brand} {Model} ({LicensePlate}) - {Year}г, {Price}$";
    }
}