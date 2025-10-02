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
        if(this.location == -1)
        {
            if (steps < 0)
            {
                this.location = size + steps;
            }
            else
            {
                this.location = steps;
            }
            this.state = State.Playing;
        }
        else
        {
            this.location += steps;
            this.distanceTraveled += Math.Abs(steps);
            if(this.location < 0)
            {
                this.location += size;
            }
            else if(this.location > size)
            {
                this.location -= size;
            }
        }
    }
}