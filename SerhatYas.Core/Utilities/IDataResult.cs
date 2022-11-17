using System;
using System.Collections.Generic;
using System.Text;

namespace SerhatYas.Core.Utilities
{
    public interface IDataResult<out T> : IResult
    {
        T Data { get; }
    }
}
