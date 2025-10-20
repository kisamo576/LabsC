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
        this.state = State.NotInGame;
        this.location = -1;
    }

    public void SetInitialPosition(int position, int boardSize)
    {
        if (this.state == State.NotInGame)
        {
            this.location = NormalizePosition(position, boardSize);
            this.state = State.Playing;
        }
    }

    public void Move(int steps, int boardSize)
    {
        if (this.state == State.Playing)
        {
            int newLocation = this.location + steps;
            this.location = NormalizePosition(newLocation, boardSize);
            this.distanceTraveled += Math.Abs(steps);
        }
    }

    private int NormalizePosition(int position, int size)
    {
        int normalized = ((position - 1) % size + size) % size + 1;
        return normalized;
    }
}
