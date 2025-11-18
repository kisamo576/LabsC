using System.Text;

namespace ConsoleApp1.Lab3;

public static class TextParser
{
    public static Text Parse(string input)
    {
        var text = new Text();
        var currentSentence = new Sentence();
        var currentWord = new StringBuilder();

        foreach (char c in input)
        {
            if (char.IsLetterOrDigit(c))
            {
                currentWord.Append(c);
            }
            else
            {
                if (currentWord.Length > 0)
                {
                    currentSentence.Tokens.Add(new Word (currentWord.ToString()));
                    currentWord.Clear();
                }
            }
            
            if (char.IsPunctuation(c))
            {
                    currentSentence.Tokens.Add(new Punctuation(c.ToString()));

                if (IsSentenceEndingPunctuation(c))
                {
                        text.Sentences.Add(currentSentence);
                        currentSentence = new Sentence();
                    
                }
            }
        }

        if (currentSentence.Tokens.Count > 0)
            text.Sentences.Add(currentSentence);
        
        return text;
    }

    private static bool IsSentenceEndingPunctuation(char c)
    {
        return c == '.' || c == '!' || c == '?' || c == '…';
    }
}