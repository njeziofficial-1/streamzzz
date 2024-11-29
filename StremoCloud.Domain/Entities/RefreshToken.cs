using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StremoCloud.Domain.Entities;

public class RefreshToken
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string RefreshTokenValue { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateExpires { get; set; }

}
