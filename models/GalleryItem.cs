﻿using Newtonsoft.Json;

namespace ModrinthSharp
{
    public class GalleryItem
    {
        [JsonProperty("url")] public string Url { get; set; }
        [JsonProperty("featured")] public bool Featured { get; set; }
        [JsonProperty("title")] public string Title { get; set; }
        [JsonProperty("description")] public string Description { get; set; }
        [JsonProperty("created")] public string Created { get; set; }
        [JsonProperty("ordering")] public int Ordering { get; set; }
    }
}