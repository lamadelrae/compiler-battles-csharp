using SimpleCompiler.Models.Terms.Shared;

namespace SimpleCompiler.Models.Terms;

public class Print : Term
{
    public Term Value { get; set; }
}