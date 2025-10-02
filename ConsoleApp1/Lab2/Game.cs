namespace ConsoleApp1.Lab2;

class Game
{
    public string inputFilePath;
    public string outputFilePath;
    public int size;
    public Player cat;
    public Player mouse;
    
    public Game(string input, string output)
    {
        this.inputFilePath = input;
        this.outputFilePath = output;
        cat = new Player("Cat");
        mouse = new Player("Mouse");
    }
    
    public void Run()
    {
        Console.WriteLine("Игра запущена");
        using (StreamWriter writer = new StreamWriter(outputFilePath))
        {
            writer.WriteLine("Cat and Mouse\n");
            writer.WriteLine("Cat Mouse  Distance");
            writer.WriteLine("-------------------");
        }
        
        using (StreamReader reader = new StreamReader(inputFilePath))
        {
            size = Convert.ToInt32(reader.ReadLine());
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] str = line.Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if (str[0] == "P")
                {
                    DoPrintCommand();
                }
                else
                {
                    DoCommand(Convert.ToChar(str[0]), Convert.ToInt32(str[1]));
                }
            }
        }
        
        Console.WriteLine("Обработка файла завершена");
    }
    
    public void DoCommand(char command, int steps)
    {
        switch (command)
        {
            case 'M': 
                mouse.Move(steps, size);
                break;
            case 'C': 
                cat.Move(steps, size);
                break;
        }
    }
    
    public void DoPrintCommand()
    {
        using (StreamWriter writer = new StreamWriter(outputFilePath, true))
        {
            if (cat.state == State.NotInGame)
            {
                writer.WriteLine("??" + "  " + mouse.location);
            }
            else if (mouse.state == State.NotInGame)
            {
                writer.WriteLine(cat.location + "  " + "??");
            }
            else
            {
                writer.WriteLine(cat.location + "  " + mouse.location);
            }
        }
    }
}