using SimpleCompiler.Models.Terms.Shared;

namespace SimpleCompiler.Models
{
    public class InterpretationResult
    {
        public string Kind { get; set; }
        public object Value { get; set; }

        public bool AsBool() => (bool)Value;
        public int AsInt() => (int)Value;
        public string AsString() => (string)Value;
        public Closure AsClosure() => (Closure)Value;

        public static InterpretationResult From(string kind, object value) => new() { Kind = kind, Value = value };
    }
}
