using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pafetser.Yarf;

public record Information : Message
{
    public Information() : base()
    {
    }
    [SetsRequiredMembers]
    public Information(string description, string? code = default) : base(description, code)
    {
    }
    public static implicit operator Information(string description) => new Information(description);
    public static implicit operator string?(Information information) => information?.ToString();
    public override string ToString()
    {
        return base.ToString();
    }
}