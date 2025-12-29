namespace LAB5;

class Program
{
    static TaxiParkManager manager = new TaxiParkManager();
    
    static void Main(string[] args)
    {
        if (!ShowAuthMenu())
        {
            Console.WriteLine("Выход из программы...");
            return;
        }
        
        bool exit = false;

        while (!exit)
        {
            Console.Clear();
            
            var user = manager.CurrentUser;
            string roleInfo = user?.Role == "admin" ? " [АДМИН]" : " [ПОЛЬЗОВАТЕЛЬ]";
            
            Console.WriteLine($"=== ТАКСОПАРК{roleInfo} ===");
            Console.WriteLine($"Пользователь: {user?.Login}");
            Console.WriteLine("================================");
            Console.WriteLine("1. Показать все автомобили");
            Console.WriteLine("2. Добавить автомобиль");
            Console.WriteLine("3. Удалить автомобиль");
            Console.WriteLine("4. Общая стоимость парка");
            Console.WriteLine("5. Сортировать автомобили");
            Console.WriteLine("6. Найти автомобили");
            Console.WriteLine("7. Статистика");
            Console.WriteLine("8. Сменить пользователя");
            Console.WriteLine("0. Выход");
            Console.WriteLine("================================");

            
            Console.Write("\nВыбор: ");
            string? choice = Console.ReadLine();
            
            switch (choice)
            {
                case "1": manager.ShowCars(); break;
                case "2": manager.AddCar(); break;
                case "3": manager.RemoveCar(); break;
                case "4": manager.ShowTotalPrice(); break;
                case "5": manager.SortCars(); break;
                case "6": manager.FindCars(); break;
                case "7": manager.ShowStats(); break;
                case "8": 
                    manager.Logout();
                    if (!ShowAuthMenu())
                        exit = true;
                    break;
                case "0": exit = true; break;
                default: 
                    ConsoleHelper.ShowError("Неверный выбор!");
                    ConsoleHelper.Wait();
                    break;
            }
        }
        
        Console.WriteLine("\nВыход из программы...");
    }
    
    static bool ShowAuthMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== АВТОРИЗАЦИЯ ===");
            Console.WriteLine("1. Войти в систему");
            Console.WriteLine("2. Зарегистрироваться");
            Console.WriteLine("0. Выход из программы");
            
            Console.Write("\nВыбор: ");
            string choice = Console.ReadLine();
            
            switch (choice)
            {
                case "1":
                    Console.Write("\nЛогин: ");
                    string login = Console.ReadLine();
                    Console.Write("Пароль: ");
                    string password = Console.ReadLine();
                    
                    if (manager.Login(login, password))
                    {
                        ConsoleHelper.ShowSuccess($"\nДобро пожаловать, {manager.CurrentUser?.Login}!");
                        ConsoleHelper.ShowSuccess($"Ваша роль: {manager.CurrentUser?.Role}");
                        ConsoleHelper.Wait();
                        return true;
                    }
                    else
                    {
                        ConsoleHelper.ShowError("\nНеверный логин или пароль!");
                        ConsoleHelper.Wait();
                    }
                    break;
                     
                case "2":
                    Console.Write("\nЛогин: ");
                    string newLogin = Console.ReadLine();
                    Console.Write("Пароль: ");
                    string newPassword = Console.ReadLine();
                    
                    if (manager.Register(newLogin, newPassword))
                    {
                        ConsoleHelper.ShowSuccess("\nРегистрация успешна! Теперь войдите в систему.");
                    }
                    else
                    {
                        ConsoleHelper.ShowError("\nЛогин уже занят!");
                    }
                    ConsoleHelper.Wait();
                    break;
                    
                case "0":
                    return false;
                    
                default:
                    ConsoleHelper.ShowError("Неверный выбор!");
                    ConsoleHelper.Wait();
                    break;
            }
        }
    }
}