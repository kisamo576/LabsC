namespace ConsoleApp1.Lab2;

enum State
{
    Winner,
    Loser,
    Playing,
    NotInGame,
}

class Player
{
    public string name;
    public State state = State.NotInGame;
    public int location;
    public int distanceTraveled;
    
    public Player(string name)
    {
        this.name = name;
        this.location = -1;
    }
    
    public void Move(int steps, int size)
    {
        
    }
}