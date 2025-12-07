using System.Diagnostics.Tracing;
using ConsoleApp1.Lab3;
using System.Xml.Serialization;

class Program
{
    private static void ExportToXml(Text text, string path)
    {
        var serializer = new XmlSerializer(typeof(Text));

        using (StreamWriter sw = new StreamWriter(path))
        {
            serializer.Serialize(sw, text);
        }

        Console.WriteLine("\nФайл успешно сохранён! ");
    }
    static void Main()
    {
        Console.WriteLine("Выберите язык текста:");
        Console.WriteLine("1 - Русский");
        Console.WriteLine("2 - Английский");
        Console.Write("Ваш выбор:  \n");
        string choice = (Console.ReadLine() ?? "1");
        Text text = new Text();

        string textData = "";
        string stopFile = "";

        switch (choice)
        {
            case "1":
            {
                using (StreamReader sr = new StreamReader("C:\\Users\\edima\\RiderProjects\\ConsoleApp1\\ConsoleApp1\\Lab3\\files\\text_ru.txt"))
                {
                    textData = sr.ReadToEnd();
                }
                text = TextParser.Parse(textData);
                text.Fulltext = textData;
                text.Lines = new List<String>();
                text.Lines = textData.Split('\n').ToList();
                break;
            }
            
            case "2":
            {
                using (StreamReader sr = new StreamReader("C:\\Users\\edima\\RiderProjects\\ConsoleApp1\\ConsoleApp1\\Lab3\\files\\text_en.txt"))
                       {
                           textData = sr.ReadToEnd();
                       }
                text = TextParser.Parse(textData);
                text.Fulltext = textData;
                text.Lines = new List<String>();
                text.Lines = textData.Split('\n').ToList();
                break;
            }
        }

        while (true)
        {
            Console.WriteLine("\n=== МЕНЮ ===");
            Console.WriteLine("1 - Показать текст");
            Console.WriteLine("2 - Сортировать по количеству слов");
            Console.WriteLine("3 - Сортировать по длине предложений");
            Console.WriteLine("4 - Найти слова длины 4 в вопросительных предложениях");
            Console.WriteLine("5 - Удалить стоп-слова");
            Console.WriteLine("6 - Удалить слова заданной длины (согласные)");
            Console.WriteLine("7 - Заменить слова в предложении");
            Console.WriteLine("8 - Экспортировать в XML");
            Console.WriteLine("9 - Создать конкорданс");
            Console.WriteLine("0 - Выход");
            Console.Write("Выбор: ");
            string option = Console.ReadLine() ?? "";

            if (option == "0") break;

            switch (option)
            {
                case "1":
                    Console.WriteLine("\nТекст:"); 
                    
                    foreach (var s in text.Sentences) 
                        Console.WriteLine(s);
                    break;

                case "2": 
                    Console.WriteLine("\nПо количеству слов:"); 
                    
                    foreach (var s in TextProcessor.SortByWordCount(text)) 
                        Console.WriteLine(s);
                    break;

                case "3": 
                    Console.WriteLine("\nПо длине предложений:"); 
                    
                    foreach (var s in TextProcessor.SortByLength(text))
                      Console.WriteLine(s);
                    break;

                case "4":
                    Console.WriteLine("\nСлова длины 4 в вопросительных предложениях:");
                    
                    foreach (string w in TextProcessor.FindWordsInQuestions(text, 4))
                      Console.WriteLine(w);
                    break;

                case "5":
                    if (choice == "1")
                    {
                        stopFile = "C:\\Users\\edima\\RiderProjects\\ConsoleApp1\\ConsoleApp1\\Lab3\\files\\stopwords_ru.txt";
                    }
                    else if (choice == "2")
                    {
                        stopFile = "C:\\Users\\edima\\RiderProjects\\ConsoleApp1\\ConsoleApp1\\Lab3\\files\\stopwords_en.txt";
                    }

                    if (File.Exists(stopFile))
                    {
                        string[] stopWords = File.ReadAllLines(stopFile);
                        TextProcessor.RemoveStopWords(text, stopWords);
                        Console.WriteLine("\nТекст с удалёнными стоп-словами: ");

                        foreach (var s  in text.Sentences)
                        {
                            Console.WriteLine(s);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Файл {stopFile} не найден");
                    }
                    
                    break;
                
                case "6": 
                    Console.WriteLine("\nУдаление слов по длине начиная с согласной"); 
                    Console.Write("Введите длину слов для удаления: "); 
                    if (int.TryParse(Console.ReadLine(), out int deleteLength) && deleteLength > 0)
                    {
                        int removedCount = TextProcessor.RemoveWordsByLengthAndConsonant(text, deleteLength);
                        Console.WriteLine($"Удалено слов: {removedCount}");
                        Console.WriteLine("Обновленный текст:");
                        
                        foreach (var sentence in text.Sentences)
                            Console.WriteLine(sentence);
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: введите корректную длину!");
                    }
                    break;

                case "7":
                    Console.WriteLine("\nЗамена слов в предложении");
                    Console.Write("Введите номер предложения (начиная с 1): ");
                    if (int.TryParse(Console.ReadLine(), out int sentenceNum) && sentenceNum > 0 && sentenceNum <= text.Sentences.Count)
                    {
                        Console.Write("Введите длину слов для замены: ");
                        if (int.TryParse(Console.ReadLine(), out int replaceLength) && replaceLength > 0)
                        {
                            Console.Write("Введите подстроку для замены: ");
                            string replacement = Console.ReadLine() ?? "";
            
                            var sentence = text.Sentences[sentenceNum - 1];
                            int replacedCount = TextProcessor.ReplaceWordsInSentence(sentence, replaceLength, replacement);
            
                            Console.WriteLine($"Заменено слов: {replacedCount}");
                            Console.WriteLine("Обновленный текст:"); 
                            
                            foreach (var s in text.Sentences) 
                                Console.WriteLine(s);
                        }
                        else
                        {
                            Console.WriteLine("Ошибка: введите корректную длину!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: предложение с таким номером не найдено!");
                    }
                    break;
                
                case  "8":
                    ExportToXml(text, "C:\\Users\\edima\\RiderProjects\\ConsoleApp1\\ConsoleApp1\\Lab3\\files\\output.xml");
                    break;
                
                case "9":
                    TextProcessor.BuildConcordance(text);
                    break;
                
                default: 
                    Console.WriteLine("Неверный выбор."); 
                    break;
            }
        }

        Console.WriteLine("\nВыход из программы...");
    }
}
