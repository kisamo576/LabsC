namespace LAB5;

class Program
{
    static void Main(string[] args)
    {
        TaxiPark park = new TaxiPark();
        
        park.AddCar(new EconomyCar("Volkswagen", "Polo sedan", "ABC123", 10000, 180, 7.0, 2015, "Белый", true));
        park.AddCar(new EconomyCar("Renault", "Logan", "DFG321", 9000, 160, 6.5, 2018, "Синий", true));
        park.AddCar(new EconomyCar("Skoda", "Rapid", "QWE761", 11000, 190, 8.0, 2019, "Серый", true));
        
        
        park.PrintCars();
        
        Console.WriteLine($"===Общая стоимость таксопарка: {park.CalculateTotalPrice()}===\n");
        
        Console.WriteLine($"===Сортировка по расходу топлива:=== \n");
        var sorted = park.SortByFuelFlow();
        foreach (var car in sorted)
        {
            Console.WriteLine($"    {car.GetInfo()} - {car.FuelFlow} л/100км");
        }
        
        Console.WriteLine("\n===Автомобили со скоростью 170-200:=== \n");
        var speedCars = park.FindCarsBySpeedRange(170, 200);
        foreach (var car in speedCars)
        {
            Console.WriteLine($"    {car.GetInfo()} - {car.MaxSpeed} км/ч");
        }
        
        Console.WriteLine("\n Нажмите Enter для завершения...");
        Console.ReadLine();
    }
}