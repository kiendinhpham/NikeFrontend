using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using NikeFrontend.Data;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NikeFrontend.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ISessionStorageService _sessionStorageService;
        public IUserService _userService { get; set; }

        public CustomAuthenticationStateProvider(ISessionStorageService sessionStorageService,
            IUserService userService)
        {
            _sessionStorageService = sessionStorageService;
            _userService = userService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _sessionStorageService.GetItemAsync<string>("token");

            ClaimsIdentity identity;

            if (token != null && token != string.Empty)
            {
                UserDataRoot user = await _userService.GetUserByAccessTokenAsync(token);
                if (user.succeeded)
                {
                    identity = GetClaimsIdentity(user);
                }
                else
                {
                    identity = new ClaimsIdentity();
                }
                
            }
            else
            {
                identity = new ClaimsIdentity();
            }

            var claimsPrincipal = new ClaimsPrincipal(identity);

            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }

        private ClaimsIdentity GetClaimsIdentity(UserDataRoot user)
        {
            var claimsIdentity = new ClaimsIdentity();

            if (user.data.userName != null)
            {
                claimsIdentity = new ClaimsIdentity(new[]
                                {
                                    new Claim(ClaimTypes.Name, user.data.name),
                                    new Claim(ClaimTypes.Role, user.data.roles[0].name)
                                }, "apiauth_type");
            }

            return claimsIdentity;
        }

        public void MarkUserAsAuthenticated(string userName)
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, userName),
            }, "apiauth_type");

            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public void MarkUserAsLoggedOut()
        {
            _sessionStorageService.RemoveItemAsync("userName");
            _sessionStorageService.RemoveItemAsync("token");

            var identity = new ClaimsIdentity();

            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
    }
}