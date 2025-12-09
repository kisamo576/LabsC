namespace ConsoleApp1.Lab3;

public static class TextProcessor
{
    public static List<Sentence> SortByWordCount(Text text)
    {
        var result = new List<Sentence>(text.Sentences);
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
            if (!sentence.IsQuestion())
                continue;

            foreach (var word in sentence.GetWords())
            {
                if (word.Value.Length == length)
                {
                    string lowerWord = word.Value.ToLower();
                    if (!found.Contains(lowerWord))
                        found.Add(lowerWord);
                }
            }
        }

        return found;
    }

    public static void RemoveStopWords(Text text, string[] stopWords)
    {
        string[] stopList = stopWords.Select(s => s.ToLower()).ToArray();

        foreach (var sentence in text.Sentences)
        {
            for (int i = 0; i < sentence.Tokens.Count; i++)
            {
                if (sentence.Tokens[i] is Word w && 
                    w.IsStopWord(stopList))
                {
                    sentence.Tokens.RemoveAt(i);
                    i--;
                }
            }
        }
    }

    public static int RemoveWordsByLengthAndConsonant(Text text, int length)
    {
        int removedCount = 0;
        var removedWords = new List<string>();

        foreach (var sentence in text.Sentences)
        {
            for (int i = sentence.Tokens.Count - 1; i >= 0; i--)
            {
                if (sentence.Tokens[i] is Word word &&
                    word.Value.Length == length &&
                    word.StartsWithConsonant())
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

    public static void BuildConcordance(Text text)
    {
        var concordance = new Dictionary<string, (int count, List<int> lines)>();

        int lineNumber = 1;

        foreach (string line in text.Lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                lineNumber++;
                continue;
            }
            
            string[] words = line.Split(new[] { ' ', ',', '.', '!', '?', ';', ':', '[', ']', '{', '}', '"', '—', '\'' },
                StringSplitOptions.RemoveEmptyEntries);
            
            var wordsInLine = new List<string>();

            foreach (string word in words)
            {
                string cleanWord = word.Trim().ToLower();

                if (string.IsNullOrEmpty(cleanWord))
                {
                    continue;
                }

                if (!concordance.ContainsKey(cleanWord))
                {
                    concordance[cleanWord] = (0, new List<int>());
                }
                
                var tuple = concordance[cleanWord];
                concordance[cleanWord] = (tuple.count + 1, tuple.lines);

                if (!wordsInLine.Contains(cleanWord))
                {
                    concordance[cleanWord].lines.Add(lineNumber);
                    wordsInLine.Add(cleanWord);
                }
            }

            lineNumber++;
        }
        
        var sortedKeys = new List<string>(concordance.Keys);
        sortedKeys.Sort();
        
        Console.WriteLine("\nКонкорданс: ");

        foreach (string word in sortedKeys)
        {
            int totalCount = concordance[word].count;
            List<int> lineNumbers = concordance[word].lines;
            
            lineNumbers.Sort();

            string lineNumberStr = " ";
            
            foreach (int num in lineNumbers)
            {
                lineNumberStr += num.ToString();
            }
            
            int dotsCount = 20 - word.Length;
            if (dotsCount < 0) dotsCount = 1;
            
            string dots = new string('.', dotsCount);
            
            Console.WriteLine($"{word} {dots} {totalCount}: {string.Join(", ", lineNumbers)}");
        }
        Console.WriteLine("Конкорданс готов!");
    }
}