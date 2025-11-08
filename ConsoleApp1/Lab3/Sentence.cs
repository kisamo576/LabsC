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
            for (int i = 0; i < Tokens.Count; i++) {
                var token = Tokens[i];
                if (token is Word)
                {
                    result.Append(token.Value);
                }
                else
                {
                    if (token.Value != " " && i > 0 && Tokens[i - 1].Value != " ")
                    {
                        result.Length--;
                    }
                    result.Append(token.Value);
                }
            }
            return result.ToString().Trim();
        }
    }
}