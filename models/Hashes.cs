using Newtonsoft.Json;

namespace ModrinthSharp
{
    public class Hashes
    {
        [JsonProperty("sha512")] public string Sha512 { get; set; }
        [JsonProperty("sha1")] public string Sha1 { get; set; }
    }
}