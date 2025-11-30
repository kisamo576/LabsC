using System.Xml.Serialization;

namespace ConsoleApp1.Lab3;

[XmlRoot("text")]
public class Text
{
    [XmlElement("sentence")]
    public List<Sentence> Sentences { get; set; } =  new List<Sentence>();

    public override string ToString()
    {
        return string.Join("\n", Sentences);
    }

}