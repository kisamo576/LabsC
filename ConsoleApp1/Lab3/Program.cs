using ConsoleApp1.Lab3;

class Program
{
    static void Main()
    {
        Console.WriteLine("Выберите язык текста:");
        Console.WriteLine("1 - Английский");
        Console.WriteLine("2 - Русский");
        Console.Write("Ваш выбор: ");
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
                       "Сколько времени занимает изучение C#?";
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
            Console.WriteLine("0 - Выход");
            Console.Write("Выбор: ");
            string option = Console.ReadLine() ?? "";

            if (option == "0") break;

            switch (option)
            {
                case "1":
                    Console.WriteLine("\nИсходный текст:");
                    foreach (var s in text.Sentences)
                        Console.WriteLine(s.Value);
                    break;

                case "2":
                    Console.WriteLine("\nПо количеству слов:");
                    foreach (var s in TextProcessor.SortByWordCount(text))
                        Console.WriteLine(s.Value);
                    break;

                case "3":
                    Console.WriteLine("\nПо длине предложений:");
                    foreach (var s in TextProcessor.SortByLength(text))
                        Console.WriteLine(s.Value);
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
                            Console.WriteLine(s.Value);
                    }
                    else
                    {
                        Console.WriteLine($"Файл {stopFile} не найден!");
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
