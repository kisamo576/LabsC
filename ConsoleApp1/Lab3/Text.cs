namespace ConsoleApp1.Lab3;

public class Text
{
    public List<Sentence> Sentences { get; set; } =  new List<Sentence>();

    public override string ToString()
    {
        return string.Join("\n", Sentences);
    }

}