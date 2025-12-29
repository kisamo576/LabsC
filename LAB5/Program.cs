namespace LAB5;

class Program
{
    static TaxiParkManager manager = new TaxiParkManager();
    
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
                case "0": exit = true; break;
                default: 
                    ConsoleHelper.ShowError("Неверный выбор!");
                    ConsoleHelper.Wait();
                    break;
            }
        }
        
        Console.WriteLine("\nВыход из программы...");
    }
}