namespace SimpleCompiler.Models.Terms.Shared;

public class Loc
{
    public int Start { get; set; }
    public int End { get; set; }
    public string FileName { get; set; }

    public static Loc From(dynamic node)
    {
        return new Loc
        {
            Start = node.start,
            End = node.end,
            FileName = node.filename,
        };
    }
}
