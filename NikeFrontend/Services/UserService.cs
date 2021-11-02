using Blazored.SessionStorage;
using Newtonsoft.Json;
using NikeFrontend.Data;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace NikeFrontend.Services
{
    public class UserService : IUserService
    {
        public HttpClient _httpClient { get; }
        private readonly IHttpClientFactory _clientFactory;
        private readonly ISessionStorageService _sessionStorageService;

        public UserService(HttpClient httpClient, 
            IHttpClientFactory ClientFactory,
            ISessionStorageService SessionStorageService)
        {
            _httpClient = httpClient;
            _clientFactory = ClientFactory;
            _sessionStorageService = SessionStorageService;
        }

        public async Task<UserDataRoot> LoginAsync(UserData user)
        {
            string serializedUser = JsonConvert.SerializeObject(user);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Login");
            requestMessage.Content = new StringContent(serializedUser);

            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseBody = await response.Content.ReadAsStringAsync();

            var returnedUser = JsonConvert.DeserializeObject<UserDataRoot>(responseBody);

            return await Task.FromResult(returnedUser);
        }

        public async Task<UserDataRoot> GetUserByAccessTokenAsync(string token)
        {
            StringToken stringToken = new StringToken();
            stringToken.Token = token;

            string serializedToken = JsonConvert.SerializeObject(stringToken);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Login/check");
            requestMessage.Content = new StringContent(serializedToken);

            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseBody = await response.Content.ReadAsStringAsync();

            var returnedUser = JsonConvert.DeserializeObject<UserDataRoot>(responseBody);

            return await Task.FromResult(returnedUser);
        }

        public async Task<ListUserDataRoot> GetAllUsers()
        {
            ListUserDataRoot listUserDataRoot = new ListUserDataRoot();
            var token = await _sessionStorageService.GetItemAsync<string>("token");
            var client = _clientFactory.CreateClient("KSC");
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            try
            {
                listUserDataRoot = await client.GetFromJsonAsync<ListUserDataRoot>("Login");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error:{ex.Message}");
            }
            
            return await Task.FromResult(listUserDataRoot);
        }
    }
}