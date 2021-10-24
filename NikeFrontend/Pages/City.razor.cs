using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using NikeFrontend.Data;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace NikeFrontend.Pages
{
    public partial class City
    {
        [Inject]
        public IHttpClientFactory _clientFactory { get; set; }

        [Inject]
        public ISessionStorageService _sessionStorageService { get; set; }

        public List<CityModel> listCities { get; set; }
        public List<District> listDistricts { get; set; }
        public CityModel city { get; set; }

        public string districtMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await Task.Run(() => getCity(1));
        }

        public async Task getListCities()
        {
            var client = _clientFactory.CreateClient("KSC_auth");
            try
            {
                var result = await client.GetFromJsonAsync<CityRootobject>("Cities");
                listCities = result.data;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public async Task getCity(int id)
        {
            var client = _clientFactory.CreateClient("KSC_auth");
            var token = await _sessionStorageService.GetItemAsync<string>("token");
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            try
            {
                var result = await client.GetFromJsonAsync<SingleCityRootobject>($"Cities/{id}");
                city = result.data;
                listDistricts = city.districts;
                districtMessage = "";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                districtMessage = ex.Message;
            }
        }
    }
}