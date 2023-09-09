using SimpleCompiler.Models.Terms.Shared;

namespace SimpleCompiler.Models.Terms;

public class Let : Term
{
    public Parameter Name { get; set; }
    public Term Value { get; set; }
    public Term Next { get; set; }
}