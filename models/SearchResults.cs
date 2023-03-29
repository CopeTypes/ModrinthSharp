using System.Collections.Generic;
using Newtonsoft.Json;

namespace ModrinthSharp
{
    public class SearchResults
    {
        [JsonProperty("hits")] public List<SearchResult> Results { get; set; }
        [JsonProperty("offset")] public int Offset { get; set; }
        [JsonProperty("limit")] public int Limit { get; set; }
        [JsonProperty("total_hits")] public int TotalResults { get; set; }
    }
}