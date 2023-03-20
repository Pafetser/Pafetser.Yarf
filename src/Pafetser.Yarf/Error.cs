using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pafetser.Yarf;

public record Error : Message
{
    public Error() : base()
    {
    }
    [SetsRequiredMembers]
    public Error(string description, string? code = default) : base(description, code)
    {
    }
    public static implicit operator Error(string description) => new Error(description);
    public static implicit operator string?(Error error) => error?.ToString();
    public override string ToString()
    {
        return base.ToString();
    }
}