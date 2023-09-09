using SimpleCompiler.Models.Terms.Shared;

namespace SimpleCompiler.Models.Terms;

public class Function : Term
{
    public List<Parameter> Parameters { get; set; }
    public Term Value { get; set; }
}