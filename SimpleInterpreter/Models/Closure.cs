using SimpleCompiler.Models.Terms.Shared;

namespace SimpleCompiler.Models;

public class Closure
{
    public Term Body { get; set; }
    public List<string> Parameters { get; set; }
    public InterpretationEnvironment Env { get; set; }
}
