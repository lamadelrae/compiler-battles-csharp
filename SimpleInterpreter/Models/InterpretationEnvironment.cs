namespace SimpleCompiler.Models
{
    public class InterpretationEnvironment
    {
        public Dictionary<string, InterpretationResult> Objects { get; set; }

        public InterpretationEnvironment DeepClone() => new()
        {
            Objects = Objects.ToDictionary(k => k.Key, v => v.Value)
        };
    }
}
