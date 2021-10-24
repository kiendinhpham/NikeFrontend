using Newtonsoft.Json;
using NikeFrontend.Data;
using System.Net.Http;
using System.Threading.Tasks;

namespace NikeFrontend.Services
{
    public class UserService : IUserService
    {
        public HttpClient _httpClient { get; }

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserRootobject> LoginAsync(User user)
        {
            string serializedUser = JsonConvert.SerializeObject(user);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Login");
            requestMessage.Content = new StringContent(serializedUser);

            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseBody = await response.Content.ReadAsStringAsync();

            var returnedUser = JsonConvert.DeserializeObject<UserRootobject>(responseBody);

            return await Task.FromResult(returnedUser);
        }
    }
}