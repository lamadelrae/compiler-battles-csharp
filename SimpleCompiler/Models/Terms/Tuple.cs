using SimpleCompiler.Models.Terms.Shared;

namespace SimpleCompiler.Models.Terms;

public class Tuple : Term
{
    public Tuple First { get; set; }
    public Tuple Second { get; set; }
}