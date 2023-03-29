using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ModrinthSharp
{
    public class APIClient
    {
        private const string BaseUrl = "https://api.modrinth.com/v2";
        private readonly HttpClient _client;

        public APIClient()
        {
            _client = new HttpClient { BaseAddress = new Uri(BaseUrl) };
        }
        
        
        public async Task<T> GetAsync<T>(string endpoint)
        {
            var response = await _client.GetAsync($"/{endpoint}");
            if (!response.IsSuccessStatusCode) throw new WebException(); // todo better error handling
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}