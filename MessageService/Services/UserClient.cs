namespace MessageService.Services
{
    public class UserClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public UserClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<bool> IsValidUserAsync(long userId)
        {
            var response = await _httpClient.GetAsync($"{_config["UserService:BaseUrl"]}/api/users/{userId}");
            return response.IsSuccessStatusCode;
        }
    }


}
