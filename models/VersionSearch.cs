using System.Collections.Generic;
using Newtonsoft.Json;

namespace ModrinthSharp
{
    public class VersionSearch
    {
        [JsonProperty("")] public List<Version> Versions { get; set; }
    }
}