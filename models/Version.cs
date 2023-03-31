using System.Collections.Generic;
using Newtonsoft.Json;

namespace ModrinthSharp
{
    public class Version
    {
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("version_number")] public string VersionNumber { get; set; }
        [JsonProperty("changelog")] public string Changelog { get; set; }
        [JsonProperty("dependencies")] public List<Dependency> Dependencies { get; set; }
        [JsonProperty("game_versions")] public List<string> GameVersions { get; set; }
        [JsonProperty("version_type")] public string VersionType { get; set; }
        [JsonProperty("loaders")] public List<string> Loaders { get; set; }
        [JsonProperty("featured")] public bool Featured { get; set; }
        [JsonProperty("status")] public string Status { get; set; }
        [JsonProperty("requested_status")] public string RequestedStatus { get; set; }
        [JsonProperty("id")] public string Id { get; set; }
        [JsonProperty("project_id")] public string ProjectId { get; set; }
        [JsonProperty("author_id")] public string AuthorId { get; set; }
        [JsonProperty("date_published")] public string DatePublished { get; set; }
        [JsonProperty("downloads")] public int Downloads { get; set; }
        [JsonProperty("changelog_url")] public object ChangelogUrl { get; set; }
        [JsonProperty("files")] public List<ModFile> Files { get; set; }
    }
}