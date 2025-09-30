namespace ConsoleApp1.Lab1
{
    public struct GeneticData
    {
        public string Protein;
        public string Organism;
        public string AminoAcids;

            
         public GeneticData(string Protein, string Organism)
         {
             this.Protein = Protein;
             this.Organism = Organism;
             this.AminoAcids = "";
        }

        public GeneticData CreateGen ()
        {
            GeneticData genData = new GeneticData();
            
            Console.Write("Введите название белка: ");
            genData.Protein = Console.ReadLine();
            Console.Write("Введите организм: ");
            genData.Organism = Console.ReadLine();
            Console.Write("Введите аминокислоты: ");
            genData.AminoAcids = Console.ReadLine();
            return genData;
        }

        public GeneticData[] CreateArray()
        {
            // добавить объект в массив + 2 метода (поиск объекта по параметру организм и сортировка по аминоаксиду алфавит
            GeneticData[] array = new GeneticData[3];
            
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"Ввод данных {i + 1}:");
                array[i] = CreateGen() ;
                Console.WriteLine();
            }
            
            return array;
        }
        
        public void FindByOrganism(GeneticData[] array)
        {
            Console.Write("Введите организм для поиска: ");
            string search = Console.ReadLine();
            
            bool found = false;
            
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Organism.ToLower() == search.ToLower())
                {
                    Console.WriteLine($"Найден: {array[i].Protein}, {array[i].Organism}, {array[i].AminoAcids}");
                    found = true;
                }
            }
            
            if (!found)
            {
                Console.WriteLine("Ничего не найдено");
            }
        }
        public void SortByAminoAcids(GeneticData[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 0; j < array.Length - 1; j++)
                {
                    if (string.Compare(array[j].AminoAcids, array[j + 1].AminoAcids) > 0)
                    {
                        GeneticData temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
            
            Console.WriteLine("Отсортированный массив:");
            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {array[i].Protein} - {array[i].AminoAcids}");
            }
        }    
    }
}    
