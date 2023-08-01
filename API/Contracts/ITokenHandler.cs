using System.Security.Claims;

namespace API.Contracts;

public interface ITokenHandler
{
    string GenereateToken(IEnumerable<Claim> claims);
}
