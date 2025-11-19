namespace ConsoleApp1.Lab3;

public abstract class Token
{
    public string Value { get; set; }
    public override string ToString () => Value;
    
}