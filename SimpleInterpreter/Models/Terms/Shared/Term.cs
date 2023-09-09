namespace SimpleCompiler.Models.Terms.Shared;

public abstract class Term
{
    public string Kind { get; set; }
    public Loc Location { get; set; }
}