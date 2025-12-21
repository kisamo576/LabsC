using System.Text;
using System.Xml.Serialization;

namespace ConsoleApp1.Lab3;

public class Sentence
{
    [XmlIgnore]
    public List<Token> Tokens {get;}

    [XmlElement("word")]
    public List<string> XmlWords
    {
        get
        {
            return Tokens.OfType<Word>().Select(w => w.Value).ToList();
        }
    }

    public Sentence()
    {
        Tokens = new List<Token>();
    }

    public Sentence(List<Token> tokens)
    {
        if (tokens == null || tokens.Count == 0)
            throw new ArgumentException("Предложение должно содержать хотя бы один токен");

        foreach (var t in tokens)
        {
            if (!(t is Word || t is Punctuation))
                throw new ArgumentException("Sentence может содержать только Word или Punctuation");
        }
        
        
        for (int i = 0; i < tokens.Count - 2; i++)
        {
            string first = tokens[i].Value;
            string last = tokens[i+1].Value;
            string third = tokens[i+2].Value;
            
            if (first == "." && last == "." && third == ".")
            {
                throw new ArgumentException("Многоточие должно быть записано как один токен, а не тремя отдельными точками!");
            }
        }
        
        int sentencEndCount = 0;

        foreach (var t in tokens)
        {
            if (t.Value == "." || t.Value == "!" || t.Value == "?")
            {
                sentencEndCount++;
            }
            
            if (sentencEndCount > 1)
            {
                throw new ArgumentException("В списке может хранится только одно предложение!");
            }
        }

        for (int i = 0; i < tokens.Count - 1; i++)
        {
            string first = tokens[i].Value;
            string last = tokens[i + 1].Value;

            if ((first == "." && last == ".") ||
                (first == "," && last == ",") ||
                (first == ":" && last == ":") ||
                (first == ";" && last == ";"))
            {
                if (first == "." && last == ".")
                {
                    continue;
                }
                
                throw new ArgumentException("Sentence не может заканчиваться на 2 и более одинаковых знака препинания подряд!");
            }
        }
        Tokens = new List<Token>(tokens);
    }

    public void Add(Token token)
    {
        if (token is Word || token is Punctuation)
            Tokens.Add(token);
        else
            throw new InvalidOperationException("Нельзя добавлять этот тип токена");
    }

    public bool IsQuestion()
    {
        foreach (var token in Tokens)
        {
            if (token is Punctuation p && p.Value == "?")
                return true;
        }
        return false;
    }

    public List<Word> GetWords()
    {
        var words = new List<Word>();
        foreach (var token in Tokens)
        {
            if (token is Word word)
                words.Add(word);
        }
        return words;
    }
    
    public override string ToString()
    {
        var sb = new StringBuilder();
        Token lastToken = null;

        foreach (var token in Tokens)
        {
            if (token is Word word)
            {
                if (lastToken is Word || (lastToken is Punctuation p && p.Value == ")"))
                    sb.Append(" ");

                sb.Append(word.Value);
            }
            else if (token is Punctuation p)
            {
                if (p.Value == "(" && lastToken is Word)
                    sb.Append(" ");

                sb.Append(p.Value);
                
                if (p.Value == ")")
                    sb.Append(" ");
            }

            lastToken = token;
        }

        return sb.ToString().Replace("  ", " ").Trim();
    }


    public string Value => ToString(); 
    }