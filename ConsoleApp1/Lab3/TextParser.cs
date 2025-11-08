using System.Text;

namespace ConsoleApp1.Lab3;

public static class TextParser
{
    public static Text Parse(string input)
    {
        var text = new Text();
        var currentSentence = new Sentence();
        var currentWord = new StringBuilder();
        bool lastWasWord = false;

        foreach (char c in input)
        {
            if (char.IsLetterOrDigit(c))
            {
                currentWord.Append(c);
                lastWasWord = true;
            }
            else if (char.IsWhiteSpace(c))
            {
                if (currentWord.Length > 0)
                {
                    currentSentence.Tokens.Add(new Word {Value = currentWord.ToString()});
                    currentWord.Clear();
                    lastWasWord = true;
                }
                
                if (lastWasWord)
                {
                    currentSentence.Tokens.Add(new Punctuation { Value = " " });
                    lastWasWord = false;
                }
            }
            else if (char.IsPunctuation(c))
            {
                if (currentWord.Length > 0)
                {
                    currentSentence.Tokens.Add(new Word {Value = currentWord.ToString()});
                    currentWord.Clear();
                }
                   
                var punctuation = new Punctuation {Value = c.ToString() };
                currentSentence.Tokens.Add(punctuation);

                if (IsSentenceEndingPunctuation(c))
                {
                    if (currentSentence.Tokens.Count > 0)
                    {
                        text.sentences.Add(currentSentence);
                        currentSentence = new Sentence();
                    }
                }
                
                lastWasWord = false;
            }
        }
        
        if (currentWord.Length > 0)
            currentSentence.Tokens.Add(new Word {Value = currentWord.ToString()});

        if (currentSentence.Tokens.Count > 0)
            text.sentences.Add(currentSentence);
        
        return text;
    }

    private static bool IsSentenceEndingPunctuation(char c)
    {
        return c == '.' || c == '!' || c == '?' || c == '…';
    }
}