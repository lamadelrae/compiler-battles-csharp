using SimpleCompiler.Models.Terms.Shared;

namespace SimpleCompiler.Models.Terms;

public class String : Term
{
    public string Value { get; set; }
}