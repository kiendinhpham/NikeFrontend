using NikeFrontend.Data;
using System.Threading.Tasks;

namespace NikeFrontend.Services
{
    public interface IUserService
    {
        public Task<UserRootobject> LoginAsync(User user);
        public Task<UserDataFromTokenRoot> GetUserByAccessTokenAsync(string token);
    }
}