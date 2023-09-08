using SimpleCompiler.Models;
using SimpleCompiler.Models.Terms.Shared;
using String = SimpleCompiler.Models.Terms.String;
using Bool = SimpleCompiler.Models.Terms.Bool;
using Int = SimpleCompiler.Models.Terms.Int;
using Tuple = SimpleCompiler.Models.Terms.Tuple;
using SimpleCompiler.Models.Terms;

namespace SimpleCompiler.Services;

public class Interpreter
{
    public InterpretationResult Handle(Term term, InterpretationEnvironment env)
    {
        switch (term.Kind)
        {
            case "Let":
                var let = (Let)term;
                var newEnv = env.DeepClone();
                var value = Handle(let, newEnv);

                newEnv.Objects.Add(let.Name.Text, value);

                return Handle(let.Next, newEnv);

            case "Str":
                return InterpretationResult.From("string", ((String)term).Value);
            case "Bool":
                return InterpretationResult.From("string", ((Bool)term).Value);
            case "Int":
                return InterpretationResult.From("string", ((Int)term).Value);
            case "If":
                var @if = (If)term;
                var condition = Handle(@if.Condition, env);
                var @bool = condition.AsBool();

                return Handle(@bool ? @if.Then : @if.Otherwise, env);
            case "Tuple":
                var @tuple = (Tuple)term;
                var first = Handle(@tuple.First, env);
                var second = Handle(@tuple.Second, env);

                return InterpretationResult.From("tuple", (first.Value, second.Value));

            case "Binary":
                var binary = (Binary)term;

                var left = Handle(binary.Lhs, env);
                var right = Handle(binary.Rhs, env);

                return BinaryHandler.Handle(left, right, binary.Op);

            case "Call": 
                var call = (Call)term;
                var function = Handle(call, env);

                var closure = function.AsClosure();
                var closureEnv = closure.Env.DeepClone();

                if (call.Arguments.Count != closure.Parameters.Count) throw new Exception("Length not right");

                for(int i = 0; i < closure.Parameters.Count; i++)
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