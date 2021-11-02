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

        public ListUserDataRoot userDataResult { get; set; }
        public List<UserData> listUserData { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {

            if (firstRender)
            {
                await getListUser();
                StateHasChanged();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        public async Task getListUser()
        {
            userDataResult = await _userService.GetAllUsers();
            listUserData = userDataResult.data;
        }
    }
}
