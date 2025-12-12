using FileStack.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace FileStack.Application.Interfaces
{
    public interface ItokenHandler
    {
        Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user);

    }
}
