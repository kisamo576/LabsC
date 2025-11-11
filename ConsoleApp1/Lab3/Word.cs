namespace ConsoleApp1.Lab3;

public class Word : Token
{
    public string Value { get; set; }

    public Word(string value)
    {
        Value = value;
    }

    public Word()
    {
        
    }
    
    public override string ToString() => Value;
}