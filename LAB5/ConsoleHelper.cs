namespace LAB5;

public static class ConsoleHelper
{
    public static void Wait()
    {
        Console.WriteLine("\nНажмите любую клавишу...");
        Console.ReadKey();
    }
    
    public static string ReadString(string prompt, bool allowEmpty = false)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();
            
            if (!string.IsNullOrWhiteSpace(input) || allowEmpty)
                return input?.Trim() ?? string.Empty;
                
            ShowError("Поле не может быть пустым!");
        }
    }
    
    public static int ReadInt(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out int value) && value >= min && value <= max)
                return value;
                
            ShowError($"Введите число от {min} до {max}!");
        }
    }
    
    public static double ReadDouble(string prompt, double min, double max)
    {
        while (true)
        {
            Console.Write(prompt);
            if (double.TryParse(Console.ReadLine(), out double value) && value >= min && value <= max)
                return value;
                
            ShowError($"Введите число от {min} до {max}!");
        }
    }
    
    public static bool ReadBool(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine()?.ToLower();
            
            if (input == "да" || input == "yes" || input == "1")
                return true;
            if (input == "нет" || input == "no" || input == "0")
                return false;
                
            ShowError("Введите 'да' или 'нет'!");
        }
    }
    
    public static void ShowError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Ошибка: {message}");
        Console.ResetColor();
    }
    
    public static void ShowSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }
    
    public static void ShowWarning(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(message);
        Console.ResetColor();
    }
    
    public static void ShowHeader(string title)
    {
        Console.Clear();
        Console.WriteLine($"=== {title} ===");
        Console.WriteLine();
    }
}