using Newtonsoft.Json;

namespace ModrinthSharp
{
    public class ModFile
    {
        [JsonProperty("hashes")] public Hashes Hashes { get; set; }
        [JsonProperty("url")] public string Url { get; set; }
        [JsonProperty("filename")] public string Filename { get; set; }
        [JsonProperty("primary")] public bool Primary { get; set; }
        [JsonProperty("size")] public int Size { get; set; }
        [JsonProperty("file_type")] public string FileType { get; set; }
    }
}