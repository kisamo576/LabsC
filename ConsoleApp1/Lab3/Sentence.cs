using System.Text;

namespace ConsoleApp1.Lab3;

public class Sentence : Token
{
    public List<Token> Tokens {get; set;} =  new List<Token>();

    public new string Value
    {
        get
        {
            var result = new StringBuilder();
            foreach (var token in Tokens)
            {
                if (token is Word)
                {
                    if (result.Length > 0 && !char.IsLetterOrDigit(result[result.Length - 1]))
                        result.Append(' ');
                    result.Append(token.Value);
                    
                }
                else
                {
                    result.Append(token.Value);
                }
            }
            return result.ToString();
        }
        set { }
    }
}