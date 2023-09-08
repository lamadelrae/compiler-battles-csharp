using SimpleCompiler.Models.Nodes;

namespace SimpleCompiler.Services
{
    public static class TypedAstGenerator
    {
        public static Term Generate(dynamic dynamicNode)
        {
            string kind = dynamicNode.kind;
            switch ((string)kind)
            {
                case "Let":
                    return new Let
                    {
                        Kind = kind,
                        Name = new Let.LetName
                        {
                            Text = dynamicNode.name.text,
                            Location = Loc.From(dynamicNode.name.location)
                        },
                        Value = Generate(dynamicNode.value),
                        Next = Generate(dynamicNode.next),
                        Location = Loc.From(dynamicNode.location)
                    };
                case "Function":
                    var functionNode = new Function
                    {
                        Kind = kind,
                        Parameters = new List<Function.Parameter>(),
                        Value = Generate(dynamicNode.value),
                        Location = Loc.From(dynamicNode.location)
                    };
                    foreach (var parameter in dynamicNode.parameters)
                    {
                        functionNode.Parameters.Add(new Function.Parameter
                        {
                            Text = parameter.text,
                            Location = Loc.From(parameter.location)
                        });
                    }
                    return functionNode;
                case "If":
                    return new If
                    {
                        Kind = kind,
                        Condition = Generate(dynamicNode.condition),
                        Then = Generate(dynamicNode.then),
                        Otherwise = Generate(dynamicNode.otherwise),
                        Location = Loc.From(dynamicNode.location)
                    };
                case "Binary":
                    return new Binary
                    {
                        Kind = kind,
                        Lhs = Generate(dynamicNode.lhs),
                        Op = ConvertToBinaryOp(dynamicNode.op),
                        Rhs = Generate(dynamicNode.rhs),
                        Location = Loc.From(dynamicNode.location)
                    };
                case "Var":
                    return new Var
                    {
                        Kind = kind,
                        Text = dynamicNode.text,
                        Location = Loc.From(dynamicNode.location)
                    };
                case "Print":
                    return new Print
                    {
                        Kind = kind,
                        Value = Generate(dynamicNode.value),
                        Location = Loc.From(dynamicNode.location)
                    };
                case "Call":
                    var callNode = new Call
                    {
                        Kind = kind,
                        Callee = Generate(dynamicNode.callee),
                        Arguments = new List<Term>(),
                        Location = Loc.From(dynamicNode.location)
                    };
                    foreach (var argument in dynamicNode.arguments)
                    {
                        callNode.Arguments.Add(Generate(argument));
                    }
                    return callNode;
                case "Int":
                    return new Int
                    {
                        Kind = kind,
                        Value = dynamicNode.value,
                        Location = Loc.From(dynamicNode.location)
                    };
                case "Str":
                    return new Str
                    {
                        Kind = kind,
                        Value = dynamicNode.value,
                        Location = Loc.From(dynamicNode.location)
                    };
                case "Bool":
                    return new Bool
                    {
                        Kind = kind,
                        Value = dynamicNode.value,
                        Location = Loc.From(dynamicNode.location)
                    };
                // Add cases for other node kinds as needed
                default:
                    throw new NotSupportedException($"Unsupported node kind: {kind}");
            }
        }

        private static BinaryOp ConvertToBinaryOp(dynamic dynamicOp)
        {
            string op = dynamicOp;
            return op switch
            {
                "Add" => BinaryOp.Add,
                "Sub" => BinaryOp.Sub,
                "Mul" => BinaryOp.Mul,
                "Div" => BinaryOp.Div,
                "Rem" => BinaryOp.Rem,
                "Eq" => BinaryOp.Eq,
                "Neq" => BinaryOp.Neq,
                "Lt" => BinaryOp.Lt,
                "Gt" => BinaryOp.Gt,
                "Lte" => BinaryOp.Lte,
                "Gte" => BinaryOp.Gte,
                "And" => BinaryOp.And,
                "Or" => BinaryOp.Or,
                _ => throw new NotSupportedException($"Unsupported binary operation: {op}")
            };
        }
    }
}
