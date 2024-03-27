using Application.Models.Authorizations;

namespace Application.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(LoginRequest loginRequest);
    }
}
