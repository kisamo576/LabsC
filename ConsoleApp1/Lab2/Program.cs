namespace ConsoleApp1.Lab2;

class Program
{
    public static void Main(string[] args)
    {
        
        string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"../../../Lab2/files");

        
        Console.WriteLine("Выберите файл:\n" +
                          "1 1.ChaseData\n" +
                          "2 2.ChaseData\n" +
                          "3 3.ChaseData");
        int choice = Convert.ToInt32(Console.ReadLine());
        
        string input = "";
        switch (choice)
        {
            case 1:
                input = Path.Combine(basePath, "1.ChaseData.txt");
                break;
            case 2:
                input = Path.Combine(basePath, "2.ChaseData.txt");
                break;
            case 3:
                input = Path.Combine(basePath, "3.ChaseData.txt");
                break;
        }
        
        string output = "PursuitLog.txt";
        Game game = new Game(input, output);
        game.Run();
    }
}