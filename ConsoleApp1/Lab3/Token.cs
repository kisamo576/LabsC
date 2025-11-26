namespace ConsoleApp1.Lab3;

public abstract class Token
{
    public string Value { get; set; }

    public override string ToString()
    {
        return Value;
    }

    // public interface IWork
    // {
    //     public static List<Sentence> SortByWordCount(Text text);
    //
    // }

    // public class worker : IWork
    // {
    //     //IEnumerable<>
    //         
    // }
}