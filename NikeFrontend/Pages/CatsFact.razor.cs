using Microsoft.AspNetCore.Components;
using NikeFrontend.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace NikeFrontend.Pages
{
    public partial class CatsFact
    {
        [Inject]
        public IHttpClientFactory _clientFactory { get; set; }

        public CatsfactModel catsFact { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await GetCatsFact();
        }

        public async Task GetCatsFact()
        {
            var client = _clientFactory.CreateClient();
            catsFact = await client.GetFromJsonAsync<CatsfactModel>("https://catfact.ninja/fact");
        }
    }
}
