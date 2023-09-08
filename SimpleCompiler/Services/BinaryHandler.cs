using SimpleCompiler.Enums;
using SimpleCompiler.Models;
using System.Diagnostics.SymbolStore;
using System.Linq.Expressions;
using System.Numerics;

namespace SimpleCompiler.Services
{
    public static class BinaryHandler
    {
        public static InterpretationResult Handle(InterpretationResult left, InterpretationResult right, BinaryOp op)
        {
            switch (op)
            {
                case BinaryOp.Add:
                    if (left.Kind == "number" && right.Kind == "number") return InterpretationResult.From("number", left.AsInt() + right.AsInt());
                    return InterpretationResult.From("string", left.AsString() + right.AsString());
                case BinaryOp.Sub:
                    return InterpretationResult.From("number", left.AsInt() - right.AsInt());
                case BinaryOp.Mul:
                    return InterpretationResult.From("number", left.AsInt() * right.AsInt());
                case BinaryOp.Div:
                    return InterpretationResult.From("number", left.AsInt() / right.AsInt());
                case BinaryOp.Rem:
                    return InterpretationResult.From("number", left.AsInt() % right.AsInt());
                case BinaryOp.Lt:
                    return InterpretationResult.From("number", left.AsInt() < right.AsInt());
                case BinaryOp.Gt:
                    return InterpretationResult.From("number", left.AsInt() > right.AsInt());
                case BinaryOp.Lte:
                    return InterpretationResult.From("number", left.AsInt() <= right.AsInt());
                case BinaryOp.Gte:
                    return InterpretationResult.From("number", left.AsInt() >= right.AsInt());
                case BinaryOp.And:
                    return InterpretationResult.From("boolean", left.AsBool() && right.AsBool());
                case BinaryOp.Or:
                    return InterpretationResult.From("boolean", left.AsBool() || right.AsBool());
                case BinaryOp.Eq:
                    return InterpretationResult.From("boolean", IsEqual(left, right));
                case BinaryOp.Neq:
                    return InterpretationResult.From("boolean", !IsEqual(left, right));
            }

            throw new NotImplementedException("Not implemented");
        }

        private static bool IsEqual(InterpretationResult left, InterpretationResult right)
        {
            if (left.Kind == "number" && right.Kind == "number")
                return left.AsInt() == right.AsInt();

            if (left.Kind == "string" && right.Kind == "string")
                return left.AsString().Equals(right.AsString());

            if (left.Kind == "boolean" && right.Kind == "boolean")
                return left.AsBool() == right.AsBool();

            return false;
        }
    }
}
