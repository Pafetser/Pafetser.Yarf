using System.Diagnostics.CodeAnalysis;

namespace Pafetser.Yarf;

public record Warning : Message
{
    public Warning() : base()
    {
    }
    [SetsRequiredMembers]
    public Warning(string description, string? code = default) : base(description, code)
    {
    }

    public static implicit operator Warning(string description) => new Warning(description);
    public static implicit operator string?(Warning warning) => warning?.ToString();
    public override string ToString()
    {
        return base.ToString();
    }
}