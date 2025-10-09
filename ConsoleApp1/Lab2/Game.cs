namespace ConsoleApp1.Lab2;

class Game
{
    public string inputFilePath;
    public string outputFilePath;
    public int size;
    public Player cat;
    public Player mouse;
    public bool gameEnded;

    public Game(string input, string output)
    {
        this.inputFilePath = input; 
        this.outputFilePath = output;
        cat = new Player("Cat");
        mouse = new Player("Mouse");
        gameEnded = false;
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
            while ((line = reader.ReadLine()) != null && !gameEnded)
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

        using (StreamWriter writer = new StreamWriter(outputFilePath, true))
        {
            writer.WriteLine("-------------------\n\n");
            writer.WriteLine("Distance traveled:   Mouse   Cat");
            writer.WriteLine($"                     {mouse.distanceTraveled}      {cat.distanceTraveled}\n");

            if (gameEnded)
            {
                writer.WriteLine($"Mouse caught at: {mouse.location}");
                writer.WriteLine($"Distance in the end = {Math.Abs(mouse.location - cat.location)}");
            }
            else
            {
                writer.WriteLine("Mouse evaded Cat");
                writer.WriteLine($"Distance in the end = {Math.Abs(mouse.location - cat.location)}");
            }
        }

        Console.WriteLine("Результат записан в файл PursuitLog");

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

        if (cat.state == State.Playing && mouse.state == State.Playing)
        {
            if (cat.location == mouse.location)
            {
                mouse.state = State.Loser;
                cat.state = State.Winner;
                gameEnded = true;
            }
        }
    }

    int count = 0;

       public void DoPrintCommand()
        {
            using (StreamWriter writer = new StreamWriter(outputFilePath, true))
            {

                if (cat.state == State.NotInGame)
                {
                    writer.WriteLine("??" + "     " + mouse.location);
                    count++;
                }
                else if (mouse.state == State.NotInGame)
                {
                    writer.WriteLine(cat.location + "     " + "??");
                    count++;
                }

                else
                {
                    int distance = Math.Abs(mouse.location - cat.location);
                    writer.WriteLine(cat.location + "     " + mouse.location + "       " + distance);
                }

                if (Math.Abs(mouse.location - cat.location) == 1)
                {
                    writer.WriteLine("Distance between = 1");
                }


                if (count >= 4)
                {
                    writer.WriteLine("Координаты не инициализированы");
                    count = 0;
                }

            }

        }
    }