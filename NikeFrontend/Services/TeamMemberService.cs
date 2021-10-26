using Blazored.SessionStorage;
using Microsoft.AspNetCore.Mvc;
using NikeFrontend.Data;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace NikeFrontend.Services
{
    public class TeamMemberService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ISessionStorageService _sessionStorageService;

        public TeamMemberService(IHttpClientFactory ClientFactory,
            ISessionStorageService SessionStorageService)
        {
            _clientFactory = ClientFactory;
            _sessionStorageService = SessionStorageService;
        }

        public async Task<TeamMemberModelRootobject> getListTeamMember()
        {
            TeamMemberModelRootobject listTeamMember = new TeamMemberModelRootobject();
            var client = _clientFactory.CreateClient("KSC_auth");
            var token = await _sessionStorageService.GetItemAsync<string>("token");
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            try
            {
                var result = await client.GetFromJsonAsync<TeamMemberModelRootobject>("TeamMember");
                listTeamMember = result;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error:{ex.Message}");
            }
            return await Task.FromResult(listTeamMember);
        }
    }
}