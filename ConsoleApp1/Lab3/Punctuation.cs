namespace ConsoleApp1.Lab3;

public class Punctuation  : Token
{
    public Punctuation(string value)
    {
        if (string.IsNullOrEmpty(value) || value.Length > 3)
            throw new ArgumentException("Punctuation должен быть одним знаком или многоточием");
        Value = value;
    }

    public bool IsSentenceEnding()
    {
        return Value == "." || Value == "!" ||  Value == "?" || Value == "...";
    }
}