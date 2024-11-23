using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StremoCloud.Shared.Request
{
    public class GenerateOtpRequest
    {
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
