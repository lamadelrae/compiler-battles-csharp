using SimpleCompiler.Models.Nodes;

namespace SimpleCompiler.CodeGenerator
{
    internal class JavascriptCodeGenerator
    {
        public static string GenerateJavaScript(Term node)
        {
            switch (node)
            {
                case Let letNode:
                    return $"let {letNode.Name.Text} = {GenerateJavaScript(letNode.Value)};\n{GenerateJavaScript(letNode.Next)}";
                case Function functionNode:
                    string parameters = string.Join(", ", functionNode.Parameters.Select(p => p.Text));
                    return $"function ({parameters}) {{\n{GenerateJavaScript(functionNode.Value)}}}";
                case If ifNode:
                    string elsePart = ifNode.Otherwise != null ? $" else {{\n{GenerateJavaScript(ifNode.Otherwise)}\n}}" : "";
                    return $"if ({GenerateJavaScript(ifNode.Condition)}) {{\n{GenerateJavaScript(ifNode.Then)}}}{elsePart}";
                case Binary binaryNode:
                    string op = GetJavaScriptOperator(binaryNode.Op);
                    return $"({GenerateJavaScript(binaryNode.Lhs)} {op} {GenerateJavaScript(binaryNode.Rhs)})";
                case Var varNode:
                    return varNode.Text;
                case Print printNode:
                    return $"console.log({GenerateJavaScript(printNode.Value)});";
                case Call callNode:
                    string arguments = string.Join(", ", callNode.Arguments.Select(GenerateJavaScript));
                    return $"{GenerateJavaScript(callNode.Callee)}({arguments})";
                case Int intNode:
                    return intNode.Value.ToString();
                case Str strNode:
                    return $"\"{strNode.Value}\"";
                case Bool boolNode:
                    return boolNode.Value.ToString().ToLower();
                default:
                    return $"/* Unhandled Node Kind: {node.Kind} */";
            }
        }

        private static string GetJavaScriptOperator(BinaryOp op)
        {
            switch (op)
            {
                case BinaryOp.Add: return "+";
                case BinaryOp.Sub: return "-";
                case BinaryOp.Mul: return "*";
                case BinaryOp.Div: return "/";
                case BinaryOp.Rem: return "%";
                case BinaryOp.Eq: return "==";
                case BinaryOp.Neq: return "!=";
                case BinaryOp.Lt: return "<";
                case BinaryOp.Gt: return ">";
                case BinaryOp.Lte: return "<=";
                case BinaryOp.Gte: return ">=";
                case BinaryOp.And: return "&&";
                case BinaryOp.Or: return "||";
                default: return "/* Unknown Operator */";
            }
        }

    }
}
