using Microsoft.AspNetCore.Components;
using NikeFrontend.Data;
using NikeFrontend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NikeFrontend.Pages
{
    public partial class TeamMember
    {
        [Inject]
        public IHttpClientFactory _clientFactory { get; set; }
        [Inject]
        public TeamMemberService _teamMemberService { get; set; }

        public TeamMemberModelRootobject listTeamMemberResult { get; set; }
        public List<TeamMemberModel> listTeamMember { get; set; }
        public string teamMemberApiMessage { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            Console.WriteLine("Product - OnAfterRenderAsync - firstRender = " + firstRender);

            if (firstRender)
            {
                await getListTeamMember();
                StateHasChanged();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        public async Task getListTeamMember()
        {
            listTeamMemberResult = await _teamMemberService.getListTeamMember();
            if (listTeamMemberResult.succeeded)
            {
                listTeamMember = listTeamMemberResult.data;
            }
            else
            {
                teamMemberApiMessage = "Bạn không có quyền xem nội dung này";
            }
            
        }
    }
}
