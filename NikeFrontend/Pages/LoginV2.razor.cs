using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using NikeFrontend.Data;
using NikeFrontend.Services;
using System.Threading.Tasks;

namespace NikeFrontend.Pages
{
    public partial class LoginV2
    {
        [Inject]
        public IUserService _userService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        public Blazored.SessionStorage.ISessionStorageService sessionStorage { get; set; }

        private User user = new User();
        private bool isTaskRunning = false;
        public string LoginMesssage { get; set; }
        public string loginButtonText = "Đăng Nhập";

        private async Task<bool> CheckUser()
        {
            LoginMesssage = "";
            isTaskRunning = true;
            loginButtonText = "Vui lòng chờ...";
            var returnUser = await _userService.LoginAsync(user);

            if (returnUser.succeeded)
            {
                await sessionStorage.SetItemAsync("userName", user.userName);
                await sessionStorage.SetItemAsync("token", returnUser.data.token);

                ((CustomAuthenticationStateProvider)AuthenticationStateProvider).MarkUserAsAuthenticated(user.userName);
                NavigationManager.NavigateTo("/", true);
            }
            else
            {
                isTaskRunning = false;
                loginButtonText = "Đăng Nhập";
                LoginMesssage = "Tài khoản hoặc mật khẩu sai";
            }

            return await Task.FromResult(true);
        }
    }
}