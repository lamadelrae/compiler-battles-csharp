using SimpleCompiler.Models;
using SimpleCompiler.Models.Terms;
using SimpleCompiler.Models.Terms.Shared;
using Bool = SimpleCompiler.Models.Terms.Bool;
using Int = SimpleCompiler.Models.Terms.Int;
using String = SimpleCompiler.Models.Terms.String;

namespace SimpleCompiler.Services;

public static class Interpreter
{
    public static InterpretationResult Handle(Term term, InterpretationEnvironment env)
    {
        switch (term.Kind)
        {
            case "Let":
                var let = (Let)term;
                var newEnv = env.DeepClone();
                var value = Handle(let.Value, newEnv);

                newEnv.Objects.Add(let.Name.Text, value);

                return Handle(let.Next, newEnv);

            case "Str":
                return InterpretationResult.From("string", ((String)term).Value);
            case "Bool":
                return InterpretationResult.From("boolean", ((Bool)term).Value);
            case "Int":
                return InterpretationResult.From("number", ((Int)term).Value);
            case "Var":
                var @var = (Var)term;

                var isValValid = env.Objects.TryGetValue(@var.Text, out var result);
                if (!isValValid)
                    throw new Exception();

                return result;
            case "If":
                var @if = (If)term;
                var condition = Handle(@if.Condition, env);
                var @bool = condition.AsBool();

                return Handle(@bool ? @if.Then : @if.Otherwise, env);

            case "Binary":
                var binary = (Binary)term;

                var left = Handle(binary.Lhs, env);
                var right = Handle(binary.Rhs, env);

                return BinaryHandler.Handle(left, right, binary.Op);

            case "Print":
                var print = (Print)term;
                var printVal = Handle(print.Value, env);

                if (printVal.Kind == "number") Console.WriteLine(printVal.AsInt());
                if (printVal.Kind == "boolean") Console.WriteLine(printVal.AsBool());
                if (printVal.Kind == "string") Console.WriteLine(printVal.AsString());
                if (printVal.Kind == "closure") Console.WriteLine(printVal.AsClosure());

                return printVal;

            case "Call":
                var call = (Call)term;
                var function = Handle(call.Callee, env);

                var closure = function.AsClosure();
                var closureEnv = closure.Env.DeepClone();

                if (call.Arguments.Count != closure.Parameters.Count) throw new Exception("Length not right");

                for (int i = 0; i < closure.Parameters.Count; i++)
                {
                    var param = closure.Parameters[i];
                    var functionsArg = call.Arguments[i];

                    closureEnv.Objects.Add(param, Handle(functionsArg, env));
                }

                return Handle(closure.Body, closureEnv);


            case "Function":
                var func = (Function)term;
                return InterpretationResult.From("closure", new Closure() { Body = func.Value, Env = env, Parameters = func.Parameters.Select(x => x.Text).ToList() });

            default:
                throw new NotImplementedException();
        }
    }
}