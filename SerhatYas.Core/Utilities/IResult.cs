using System;
using System.Collections.Generic;
using System.Text;

namespace SerhatYas.Core.Utilities
{
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }
    }
}
