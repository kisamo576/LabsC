using System.Text;
using System.Collections.Generic;

namespace ConsoleApp1.Lab3;

public  class TextParser
{
    public static Text Parse(string input)
    {
        var text = new Text();
        text.Fulltext = input;
        text.Lines = input.Split('\n').ToList();

        var currentSentence = new Sentence();
        var currentWord = new StringBuilder();

        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];
            
            if (char.IsLetterOrDigit(c) || c == '_' || c == '#' || c == '+' || c == '-' || c == '(' || c == ')')
            {
                currentWord.Append(c);
            }
            else
            {
                if (currentWord.Length > 0)
                {
                    currentSentence.Add(new Word(currentWord.ToString()));
                    currentWord.Clear();
                }

                if (char.IsWhiteSpace(c))
                {
                    currentSentence.Add(new Punctuation(" "));
                    continue;
                }

                var punctuation = new Punctuation(c.ToString());
                currentSentence.Add(punctuation);

                if (punctuation.IsSentenceEnding())
                {
                    if (currentSentence.Tokens.Count > 0)
                        text.Add(currentSentence);

                    currentSentence = new Sentence();
                }
            }
        }

        if (currentWord.Length > 0)
            currentSentence.Add(new Word(currentWord.ToString()));

        if (currentSentence.Tokens.Count > 0)
            text.Add(currentSentence);

        return text;
    }
}
