﻿using Newtonsoft.Json;

namespace ModrinthSharp
{
    public class Dependency
    {
        [JsonProperty("version_id")] public string VersionId { get; set; }
        [JsonProperty("project_id")] public string ProjectId { get; set; }
        [JsonProperty("file_name")] public string FileName { get; set; }
        [JsonProperty("dependency_type")] public string DependencyType { get; set; }
    }
}