using System.Text;

namespace ConsoleApp1.Lab3;

public class Word : Token
{
    public Word(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Слово не может быть пустым");

        if (value.Any(char.IsWhiteSpace))
            throw new ArgumentException("Word не может содержать пробелы");

        Value = CleanWord(value);
    }

    public string CleanWord(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        var cleaned = new StringBuilder();
        foreach (char c in input)
        {
            if (char.IsLetterOrDigit(c) || c == '_' || c == '#' || c == '+' || c == '-' || c == '(' || c == ')' || c == '@')
                cleaned.Append(c);
        }
        return cleaned.ToString();
    }



    public bool StartsWithConsonant()
    {
        if (string.IsNullOrEmpty(Value))
            return false;
        
        char firstChar = char.ToLower(Value[0]);
        string consonant = "бвгджзйклмнпрстфхцчшщbcdfghjklmnpqrstvwxyz";
        return consonant.Contains(firstChar);
    }

    public bool IsStopWord(string[] stopWords)
    {
        return stopWords.Contains(Value.ToLower());
    }

}