namespace ConsoleApp1.Lab3;

public static class TextProcessor
{
    public static List<Sentence> SortByWordCount(Text text)
    {
        var result = new List<Sentence>(text.Sentences);
        result.Sort((a, b) =>
            a.Tokens.OfType<Word>().Count().CompareTo(b.Tokens.OfType<Word>().Count()));
        return result;
    }
    
    public static List<Sentence> SortByLength(Text text)
    {
        var result = new List<Sentence>(text.Sentences);
        result.Sort((a, b) => a.Value.Length.CompareTo(b.Value.Length));
        return result;
    }
    
    public static List<string> FindWordsInQuestions(Text text, int length)
    {
        var found = new List<string>();

        foreach (var sentence in text.Sentences)
        {
            bool isQuestion = false;
            foreach (var token in sentence.Tokens)
            {
                if (token is Punctuation p && p.Value == "?")
                {
                    isQuestion = true;
                    break;
                }
            }

            if (!isQuestion)
                continue;
            
            foreach (var token in sentence.Tokens)
            {
                if (token is Word w && w.Value.Length == length)
                {
                    if (!found.Contains(w.Value, StringComparer.OrdinalIgnoreCase))
                        found.Add(w.Value);
                }
            }
        }

        return found;
    }
    
    public static void RemoveConsonantWords(Text text, int length)
    {
        string consonants = "бвгджзйклмнпрстфхцчшщbcdfghjklmnpqrstvwxyz";

        foreach (var sentence in text.Sentences)
        {
            for (int i = 0; i < sentence.Tokens.Count; i++)
            {
                if (sentence.Tokens[i] is Word w &&
                    w.Value.Length == length &&
                    consonants.Contains(char.ToLower(w.Value[0])))
                {
                    sentence.Tokens.RemoveAt(i);
                    i--;
                }
            }
        }
    }
    
    public static void ReplaceWordsInSentence(Sentence sentence, int length, string replacement)
    {
        for (int i = 0; i < sentence.Tokens.Count; i++)
        {
            if (sentence.Tokens[i] is Word w && w.Value.Length == length)
            {
                sentence.Tokens[i] = new Word (replacement);
            }
        }
    }
    
    public static void RemoveStopWords(Text text, IEnumerable<string> stopWords)
    {
        var stopList = new List<string>();
        foreach (var s in stopWords)
            stopList.Add(s.Trim().ToLower());

        foreach (var sentence in text.Sentences)
        {
            for (int i = 0; i < sentence.Tokens.Count; i++)
            {
                if (sentence.Tokens[i] is Word w &&
                    stopList.Contains(w.Value.ToLower()))
                {
                    sentence.Tokens.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
