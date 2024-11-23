using System.Security.Claims;

namespace StremoCloud.Shared.Helpers;

    public interface ITokenHelper
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
