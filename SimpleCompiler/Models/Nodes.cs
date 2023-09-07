using SimpleCompiler.Enums;

namespace SimpleCompiler.Models.Nodes;

public struct Loc
{
    public int Start { get; set; }
    public int End { get; set; }
    public string Filename { get; set; }
}

public abstract class Term
{
    public Kind Kind { get; protected set; }
    public Loc Location { get; set; }
}

public class File
{
    public string Name { get; set; }
    public Term Expression { get; set; }
    public Loc Location { get; set; }
}

public class If : Term
{
    public Term Condition { get; set; }
    public Term Then { get; set; }
    public Term Otherwise { get; set; }
}

public class Let : Term
{
    public Parameter Name { get; set; }
    public Term Value { get; set; }
    public Term Next { get; set; }
}

public class Str : Term
{
    public string Value { get; set; }
}

public class Bool : Term
{
    public bool Value { get; set; }
}

public class Int : Term
{
    public int Value { get; set; }
}

public class BinaryOp : Term
{
    public BinaryOpType Op { get; set; }
    public Term Left { get; set; }
    public Term Right { get; set; }
}

public class Call : Term
{
    public Term Callee { get; set; }
    public List<Term> Arguments { get; set; }
}

public class Function : Term
{
    public List<Parameter> Parameters { get; set; }
    public Term Value { get; set; }
}

public class Print : Term
{
    public Term Value { get; set; }
}

public class First : Term
{
    public Term Value { get; set; }
}

public class Second : Term
{
    public Term Value { get; set; }
}

public class Tuple : Term
{
    public Term First { get; set; }
    public Term Second { get; set; }
}

public class Parameter
{
    public string Text { get; set; }
    public Loc Location { get; set; }
}

public class Var : Term
{
    public string Text { get; set; }
}
