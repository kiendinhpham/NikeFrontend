using NikeFrontend.Data;
using System.Net.Http;
using System.Threading.Tasks;

namespace NikeFrontend.Services
{
    public interface IUserService
    {
        public Task<UserDataRoot> LoginAsync(UserData user);
        public Task<UserDataRoot> GetUserByAccessTokenAsync(string token);
        public Task<ListUserDataRoot> GetAllUsers();
        public Task<ListRoleDataRoot> GetAllRoles();
        public Task<HttpResponseMessage> AddUser(UserData user);
        public Task<HttpResponseMessage> DeleteUser(string id);
    }
}