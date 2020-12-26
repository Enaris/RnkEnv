using EnvRnk.DataAccess.DbModels;

namespace EnvRnk.Services.Services
{
    public interface ITokenService
    {
        string GenerateJwtToken(AspUser user);
    }
}