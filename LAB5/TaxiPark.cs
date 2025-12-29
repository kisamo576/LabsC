using System.Reflection.Metadata;

namespace LAB5;

public class TaxiPark
{
    private List<Car> cars = new List<Car>();
    

    // public override void DisplayInfo()
    // {
    //     Console.WriteLine("============================================");
    //     Console.WriteLine("|              Эконом Класс                |");
    //     Console.WriteLine("============================================");
    //     Console.WriteLine($"Марка: {Brand}");
    //     Console.WriteLine($"Модель: {Model}");
    // }
        
 
    // public override string GetCarType()
    // {
    //     return "TaxiPark";
    // }
    //
    // public  virtual string GetInfo()
    // {
    //     return $"{Brand} {Model} ({LicensePlate}) - {Color}, {Year} год";
    // }

    public static TaxiPark InitializePark()
    {
        TaxiPark park = new TaxiPark();
        
        park.AddCar(new EconomyCar("Volkswagen", "Polo", "ABC123", 10000, 180, 7.0, 2015, "Белый", true));
        park.AddCar(new EconomyCar("Renault", "Logan", "DFG321", 9000, 160, 6.5, 2018, "Синий", true));
        park.AddCar(new EconomyCar("Skoda", "Rapid", "QWE761", 11000, 190, 8.0, 2019, "Серый", true));

        park.AddCar(new BusinessCar("Mercedes", "E-Class", "MVP333", 55000, 220, 10.2, 2021, "Чёрный", true, true, true));
        park.AddCar(new BusinessCar("BMW", "7 Series", "WOW777", 62000, 240, 13.1, 2022, "Синий", true, false, true));
        park.AddCar(new BusinessCar("Audi", "A8 Long", "SSS111", 80000, 250, 14, 2023, "Чёрный", true, true, true ));

        park.AddCar(new SUV("Toyota", "Land Cruiser", "BIG999", 60000, 190, 12.5, 2019, "Чёрный", true, 225, 7));
        park.AddCar(new SUV("Nissan", "X-Trail", "JAP525", 32000, 190, 9.8, 2020, "Красный", false, 210, 5));
        park.AddCar(new SUV("BMW", "X6", "MMM666", 45000, 240, 11.2, 2021, "Зелёный", true, 260, 4));
        
        return park;
    }

    public void AddCar(Car car)
    {
        foreach (var existingCar in cars)
        {
            if (existingCar.LicensePlate.ToUpper() == car.LicensePlate.ToUpper())
            {
                throw new ArgumentException($"Автомобиль с номером {car.LicensePlate} уже существует!");
            }
        }
        
        cars.Add(car);
    }

    public bool RemoveCar(string licensePlate)
    {
        if (string.IsNullOrWhiteSpace(licensePlate))
        {
            return false;
        }
    
        string searchPlate = licensePlate.Trim().ToUpper();
    
        for (int i = 0; i < cars.Count; i++)
        {
            if (cars[i].LicensePlate.Trim().ToUpper() == searchPlate)
            {
                cars.RemoveAt(i);
                return true;
            }
        }
    
        return false;
    }
    
    public List<Car> GetCars() => new List<Car>(cars);

    public double CalculateTotalPrice()
    {
        return cars.Sum(car => car.Price);
    }

    public List<Car> SortByFuelFlow()
    {
        return cars.OrderBy(car => car.FuelFlow).ToList();
    }

    public List<Car> SortByPrice()
    {
        return cars.OrderBy(car => car.Price).ToList();
    }

    public List<Car> SortByYear()
    {
        return cars.OrderBy(car => car.Year).ToList();
    }

    public List<Car> FindCarsBySpeedRange(int minSpeed, int maxSpeed)
    {
        return cars.Where(car => car.MaxSpeed >= minSpeed && car.MaxSpeed <= maxSpeed).ToList();
    }

    public List<Car> FindCarsByYear(int year)
    {
        return cars.Where(car => car.Year >= year).ToList();
    }

    public List<Car> FindCarsByLicensePlate(string licensePlate)
    {
        if (string.IsNullOrWhiteSpace(licensePlate))
            return new List<Car>();
            
        string searchPlate = licensePlate.Trim().ToUpper();
        return cars.Where(car => car.LicensePlate.Trim().ToUpper() == searchPlate).ToList();
    }

    public List<Car> FindCarsByType(string type)
    {
        return cars.Where(car => car.GetCarType().Equals(type, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    // public Car FindCar1(List<Car> test, int speed, int year)
    // {
    //     speed = 220;
    //     year = 1990;
    //     Car car = null;
    //     foreach (var value in test)
    //     {
    //         if (speed != speed && year != year)
    //         {
    //             
    //         }
    //         else
    //         {
    //             return car;
    //         }
    //     }
    //
    //     return  car;
    //     //return cars.Where(car => car.Equals(speed, year)).ToList();
    // }

    public void PrintCars()
    {
        if (cars.Count == 0)
        {
            Console.WriteLine("===В таксопарке нет автомобилей.===\n");
            return;
        }
    
        Console.WriteLine($"===Автомобили в таксопарке:=== \n");
        int counter = 1;
        foreach (var car in cars)
        {
            Console.WriteLine($"[{counter}]");
            car.DisplayInfo();
            Console.WriteLine();
            counter++;
        }
    
        Console.WriteLine("\n---------------------------------------");
        Console.WriteLine("Для более подробной информации выберите автомобиль в основном меню.\n");
        Console.WriteLine($"===Всего автомобилей в таксопарке: {cars.Count} ===\n");
    }
}