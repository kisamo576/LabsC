namespace LAB5;

public class TaxiParkManager
{
    private TaxiPark taxiPark;
    
    public TaxiParkManager()
    {
        taxiPark = TaxiPark.InitializePark();
    }
    
    public void ShowCars()
    {
        ConsoleHelper.ShowHeader("ВСЕ АВТОМОБИЛИ");
        taxiPark.PrintCars();
        ConsoleHelper.Wait();
    }
    
    public void AddCar()
    {
        ConsoleHelper.ShowHeader("ДОБАВИТЬ АВТОМОБИЛЬ");
        
        try
        {
            Console.WriteLine("Выберите тип автомобиля:");
            Console.WriteLine("1. Эконом-класс");
            Console.WriteLine("2. Бизнес-класс");
            Console.WriteLine("3. Внедорожник");
            Console.Write("\nВыбор: ");
            
            string typeChoice = Console.ReadLine();
            
            if (typeChoice != "1" && typeChoice != "2" && typeChoice != "3")
            {
                ConsoleHelper.ShowError("Неверный тип автомобиля! Допустимо: 1, 2 или 3.");
                ConsoleHelper.Wait();
                return;
            }
            
            string brand = ConsoleHelper.ReadString("Марка: ");
            string model = ConsoleHelper.ReadString("Модель: ");
            string licensePlate = ConsoleHelper.ReadString("Номер (например, А123ВС): ");
            
            var existingCars = taxiPark.FindCarsByLicensePlate(licensePlate);
            if (existingCars.Count > 0)
            {
                ConsoleHelper.ShowError($"Автомобиль с номером {licensePlate} уже существует!");
                ConsoleHelper.Wait();
                return;
            }
            
            double price = ConsoleHelper.ReadDouble("Цена (например, 10000): ", 0.01, 1000000);
            int maxSpeed = ConsoleHelper.ReadInt("Макс. скорость (км/ч): ", 1, 400);
            double fuelFlow = ConsoleHelper.ReadDouble("Расход топлива (л/100км): ", 0.1, 30);
            int year = ConsoleHelper.ReadInt("Год выпуска: ", 1900, DateTime.Now.Year + 1);
            string color = ConsoleHelper.ReadString("Цвет: ");
            
            Car newCar = null;
            
            switch (typeChoice)
            {
                case "1":
                    bool hasAC = ConsoleHelper.ReadBool("Есть кондиционер?");
                    newCar = new EconomyCar(brand, model, licensePlate, price, maxSpeed, 
                        fuelFlow, year, color, hasAC);
                    break;
                    
                case "2":
                    bool leather = ConsoleHelper.ReadBool("Кожаные сиденья?");
                    bool panorama = ConsoleHelper.ReadBool("Панорамная крыша?");
                    bool massage = ConsoleHelper.ReadBool("Массажные кресла?");
                    
                    newCar = new BusinessCar(brand, model, licensePlate, price, maxSpeed, 
                        fuelFlow, year, color, leather, panorama, massage);
                    break;
                    
                case "3":
                    bool fourWD = ConsoleHelper.ReadBool("Полный привод?");
                    int clearance = ConsoleHelper.ReadInt("Клиренс (мм, 100-400): ", 100, 400);
                    int seats = ConsoleHelper.ReadInt("Количество мест (2-9): ", 2, 9);
                    
                    newCar = new SUV(brand, model, licensePlate, price, maxSpeed, 
                        fuelFlow, year, color, fourWD, clearance, seats);
                    break;
            }

            if (newCar != null)
            {
                taxiPark.AddCar(newCar);
                ConsoleHelper.ShowSuccess($"\nАвтомобиль {brand} {model} успешно добавлен!");
            }
        }
        catch (Exception ex)
        {
            ConsoleHelper.ShowError($"\nОшибка при добавлении автомобиля: {ex.Message}");
        }
        
        ConsoleHelper.Wait();
    }
    
    public void RemoveCar()
    {
        ConsoleHelper.ShowHeader("УДАЛИТЬ АВТОМОБИЛЬ");
    
        List<Car> cars = taxiPark.GetCars();
        if (cars.Count == 0)
        {
            ConsoleHelper.ShowWarning("Нет автомобилей для удаления.");
            ConsoleHelper.Wait();
            return;
        }
        
        Console.WriteLine("Текущие автомобили:");
        for (int i = 0; i < cars.Count; i++)
        {
            Console.WriteLine($"{i+1}. {cars[i].LicensePlate} - {cars[i].Brand} {cars[i].Model}");
        }
        
        int choice = ConsoleHelper.ReadInt("\nВыберите номер автомобиля для удаления: ", 1, cars.Count);
        string plateToRemove = cars[choice - 1].LicensePlate;
        
        ConsoleHelper.ShowWarning($"Вы уверены, что хотите удалить {cars[choice-1].Brand} {cars[choice-1].Model} ({plateToRemove})?");
    
        if (ConsoleHelper.ReadBool("Подтвердите удаление (да/нет): "))
        {
            bool isRemoved = taxiPark.RemoveCar(plateToRemove);
        
            if (isRemoved)
            {
                ConsoleHelper.ShowSuccess($"Автомобиль {plateToRemove} успешно удалён!");
            }
            else
            {
                ConsoleHelper.ShowError($"Не удалось удалить автомобиль {plateToRemove}");
            }
        }
        else
        {
            ConsoleHelper.ShowWarning("Удаление отменено.");
        }
    
        ConsoleHelper.Wait();
    }
    
    public void ShowTotalPrice()
    {
        ConsoleHelper.ShowHeader("ОБЩАЯ СТОИМОСТЬ");
        
        double total = taxiPark.CalculateTotalPrice();
        int count = taxiPark.GetCars().Count;
        
        Console.WriteLine($"Автомобилей: {count} шт.");
        Console.WriteLine($"Общая стоимость: {total:F2}");
        
        if (count > 0)
        {
            Console.WriteLine($"Средняя цена: {total/count:F2}");
        }
        
        ConsoleHelper.Wait();
    }
    
    public void SortCars()
    {
        ConsoleHelper.ShowHeader("СОРТИРОВКА");
        Console.WriteLine("1. По расходу топлива");
        Console.WriteLine("2. По цене");
        Console.WriteLine("3. По году");
        Console.Write("\nВыбор: ");
        
        string choice = Console.ReadLine();
        List<Car> sorted;
        
        switch (choice)
        {
            case "1": 
                sorted = taxiPark.SortByFuelFlow();
                Console.WriteLine("\nСортировка по расходу:");
                break;
            case "2":
                sorted = taxiPark.SortByPrice();
                Console.WriteLine("\nСортировка по цене:");
                break;
            case "3": 
                sorted = taxiPark.SortByYear();
                Console.WriteLine("\nСортировка по году:");
                break;
            default: 
                ConsoleHelper.ShowWarning("Отмена.");
                ConsoleHelper.Wait();
                return;
        }
        
        if (sorted.Count == 0)
        {
            ConsoleHelper.ShowWarning("Нет автомобилей для сортировки.");
        }
        else
        {
            foreach (var car in sorted)
            {
                Console.WriteLine($"- {car.GetInfo()}");
            }
        }
        
        ConsoleHelper.Wait();
    }
    
    public void FindCars()
    {
        ConsoleHelper.ShowHeader("ПОИСК");
        Console.WriteLine("1. По скорости");
        Console.WriteLine("2. По году");
        Console.WriteLine("3. По типу");
        Console.WriteLine("4. По номеру");
        Console.Write("\nВыбор: ");
        
        string choice = Console.ReadLine();
        List<Car> found;
        
        switch (choice)
        {
            case "1":
                int min = ConsoleHelper.ReadInt("От скорости: ", 0, 400);
                int max = ConsoleHelper.ReadInt("До скорости: ", min, 400);
                found = taxiPark.FindCarsBySpeedRange(min, max);
                Console.WriteLine($"\nНайдено {found.Count} автомобилей:");
                break;
                
            case "2":
                int year = ConsoleHelper.ReadInt("Год от: ", 1900, DateTime.Now.Year + 1);
                found = taxiPark.FindCarsByYear(year);
                Console.WriteLine($"\nНайдено {found.Count} автомобилей:");
                break;
                
            case "3":
                Console.WriteLine("\nДоступные типы:");
                Console.WriteLine("1. Эконом");
                Console.WriteLine("2. Бизнес");
                Console.WriteLine("3. Внедорожник");
                Console.Write("Выберите тип (1-3): ");
    
                string typeChoice = Console.ReadLine();
    
                string searchType = "";
                string typeName = "";
    
                switch (typeChoice)
                {
                    case "1":
                        searchType = "Эконом";
                        typeName = "Эконом-класс";
                        break;
                    case "2":
                        searchType = "Бизнес";
                        typeName = "Бизнес-класс";
                        break;
                    case "3":
                        searchType = "Внедорожник";
                        typeName = "Внедорожник";
                        break;
                    default:
                        ConsoleHelper.ShowError("Неверный выбор!");
                        ConsoleHelper.Wait();
                        return;
                }
    
                found = taxiPark.FindCarsByType(searchType);
                Console.WriteLine($"\nНайдено {found.Count} автомобилей {typeName}:");
                break;
            case "4":
                string plate = ConsoleHelper.ReadString("Номер: ", true);
                found = taxiPark.FindCarsByLicensePlate(plate);
                Console.WriteLine($"\nНайдено {found.Count} автомобилей:");
                break;
                
            default:
                ConsoleHelper.ShowWarning("Отмена.");
                ConsoleHelper.Wait();
                return;
        }
        
        if (found.Count == 0)
        {
            ConsoleHelper.ShowWarning("Автомобили не найдены.");
        }
        else
        {
            foreach (var car in found)
            {
                Console.WriteLine($"- {car.GetInfo()}");
            }
        }
        
        ConsoleHelper.Wait();
    }
    
    public void ShowStats()
    {
        ConsoleHelper.ShowHeader("СТАТИСТИКА");
        
        var cars = taxiPark.GetCars();
        
        Console.WriteLine($"Всего автомобилей: {cars.Count}");
        
        if (cars.Count > 0)
        {
            var types = cars.GroupBy(c => c.GetCarType());
            Console.WriteLine("\nПо типам:");
            foreach (var type in types)
            {
                Console.WriteLine($"  {type.Key}: {type.Count()} шт.");
            }
            
            double total = taxiPark.CalculateTotalPrice();
            Console.WriteLine($"\nОбщая стоимость: {total:F2}");
            Console.WriteLine($"Средняя цена: {total/cars.Count:F2}");
            
            int minYear = cars.Min(c => c.Year);
            int maxYear = cars.Max(c => c.Year);
            Console.WriteLine($"\nГоды: от {minYear} до {maxYear}");
        }
        
        ConsoleHelper.Wait();
    }
}