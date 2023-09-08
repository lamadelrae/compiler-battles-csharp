using SimpleCompiler.Models.Terms.Shared;

namespace SimpleCompiler.Models.Terms;

public class Call : Term
{
    public Term Callee { get; set; }
    public List<Term> Arguments { get; set; }
}
