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
        
    }
    
    public void DoCommand(char command, int steps)
    {
        
    }
    
    public void DoPrintCommand()
    {
        
    }
}