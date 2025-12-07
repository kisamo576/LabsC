using System.Xml.Serialization;

namespace ConsoleApp1.Lab3;

[XmlRoot("text")]
public class Text
{
    [XmlElement("sentence")]
    public List<Sentence> Sentences { get; set; } =  new List<Sentence>();


    [XmlIgnore] 
    public string Fulltext { get; set; } = "";
    
    [XmlIgnore]
    public List<string> Lines { get; set; } =  new List<string>();
    
    public void Add(Sentence sentence)
    {
        Sentences.Add(sentence);
    }

    public List<Word> GetAllWords()
    {
        var allWords = new List<Word>();
        foreach (var sentence in Sentences)
        {
            List<Word> wordsInSentence = sentence.GetWords();

            foreach (var word in wordsInSentence)
            {
                allWords.Add(word);
            }
        }
        return allWords;
    }

    public int SentencesCount()
    {
        return Sentences.Count;
    }

    public override string ToString()
    {
        return string.Join("\n", Sentences);
    }

}