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
        
        cars.Add(car);
    }

    public void RemoveCar(string licensePlate)
    {
        for (int i = 0; i < cars.Count; i++)
        {
            if (cars[i].LicensePlate == licensePlate)
            {
                cars.RemoveAt(i);
                return;
            }
        }
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
        return cars.Where(car => car.LicensePlate == licensePlate).ToList();
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
        foreach (var car in cars)
        {
            car.DisplayInfo();
            Console.WriteLine();
        }
        Console.WriteLine($"===Всего автомобилей в таксопарке: {cars.Count} ===\n");
    }
}