using System.Xml.Serialization;

namespace ConsoleApp1.Lab3;

public class Punctuation  : Token
{
    public List<Token> PunctuationTokens { get; set; }
    
    public Punctuation(string value)
    {
        Value = value;
        PunctuationTokens = new List<Token>();
    }

    public override string ToString()
    {
        string punctuation = "";
        foreach (Token token in PunctuationTokens)
        {
            punctuation= base.ToString() + token.ToString();
        } 
        return punctuation;
    }
}