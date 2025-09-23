using System.Text;

namespace ConsoleApp1.Lab1
{
    public static class RLE
    {
        public static string RLEncoding(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";

            StringBuilder result = new StringBuilder();
            int count = 1;

            for (int i = 1; i <= input.Length; i++)
            {
                if (i < input.Length && input[i] == input[i - 1])
                {
                    count++;
                }
                else
                {
                    if (count > 2)
                        result.Append(count);
                    if (count == 1)
                        result.Append(input[i - 1]);
                    else if (count == 2)
                        result.Append(input[i - 1]).Append(input[i - 1]);
                    else
                        result.Append(input[i - 1]);
                    count = 1;
                }
            }

            return result.ToString();
        }

        public static string RLDecoding(string encoded)
        {
            if (string.IsNullOrEmpty(encoded)) return "";
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < encoded.Length; i++)
            {
                char c = encoded[i];
                if (char.IsDigit(c))
                {
                    int count = c - '0';
                    i++;
                    char symbol = encoded[i];
                    for (int j = 0; j < count; j++)
                        result.Append(symbol);
                }
                else
                {
                    result.Append(c);
                }
            }

            return result.ToString();
        }

        public static string Search(List<GeneticData> data, string sequence)
        {
            string decoded = RLDecoding(sequence);
            string result = "";
            foreach (GeneticData d in data)
            {
                if (d.AminoAcids.Contains(decoded))
                {
                    result += d.Organism + "\t" + d.Protein + Environment.NewLine;
                }
            }

            if (result == "")
                return "NOT FOUND";
            return result;
        }

        public static string Diff(List<GeneticData> data, string protein1, string protein2)
        {
            GeneticData p1 = new GeneticData();
            GeneticData p2 = new GeneticData();
            bool found1 = false, found2 = false;

            foreach (GeneticData d in data)
            {
                if (d.Protein == protein1)
                {
                    p1 = d;
                    found1 = true;
                }

                if (d.Protein == protein2)
                {
                    p2 = d;
                    found2 = true;
                }
            }

            if (!found1) return "MISSING: " + protein1;
            if (!found2) return "MISSING: " + protein2;

            string seq1 = RLDecoding(p1.AminoAcids);
            string seq2 = RLDecoding(p2.AminoAcids);

            int length = Math.Max(seq1.Length, seq2.Length);
            int diffCount = 0;

            for (int i = 0; i < length; i++)
            {
                char c1 = i < seq1.Length ? seq1[i] : '-';
                char c2 = i < seq2.Length ? seq2[i] : '-';

                if (c1 != c2)
                    diffCount++;
            }

            return "amino-acids difference:\n" + diffCount;
        }

        public static string Mode(List<GeneticData> data, string proteinName)
        {
            GeneticData p = new GeneticData();
            bool found = false;

            foreach (GeneticData d in data)
            {
                if (d.Protein == proteinName)
                {
                    p = d;
                    found = true;
                }
            }

            if (!found)
                return "MISSING: " + proteinName;

            string decoded = RLDecoding(p.AminoAcids);

            Dictionary<char, int> counts = new Dictionary<char, int>();
            foreach (char c in decoded)
            {
                if (!counts.ContainsKey(c))
                    counts[c] = 0;
                counts[c]++;
            }

            int max = 0;
            char amino = ' ';
            foreach (KeyValuePair<char, int> kv in counts)
            {
                if (kv.Value > max || (kv.Value == max && kv.Key < amino))
                {
                    max = kv.Value;
                    amino = kv.Key;
                }
            }

            return "amino-acid occurs:\n" + amino + "     " + max;
        }


        public static void CheckAminoAcids(List<GeneticData> data)
        {
            string valid = "ACDEFGHIKLMNPQRSTVWY";

            foreach (GeneticData d in data)
            {
                string decoded = RLDecoding(d.AminoAcids);

                foreach (char c in decoded)
                {
                    if (!valid.Contains(c.ToString()))
                    {
                        Console.WriteLine("Белок " + d.Protein + " содержит недопустимый символ: " + c);
                    }
                }
            }
        }
    }
}
