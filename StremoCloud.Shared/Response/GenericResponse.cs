using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StremoCloud.Shared.Response;

public class GenericResponse<T> 
{
    public T Data { get; set; }
    public string Message { get; set; }
    public bool IsSuccess { get; set; }
}
