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
                UserDataFromTokenRoot user = await _userService.GetUserByAccessTokenAsync(token);
                identity = GetClaimsIdentity(user);
            }
            else
            {
                identity = new ClaimsIdentity();
            }

            var claimsPrincipal = new ClaimsPrincipal(identity);

            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }

        private ClaimsIdentity GetClaimsIdentity(UserDataFromTokenRoot user)
        {
            var claimsIdentity = new ClaimsIdentity();

            if (user.data.email != null)
            {
                claimsIdentity = new ClaimsIdentity(new[]
                                {
                                    new Claim(ClaimTypes.NameIdentifier, user.data.email),
                                }, "apiauth_type");
            }

            return claimsIdentity;
        }

        public void MarkUserAsAuthenticated(string emailAddress)
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, emailAddress),
            }, "apiauth_type");

            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public void MarkUserAsLoggedOut()
        {
            _sessionStorageService.RemoveItemAsync("email");
            _sessionStorageService.RemoveItemAsync("token");

            var identity = new ClaimsIdentity();

            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
    }
}