using SimpleCompiler.Models.Terms.Shared;

namespace SimpleCompiler.Models;

public class File
{
    public Term Expression { get; set; }
    public Loc Location { get; set; }
}
