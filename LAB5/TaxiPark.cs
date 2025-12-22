namespace LAB5;

public class TaxiPark
{
    private List<Car> cars = new List<Car>();

    public void AddCar(Car car)
    {
        if (car == null)
        {
            throw new ArgumentNullException("car");
        }
        
        foreach (var existingCar in cars)
        {
            if (existingCar.LicensePlate.ToUpper() == car.LicensePlate.ToUpper())
            {
                throw new ArgumentException($"Автомобиль с номером {car.LicensePlate} уже существует!");
            }
        }
        
        cars.Add(car);
    }

    public void RemoveCar(string licensePlate)
    {
        if (string.IsNullOrWhiteSpace(licensePlate))
        {
            Console.WriteLine("Номер не может быть пустым!");
            return;
        }
        
        string originalPlate = licensePlate;
        licensePlate = licensePlate.Trim().ToUpper();
        
        for (int i = 0; i < cars.Count; i++)
        {
            if (cars[i].LicensePlate.Trim().ToUpper() == licensePlate)
            {
                string carInfo = cars[i].GetInfo();
                cars.RemoveAt(i);
                Console.WriteLine($"Удалён: {carInfo}");
                return;
            }
        }
        
        Console.WriteLine($"Автомобиль с номером '{originalPlate}' не найден.");
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
            Console.WriteLine($"[{counter}] {car.GetInfo()}");
            Console.WriteLine();
            counter++;
        }
        
        Console.WriteLine("\n---------------------------------------");
        Console.WriteLine("Для более подробной информации выберите автомобиль в основном меню.\n");
        Console.WriteLine($"===Всего автомобилей в таксопарке: {cars.Count} ===\n");
    }
}