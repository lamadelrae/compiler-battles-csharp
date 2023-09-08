using SimpleCompiler.Enums;
using SimpleCompiler.Models.Terms.Shared;

namespace SimpleCompiler.Models.Terms;

public class Binary : Term
{
    public Term Lhs { get; set; }
    public BinaryOp Op { get; set; }
    public Term Rhs { get; set; }
}