using ConsoleApp1.Lab3;

class Program
{
    static void Main()
    {
        Console.WriteLine("Выберите язык текста:");
        Console.WriteLine("1 - Английский");
        Console.WriteLine("2 - Русский");
        Console.Write("Ваш выбор:  \n");
        string choice = Console.ReadLine() ?? "1";

        string textData;
        string stopFile;

        if (choice == "2")
        {
            stopFile = "C:\\Users\\edima\\RiderProjects\\ConsoleApp1\\ConsoleApp1\\Lab3\\files\\stopwords_ru.txt";
            textData = "Привет! Как дела? Сегодня мы будем изучать C#. " +
                       "C# — это современный язык программирования. " +
                       "Можно ли написать программу быстро? " +
                       "Многие разработчики любят C#. " +
                       "Например, Вася создал проект на C#. " +
                       "Сколько времени занимает изучение C#? ";
        }
        else
        {
            stopFile = "C:\\Users\\edima\\RiderProjects\\ConsoleApp1\\ConsoleApp1\\Lab3\\files\\stopwords_en.txt";
            textData = "Hello! How are you? Today we will study C#. " +
                       "C# is a modern programming language. " +
                       "Can we write a program quickly? " +
                       "Many developers love C#. " +
                       "For example, John created a project in C#. " +
                       "How long does it take to learn C#?";
        }

        var text = TextParser.Parse(textData);

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
                    foreach (var w in TextProcessor.FindWordsInQuestions(text, 4))
                        Console.WriteLine(w);
                    break;

                case "5":
                    if (File.Exists(stopFile))
                    {
                        var stopWords = File.ReadAllLines(stopFile);
                        TextProcessor.RemoveStopWords(text, stopWords);
                        Console.WriteLine("\nСтоп-слова удалены. Текущий текст:");
                        foreach (var s in text.Sentences)
                            Console.WriteLine(s);
                    }
                    else
                    {
                        Console.WriteLine($"Файл {stopFile} не найден!");
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

                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }
        }

        Console.WriteLine("\nВыход из программы...");
    }
}
