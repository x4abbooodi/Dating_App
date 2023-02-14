using API.Entities;
namespace Api.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
