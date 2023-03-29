using System.Collections.Generic;
using Newtonsoft.Json;

namespace ModrinthSharp
{
    public class DependencySearch
    {
        [JsonProperty("projects")] public List<Project> ModDependencies { get; set; }
        [JsonProperty("versions")] public List<Version> VersionDependencies { get; set; }
    }
}