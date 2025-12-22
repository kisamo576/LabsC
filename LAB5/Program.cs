namespace LAB5;

class Program
{
    static TaxiPark taxiPark = InitializePark();
    
    static void Main(string[] args)
    {
        bool exit = false;

        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("=== ТАКСОПАРК - ГЛАВНОЕ МЕНЮ ===");
            Console.WriteLine("1. Показать все автомобили");
            Console.WriteLine("2. Добавить автомобиль");
            Console.WriteLine("3. Удалить автомобиль");
            Console.WriteLine("4. Общая стоимость парка");
            Console.WriteLine("5. Сортировать автомобили");
            Console.WriteLine("6. Найти автомобили");
            Console.WriteLine("7. Статистика");
            Console.WriteLine("0. Выход");
            Console.WriteLine("================================");

            
            Console.Write("\nВыбор: ");
            string choice = Console.ReadLine();
            
            switch (choice)
            {
                case "1": ShowCars(); break;
                case "2": AddCar(); break;
                case "3": RemoveCar(); break;
                case "4": TotalPrice(); break;
                case "5": SortCars(); break;
                case "6": FindCars(); break;
                case "7": ShowStats(); break;
                case "0": exit = true; break;
                default: 
                    Console.WriteLine("Неверный выбор!");
                    Wait();
                    break;
            }
        }
        
        Console.WriteLine("\nВыход из программы...");
    }

    static TaxiPark InitializePark()
    {
        TaxiPark park = new TaxiPark();
        
        park.AddCar(new EconomyCar("Volkswagen", "Polo", "ABC123", 10000, 180, 7.0, 2015, "Белый", true));
        park.AddCar(new EconomyCar("Renault", "Logan", "DFG321", 9000, 160, 6.5, 2018, "Синий", true));
        park.AddCar(new EconomyCar("Skoda", "Rapid", "QWE761", 11000, 190, 8.0, 2019, "Серый", true));

        park.AddCar(new BusinessCar("Mercedes", "E-Class", "MVP333", 55000, 220, 8.2, 2021, "Чёрный", true, true, true));
        park.AddCar(new BusinessCar("BMW", "7 Series", "WOW777", 62000, 240, 9.1, 2022, "Синий", true, false, true));

        park.AddCar(new SUV("Toyota", "Land Cruiser", "BIG999", 60000, 190, 12.5, 2019, "Чёрный", true, 225, 7));
        park.AddCar(new SUV("Nissan", "X-Trail", "JAP525", 32000, 190, 9.8, 2020, "Красный", false, 210, 5));
        
        return park;
    }

    static void ShowCars()
    {
        Console.Clear();
        taxiPark.PrintCars();
        Wait();
    }
      static void AddCar()
    {
    Console.Clear();
    Console.WriteLine("=== ДОБАВИТЬ АВТОМОБИЛЬ ===\n");
    
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
            Console.WriteLine("Неверный тип автомобиля! Допустимо: 1, 2 или 3.");
            Wait();
            return;
        }
        
        string brand;
        while (true)
        {
            Console.Write("\nМарка: ");
            brand = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(brand))
                break;
            Console.WriteLine("Марка не может быть пустой!");
        }
        
        string model;
        while (true)
        {
            Console.Write("Модель: ");
            model = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(model))
                break;
            Console.WriteLine("Модель не может быть пустой!");
        }
        
        string licensePlate;
        while (true)
        {
            Console.Write("Номер (например, А123ВС): ");
            licensePlate = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(licensePlate))
                break;
            Console.WriteLine("Номер не может быть пустым!");
        }
        
        var existingCars = taxiPark.FindCarsByLicensePlate(licensePlate);
        if (existingCars.Count > 0)
        {
            Console.WriteLine($"Автомобиль с номером {licensePlate} уже существует!");
            Wait();
            return;
        }
        
        double price;
        while (true)
        {
            Console.Write("Цена (например, 10000): ");
            if (double.TryParse(Console.ReadLine(), out price) && price > 0)
                break;
            Console.WriteLine("Введите корректную положительную цену!");
        }
        
        int maxSpeed;
        while (true)
        {
            Console.Write("Макс. скорость (км/ч): ");
            if (int.TryParse(Console.ReadLine(), out maxSpeed) && maxSpeed > 0 && maxSpeed <= 400)
                break;
            Console.WriteLine("Введите корректную скорость (1-400 км/ч)!");
        }
        
        double fuelFlow;
        while (true)
        {
            Console.Write("Расход топлива (л/100км): ");
            if (double.TryParse(Console.ReadLine(), out fuelFlow) && fuelFlow > 0 && fuelFlow <= 30)
                break;
            Console.WriteLine("Введите корректный расход (0-30 л/100км)!");
        }
        
        int year;
        while (true)
        {
            Console.Write("Год выпуска: ");
            if (int.TryParse(Console.ReadLine(), out year) && year >= 1900 && year <= DateTime.Now.Year + 1)
                break;
            Console.WriteLine($"Введите корректный год (1900-{DateTime.Now.Year + 1})!");
        }
        
        
        string color;
        while (true)
        {
            Console.Write("Цвет: ");
            color = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(color))
                break;
            Console.WriteLine("Цвет не может быть пустым!");
        }
        
        Car newCar = null;
        
        switch (typeChoice)
        {
            case "1":
                Console.Write("Есть кондиционер? (да/нет): ");
                bool hasAC = Console.ReadLine().ToLower() == "да";
                
                newCar = new EconomyCar(brand, model, licensePlate, price, maxSpeed, 
                    fuelFlow, year, color, hasAC);
                break;
                
            case "2":
                Console.Write("Кожаные сиденья? (да/нет): ");
                bool leather = Console.ReadLine().ToLower() == "да";
                
                Console.Write("Панорамная крыша? (да/нет): ");
                bool panorama = Console.ReadLine().ToLower() == "да";
                
                Console.Write("Массажные кресла? (да/нет): ");
                bool massage = Console.ReadLine().ToLower() == "да";
                
                newCar = new BusinessCar(brand, model, licensePlate, price, maxSpeed, 
                    fuelFlow, year, color, leather, panorama, massage);
                break;
                
            case "3":
                Console.Write("Полный привод? (да/нет): ");
                bool fourWD = Console.ReadLine().ToLower() == "да";
                
                int clearance;
                while (true)
                {
                    Console.Write("Клиренс (мм, 100-400): ");
                    if (int.TryParse(Console.ReadLine(), out clearance) && clearance >= 100 && clearance <= 400)
                        break;
                    Console.WriteLine("Введите корректный клиренс (100-400 мм)!");
                }
                
                int seats;
                while (true)
                {
                    Console.Write("Количество мест (2-9): ");
                    if (int.TryParse(Console.ReadLine(), out seats) && seats >= 2 && seats <= 9)
                        break;
                    Console.WriteLine("Введите корректное количество мест (2-9)!");
                }
                
                newCar = new SUV(brand, model, licensePlate, price, maxSpeed, 
                    fuelFlow, year, color, fourWD, clearance, seats);
                break;
        }

        if (newCar != null)
        {
            taxiPark.AddCar(newCar);
            Console.WriteLine($"\nАвтомобиль {brand} {model} успешно добавлен!");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\nОшибка при добавлении автомобиля: {ex.Message}");
    }
    
    Wait();
    }

    static void RemoveCar()
    {
        Console.Clear();
        Console.WriteLine("=== УДАЛИТЬ АВТОМОБИЛЬ ===\n");
        
        
        List<Car> cars = taxiPark.GetCars();
        if (cars.Count == 0)
        {
            Console.WriteLine("Нет автомобилей для удаления.");
            Wait();
            return;
        }
        
        Console.WriteLine("Текущие автомобили:");
        for (int i = 0; i < cars.Count; i++)
        {
            Console.WriteLine($"{cars[i].LicensePlate} - {cars[i].Brand} {cars[i].Model}");
        }
        
        Console.Write("\nВведите номер автомобиля: ");
        string plate = Console.ReadLine();
        
        taxiPark.RemoveCar(plate);
        Console.WriteLine($"\nАвтомобиль {plate} удалён.");
        
        Wait();
    }

    static void TotalPrice()
    {
        Console.Clear();
        double total = taxiPark.CalculateTotalPrice();
        int count = taxiPark.GetCars().Count;
        
        Console.WriteLine("=== ОБЩАЯ СТОИМОСТЬ ===\n");
        Console.WriteLine($"Автомобилей: {count} шт.");
        Console.WriteLine($"Общая стоимость: {total}");
        
        if (count > 0)
        {
            Console.WriteLine($"Средняя цена: {total/count :F}");
        }
        
        Wait();
    }

    static void SortCars()
    {
        Console.Clear();
        Console.WriteLine("=== СОРТИРОВКА ===\n");
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
                Console.WriteLine("Отмена.");
                Wait();
                return;
        }
        
        foreach (var car in sorted)
        {
            Console.WriteLine($"- {car.GetInfo()}");
        }
        
        Wait();
    }

    static void FindCars()
    {
        Console.Clear();
        Console.WriteLine("=== ПОИСК ===\n");
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
                Console.Write("От скорости: ");
                int min = int.Parse(Console.ReadLine());
                Console.Write("До скорости: ");
                int max = int.Parse(Console.ReadLine());
                found = taxiPark.FindCarsBySpeedRange(min, max);
                Console.WriteLine($"\nНайдено {found.Count} автомобилей:");
                break;
                
            case "2":
                Console.Write("Год от: ");
                int year = int.Parse(Console.ReadLine());
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
                        Console.WriteLine("Неверный выбор!");
                        Wait();
                        return;
                }
    
                found = taxiPark.FindCarsByType(searchType);
                Console.WriteLine($"\nНайдено {found.Count} автомобилей {typeName}:");
                break;
            case "4":
                Console.Write("Номер: ");
                string plate = Console.ReadLine();
                found = taxiPark.FindCarsByLicensePlate(plate);
                Console.WriteLine($"\nНайдено {found.Count} автомобилей:");
                break;
                
            default:
                Console.WriteLine("Отмена.");
                Wait();
                return;
        }
        
        if (found.Count == 0)
        {
            Console.WriteLine("Автомобили не найдены.");
        }
        else
        {
            foreach (var car in found)
            {
                Console.WriteLine($"- {car.GetInfo()}");
            }
        }
        
        Wait();
    }

    static void ShowStats()
    {
        Console.Clear();
        var cars = taxiPark.GetCars();
        
        Console.WriteLine("=== СТАТИСТИКА ===\n");
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
            Console.WriteLine($"Средняя цена: {total/cars.Count :F2}");
            
            int minYear = cars.Min(c => c.Year);
            int maxYear = cars.Max(c => c.Year);
            Console.WriteLine($"\nГоды: от {minYear} до {maxYear}");
        }
        
        Wait();
    }

    static void Wait()
    {
        Console.WriteLine("\nНажмите любую клавишу...");
        Console.ReadKey();
    }
}