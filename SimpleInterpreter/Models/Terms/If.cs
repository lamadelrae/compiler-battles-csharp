using SimpleCompiler.Models.Terms.Shared;

namespace SimpleCompiler.Models.Terms;

public class If : Term
{
    public Term Condition { get; set; }
    public Term Then { get; set; }
    public Term Otherwise { get; set; }
}
