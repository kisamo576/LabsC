namespace ConsoleApp1.Lab1
{
    class Program
    {
        static void Main()
        {
            
            GeneticData data = new GeneticData();
        
            GeneticData[] myArray = data.CreateArray();
        
            data.FindByOrganism(myArray);
        
            data.SortByAminoAcids(myArray);
            
            
            string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"../../../Lab1/files");
            string seq0 = Path.Combine(basePath, "sequences0.txt");
            string cmd0 = Path.Combine(basePath, "commands0.txt");
            string out0 = Path.Combine(basePath, "genedata0.txt");

            string seq1 = Path.Combine(basePath, "sequences1.txt");
            string cmd1 = Path.Combine(basePath, "commands1.txt");
            string out1 = Path.Combine(basePath, "genedata1.txt");

            string seq2 = Path.Combine(basePath, "sequences2.txt");
            string cmd2 = Path.Combine(basePath, "commands2.txt");
            string out2 = Path.Combine(basePath, "genedata2.txt");

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Меню:");
                Console.WriteLine("1) Обработать sequences.0  -> " + Path.GetFileName(out0));
                Console.WriteLine("2) Обработать sequences.1  -> " + Path.GetFileName(out1));
                Console.WriteLine("3) Обработать sequences.2  -> " + Path.GetFileName(out2));
                Console.WriteLine("4) Обработать все файлы");
                Console.WriteLine("5) Выход");
                Console.Write("Выбор (1-5): ");

                string choice = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(choice))
                {
                    Console.WriteLine("Пожалуйста, введите цифру от 1 до 5.");
                    continue;
                }

                if (choice == "5") break;

                try
                {
                    if (choice == "1")
                        ProcessFiles(seq0, cmd0, out0);
                    else if (choice == "2")
                        ProcessFiles(seq1, cmd1, out1);
                    else if (choice == "3")
                        ProcessFiles(seq2, cmd2, out2);
                    else if (choice == "4")
                    {
                        ProcessFiles(seq0, cmd0, out0);
                        ProcessFiles(seq1, cmd1, out1);
                        ProcessFiles(seq2, cmd2, out2);
                    }
                    else
                    {
                        Console.WriteLine("Пожалуйста, введите цифру от 1 до 5.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка: " + ex.Message);
                }
            }
        }

        static void ProcessFiles(string seqFile, string cmdFile, string outFile)
        {
            if (!File.Exists(seqFile))
                throw new FileNotFoundException("Файл не найден: " + seqFile);
            if (!File.Exists(cmdFile))
                throw new FileNotFoundException("Файл не найден: " + cmdFile);

            List<GeneticData> data = new List<GeneticData>();
            foreach (string line in File.ReadAllLines(seqFile))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                string[] parts = line.Split('\t',StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 3)
                {
                    parts = line.Split(new[] { '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                }
                if (parts.Length >= 3)
                {
                    GeneticData d = new GeneticData();
                    d.Protein = parts[0];
                    d.Organism = parts[1];
                    d.AminoAcids = parts[2];
                    data.Add(d);
                }
            }

            RLE.CheckAminoAcids(data);
float test =(float)11.2323426;
            using (StreamWriter sw = new StreamWriter(outFile))
            {
                sw.WriteLine(test.ToString("F2"));
                sw.WriteLine("Ermak Dmitry");
                sw.WriteLine("Genetic searching");
                sw.WriteLine(new string('-', 80));

                string[] commands = File.ReadAllLines(cmdFile);
                int num = 1;
                foreach (string cmd in commands)
                {
                    if (string.IsNullOrWhiteSpace(cmd)) continue;
                    string[] parts = cmd.Split('\t');
                    if (parts.Length == 1)
                        parts = cmd.Split(new[] { '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    sw.Write(num.ToString("D3") + "   " + cmd + Environment.NewLine);

                    string cmdName = parts.Length > 0 ? parts[0].Trim() : "";

                    if (cmdName.Equals("search", StringComparison.OrdinalIgnoreCase))
                    {
                        if (parts.Length < 2)
                        {
                            sw.WriteLine("organism\tprotein");
                            sw.WriteLine("INVALID COMMAND");
                        }
                        else
                        {
                            sw.WriteLine("organism\t\tprotein");
                            sw.WriteLine(RLE.Search(data, parts[1]));
                        }
                    }
                    else if (cmdName.Equals("diff", StringComparison.OrdinalIgnoreCase))
                    {
                        if (parts.Length < 3)
                        {
                            sw.WriteLine("INVALID COMMAND");
                        }
                        else
                        {
                            sw.WriteLine(RLE.Diff(data, parts[1], parts[2]));
                        }
                    }
                    else if (cmdName.Equals("mode", StringComparison.OrdinalIgnoreCase))
                    {
                        if (parts.Length < 2)
                        {
                            sw.WriteLine("INVALID COMMAND");
                        }
                        else
                        {
                            sw.WriteLine(RLE.Mode(data, parts[1]));
                        }
                    }
                    else
                    {
                        sw.WriteLine("UNKNOWN COMMAND");
                    }

                    sw.WriteLine(new string('-', 80));
                    num++;
                }
            }
            
            Console.WriteLine("Результаты сохранены в " + outFile);
        }
    }
}
