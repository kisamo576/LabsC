namespace LAB5;

public class SUV : Car
{
    public bool IsFWD { get; set; }
    public int Clearance {get; set;}
    public int NumSeats  { get; set; }

    public SUV(string brand, string model, string licensePlate, double price,
        int maxSpeed, double fuelFlow, int year, string color,
        bool isFwd, int clearance, int numSeats)
        : base(brand, model, licensePlate, price, maxSpeed,
            fuelFlow, year, color)
    {
        IsFWD = isFwd;
        Clearance = clearance;
        NumSeats = numSeats;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine("============================================");
        Console.WriteLine("|              Внедорожник                 |");
        Console.WriteLine("============================================");
        Console.WriteLine($"Марка: {Brand}");
        Console.WriteLine($"Модель: {Model}");
        Console.WriteLine($"Номер: {LicensePlate}");
        Console.WriteLine($"Цена: {Price,12}");
        Console.WriteLine($"Скорость: {MaxSpeed} км/ч");
        Console.WriteLine($"Расход: {FuelFlow} л/100км");
        Console.WriteLine($"Год: {Year}");
        Console.WriteLine($"Цвет: {Color}");
        Console.WriteLine($"Полный привод: {(IsFWD ? "Да" : "Нет")}");
        Console.WriteLine($"Клиренс: {Clearance} мм");
        Console.WriteLine($"Количество мест: {NumSeats}");
        Console.WriteLine("=============================================");
    }

    public override string GetCarType() => "Внедорожник";
    
    public override string GetInfo()
    {
        return $"[Внедорожник] {Brand} {Model} ({LicensePlate}) - {Year}г, {Price}$";
    }
}