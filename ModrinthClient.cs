using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ModrinthSharp
{
    public class ModrinthClient
    {
        private const string BaseUrl = "https://api.modrinth.com/v2";
        private readonly HttpClient _client;

        public ModrinthClient()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.UserAgent.ParseAdd("ModrinthSharp");
        }
        
        
        public async Task<T> GetAsync<T>(string endpoint)
        {
            
            var response = await _client.GetAsync(BaseUrl + endpoint);
            if (!response.IsSuccessStatusCode) throw new ModrinthApiException($"Request to {endpoint} failed with status code {response.StatusCode}: {response.ReasonPhrase}");
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}