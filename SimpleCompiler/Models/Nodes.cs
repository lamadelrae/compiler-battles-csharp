
namespace SimpleCompiler.Models.Nodes;

public class File
{
    public string Name { get; set; }
    public Term Expression { get; set; }
    public Loc Location { get; set; }
}

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


public enum BinaryOp
{
    Add,
    Sub,
    Mul,
    Div,
    Rem,
    Eq,
    Neq,
    Lt,
    Gt,
    Lte,
    Gte,
    And,
    Or
}

public abstract class Term
{
    public string Kind { get; set; }
    public Loc Location { get; set; }
}

public class Let : Term
{
    public LetName Name { get; set; }
    public Term Value { get; set; }
    public Term Next { get; set; }

    public class LetName
    {
        public string Text { get; set; }
        public Loc Location { get; set; }
    }
}

public class Function : Term
{
    public List<Parameter> Parameters { get; set; }
    public Term Value { get; set; }

    public class Parameter
    {
        public string Text { get; set; }
        public Loc Location { get; set; }
    }
}

public class If : Term
{
    public Term Condition { get; set; }
    public Term Then { get; set; }
    public Term Otherwise { get; set; }
}

public class Binary : Term
{
    public Term Lhs { get; set; }
    public BinaryOp Op { get; set; }
    public Term Rhs { get; set; }
}

public class Var : Term
{
    public string Text { get; set; }
}

public class Print : Term
{
    public Term Value { get; set; }
}

public class Call : Term
{
    public Term Callee { get; set; }
    public List<Term> Arguments { get; set; }
}

public class Int : Term
{
    public int Value { get; set; }
}

public class Str : Term
{
    public string Value { get; set; }
}

public class Bool : Term
{
    public bool Value { get; set; }
}