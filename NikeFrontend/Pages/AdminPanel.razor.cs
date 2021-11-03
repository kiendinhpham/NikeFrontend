using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using NikeFrontend.Data;
using NikeFrontend.Services;
using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace NikeFrontend.Pages
{
    public partial class AdminPanel
    {
        [Inject]
        public IUserService _userService { get; set; }
        [Inject]
        public IToastService _toastService { get; set; }

        public ListUserDataRoot userDataResult { get; set; }
        public List<UserData> listUserData { get; set; }
        public ListRoleDataRoot roleDataResult { get; set; }
        public List<Role> listRoleData { get; set; }

        private UserData newUser = new UserData();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {

            if (firstRender)
            {
                await getListUser();
                await getListRole();
                StateHasChanged();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        public async Task getListUser()
        {
            userDataResult = await _userService.GetAllUsers();
            listUserData = userDataResult.data;
        }
        public async Task getListRole()
        {
            roleDataResult = await _userService.GetAllRoles();
            listRoleData = roleDataResult.data;
        }

        public async Task addProduct()
        {
            HttpResponseMessage response = await _userService.AddUser(newUser);
            Console.WriteLine(response);
            if (response.IsSuccessStatusCode)
            {
                await getListUser();
                newUser = new UserData();
                _toastService.ShowSuccess("New user added");
            }
            else
            {
                _toastService.ShowError("There was an error");
            }
        }
    }
}
