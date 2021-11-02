using NikeFrontend.Data;
using System.Threading.Tasks;

namespace NikeFrontend.Services
{
    public interface IUserService
    {
        public Task<UserDataRoot> LoginAsync(UserData user);
        public Task<UserDataRoot> GetUserByAccessTokenAsync(string token);

    }
}