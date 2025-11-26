using System.Text;

namespace ConsoleApp1.Lab3;

public class Sentence
{
    public List<Token> Tokens {get; set;} =  new List<Token>();
    public override string ToString() {
        
        var sb = new StringBuilder();

        for (int i = 0; i < Tokens.Count; i++)
        {
            var token = Tokens[i];

            if (token is Word w)
            {
                if (sb.Length > 0)
                    sb.Append(" ");

                sb.Append(w.Value);
            }
            else if (token is Punctuation p)
            {
                if (".!?:;-".Contains(p.Value))
                {
                    sb.Append(p.Value);
                    
                    if (p.Value == ",")
                        sb.Append(" ");
                }
                else
                {
                    sb.Append(p.Value);
                }
            }
        }

        return sb.ToString();
    }
    public string Value => ToString(); 
    }