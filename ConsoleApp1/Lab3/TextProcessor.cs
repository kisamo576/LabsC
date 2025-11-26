namespace ConsoleApp1.Lab3;

public static class TextProcessor
{
    public static List<Sentence> SortByWordCount(Text text)
    {
        var result = new List<Sentence>(text.Sentences);
        // result.Sort((a, b) =>
        //   a.Tokens.OfType<Word>().Count().CompareTo(b.Tokens.OfType<Word>().Count()));
         for (int i = 0; i < result.Count - 1; i++)
         {
             for (int j = 0; j < result.Count - 1 - i; j++)
             {
                 int countA = result[j].Tokens.OfType<Word>().Count();
                 int countB = result[j + 1].Tokens.OfType<Word>().Count();

                 if (countA > countB)
                 {
                     var temp = result[j];
                     result[j] = result[j + 1];
                     result[j + 1] = temp;
                 }
             }
         }
        return result;
    }
    
    public static List<Sentence> SortByLength(Text text)
    {
        var result = new List<Sentence>(text.Sentences);
        //result.Sort((a, b) => a.Value.Length.CompareTo(b.Value.Length));

        for (int i = 0; i < result.Count - 1; i++)
        {
            for (int j = 0; j < result.Count - 1 - i; j++)
            {
                int lengthA = result[j].Value.Length;
                int lengthB = result[j + 1].Value.Length;

                if (lengthA > lengthB)
                {
                    var temp = result[j];
                    result[j] = result[j + 1];
                    result[j + 1] = temp;
                }
            }
            
        }
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
    
    public static void RemoveStopWords(Text text, string[] stopWords)
    {
        var stopList = new List<string>();
        foreach (string s in stopWords)
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
    
    public static int RemoveWordsByLengthAndConsonant(Text text, int length)
    {
        string consonants = "бвгджзйклмнпрстфхцчшщbcdfghjklmnpqrstvwxyz";
        int removedCount = 0;
        var removedWords = new List<string>();

        foreach (var sentence in text.Sentences)
        {
            for (int i = sentence.Tokens.Count - 1; i >= 0; i--)
            {
                if (sentence.Tokens[i] is Word word && 
                    word.Value.Length == length &&
                    !string.IsNullOrEmpty(word.Value) &&
                    consonants.Contains(char.ToLower(word.Value[0])))
                {
                    removedWords.Add(word.Value);
                    sentence.Tokens.RemoveAt(i);
                    removedCount++;
                }
            }
        }
        
        if (removedWords.Count > 0)
        {
            Console.WriteLine($"\nУдаленные слова:");
            Console.WriteLine(string.Join(", ", removedWords));
        }
        else
        {
            Console.WriteLine("\nСлов для удаления не найдено");
        }

        return removedCount;
    }

    public static int ReplaceWordsInSentence(Sentence sentence, int length, string replacement)
    {
        int replacedCount = 0;
        var replacedWords = new List<string>();
        int totalcharschanged = 0;
        int waswordcharchanged = 0;
        int newcharchanged = 0;
        
        for (int i = 0; i < sentence.Tokens.Count; i++)
        {
            if (sentence.Tokens[i] is Word word && word.Value.Length == length)
            {
                replacedWords.Add(word.Value);
                sentence.Tokens[i] = new Word(replacement);
                replacedCount++;
                waswordcharchanged = word.Value.Length * replacedCount;
                newcharchanged = replacement.Length * replacedCount; 
                totalcharschanged = Math.Abs(waswordcharchanged - newcharchanged);
            }
            
        }
        
        if (replacedWords.Count > 0)
        {
            Console.WriteLine($"Замененные слова:");
            Console.WriteLine(string.Join(", ", replacedWords));
            Console.WriteLine($"На: {replacement}");
            Console.WriteLine($"Символов было: " + waswordcharchanged);
            Console.WriteLine($"Символов стало: " + newcharchanged);
            Console.WriteLine($"Разница: " + totalcharschanged);
            
        }
        else
        {
            Console.WriteLine("\nСлов для замены не найдено");
        }
        return replacedCount;
        } 
}