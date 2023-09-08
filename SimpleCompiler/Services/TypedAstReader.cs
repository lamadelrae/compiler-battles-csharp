using SimpleCompiler.Models.Nodes;

namespace SimpleCompiler.Services
{
    public static class TypedAstReader
    {
        public static void Read(Term node, int indentLevel = 0)
        {
            string indentation = new string(' ', indentLevel * 2);
            Console.WriteLine(indentation + "Kind: " + node.Kind);

            switch (node)
            {
                case Let letNode:
                    Console.WriteLine(indentation + "Name: " + letNode.Name.Text);
                    Read(letNode.Value, indentLevel + 1);
                    Read(letNode.Next, indentLevel);
                    break;
                case Function functionNode:
                    foreach (var parameter in functionNode.Parameters)
                    {
                        Console.WriteLine(indentation + "Parameter: " + parameter.Text);
                    }
                    Read(functionNode.Value, indentLevel + 1);
                    break;
                case If ifNode:
                    Read(ifNode.Condition, indentLevel + 1);
                    Read(ifNode.Then, indentLevel + 1);
                    Read(ifNode.Otherwise, indentLevel + 1);
                    break;
                case Binary binaryNode:
                    Read(binaryNode.Lhs, indentLevel + 1);
                    Console.WriteLine(indentation + "Operation: " + binaryNode.Op);
                    Read(binaryNode.Rhs, indentLevel + 1);
                    break;
                case Var varNode:
                    Console.WriteLine(indentation + "Variable: " + varNode.Text);
                    break;
                case Print printNode:
                    Read(printNode.Value, indentLevel + 1);
                    break;
                case Call callNode:
                    Read(callNode.Callee, indentLevel + 1);
                    foreach (var argument in callNode.Arguments)
                    {
                        Console.WriteLine(indentation + "Arguments:");
                        Read(argument, indentLevel + 1);
                    }
                    break;
                case Int intNode:
                    Console.WriteLine(indentation + "Value: " + intNode.Value);
                    break;
                case Str strNode:
                    Console.WriteLine(indentation + "Value: " + strNode.Value);
                    break;
                case Bool boolNode:
                    Console.WriteLine(indentation + "Value: " + boolNode.Value);
                    break;
                default:
                    Console.WriteLine(indentation + "Unhandled Node Kind: " + node.Kind);
                    break;
            }
        }
    }
}
