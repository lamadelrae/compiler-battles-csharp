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
        return term.Kind switch
        {
            "Let" => HandleLet((Let)term, env),
            "Str" => InterpretationResult.From("string", ((String)term).Value),
            "Bool" => InterpretationResult.From("boolean", ((Bool)term).Value),
            "Int" => InterpretationResult.From("number", ((Int)term).Value),
            "Var" => HandleVar((Var)term, env),
            "If" => HandleIf((If)term, env),
            "Binary" => HandleBinary((Binary)term, env),
            "Print" => HandlePrint((Print)term, env),
            "Call" => HandleCall((Call)term, env),
            "Function" => HandleFunction((Function)term, env),
            _ => throw new NotImplementedException(),
        };
    }

    private static InterpretationResult HandleLet(Let let, InterpretationEnvironment env)
    {
        var newEnv = env.DeepClone();
        var value = Handle(let.Value, newEnv);

        newEnv.Objects.Add(let.Name.Text, value);

        return Handle(let.Next, newEnv);
    }

    private static InterpretationResult HandleVar(Var var, InterpretationEnvironment env)
    {
        var isValValid = env.Objects.TryGetValue(var.Text, out var result);
        if (!isValValid)
            throw new Exception();

        return result;
    }

    private static InterpretationResult HandleIf(If @if, InterpretationEnvironment env)
    {
        var condition = Handle(@if.Condition, env);
        var @bool = condition.AsBool();

        return Handle(@bool ? @if.Then : @if.Otherwise, env);
    }

    private static InterpretationResult HandleBinary(Binary binary, InterpretationEnvironment env)
    {
        var left = Handle(binary.Lhs, env);
        var right = Handle(binary.Rhs, env);

        return BinaryHandler.Handle(left, right, binary.Op);
    }

    private static InterpretationResult HandlePrint(Print print, InterpretationEnvironment env)
    {
        var printVal = Handle(print.Value, env);

        Console.WriteLine(printVal switch
        {
            { Kind: "number" } => printVal.AsInt().ToString(),
            { Kind: "boolean" } => printVal.AsBool().ToString(),
            { Kind: "string" } => printVal.AsString(),
            { Kind: "closure" } => printVal.AsClosure().ToString(),
            _ => throw new NotImplementedException(),
        });

        return printVal;
    }

    private static InterpretationResult HandleCall(Call call, InterpretationEnvironment env)
    {
        var function = Handle(call.Callee, env);

        var closure = function.AsClosure();
        var newEnv = closure.Env.DeepClone();

        if (call.Arguments.Count != closure.Parameters.Count)
            throw new Exception("Length not right");

        for (int i = 0; i < closure.Parameters.Count; i++)
        {
            var param = closure.Parameters[i];
            var argument = call.Arguments[i];
            var argumentValue = Handle(argument, env);

            newEnv.Objects.Add(param, argumentValue);
        }

        return Handle(closure.Body, newEnv);
    }

    private static InterpretationResult HandleFunction(Function func, InterpretationEnvironment env)
    {
        return InterpretationResult.From("closure", new Closure() { Body = func.Value, Env = env, Parameters = func.Parameters.Select(x => x.Text).ToList() });
    }
}