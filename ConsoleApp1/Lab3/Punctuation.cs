namespace ConsoleApp1.Lab3;

public class Punctuation  : Token
{
    public string Value {get; set;}

    public Punctuation(string value)
    {
        Value = value;
    }

    public Punctuation()
    {
        
    }
    
    public override string ToString() => Value;
}