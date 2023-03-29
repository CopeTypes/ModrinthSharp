using System.Collections.Generic;
using Newtonsoft.Json;

namespace ModrinthSharp
{
    public class SearchResult
    {
        [JsonProperty("slug")] public string Slug { get; set; }
        [JsonProperty("title")] public string Title { get; set; }
        [JsonProperty("description")] public string Description { get; set; }
        [JsonProperty("categories")] public List<string> Categories { get; set; }
        [JsonProperty("client_side")] public string ClientSide { get; set; }
        [JsonProperty("server_side")] public string ServerSide { get; set; }
        [JsonProperty("project_type")] public string ProjectType { get; set; }
        [JsonProperty("downloads")] public int Downloads { get; set; }
        [JsonProperty("icon_url")] public string IconUrl { get; set; }
        [JsonProperty("color")] public int Color { get; set; }
        [JsonProperty("project_id")] public string ProjectId { get; set; }
        [JsonProperty("author")] public string Author { get; set; }
        [JsonProperty("display_categories")] public List<string> DisplayCategories { get; set; }
        [JsonProperty("versions")] public List<string> Versions { get; set; }
        [JsonProperty("follows")] public int Follows { get; set; }
        [JsonProperty("date_created")] public string DateCreated { get; set; }
        [JsonProperty("date_modified")] public string DateModified { get; set; }
        [JsonProperty("latest_version")] public string LatestVersion { get; set; }
        [JsonProperty("license")] public string LicenseType { get; set; }
        [JsonProperty("gallery")] public List<string> Gallery { get; set; }
        [JsonProperty("featured_gallery")] public string FeaturedGallery { get; set; }
    }
}