namespace ConsoleApp1.Lab3;

public class Punctuation  : Token
{
    public Punctuation() { }

    public Punctuation(string value)
    {
        Value = value;
    }
    
    public override string ToString() => Value;
}