using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ModrinthSharp.util;


namespace ModrinthSharp
{
    public class ModrinthApi
    {

        private ModrinthClient _modrinthClient;

        public ModrinthApi()
        {
            _modrinthClient = new ModrinthClient();
        }

        private async Task<bool> CheckProject(string id)
        {
            try
            {
                var check = await _modrinthClient.GetAsync<ProjectCheck>($@"/project/{id}/check");
                return !string.IsNullOrEmpty(check.Id);
            }
            catch (WebException)
            {
                return false;
            }
        }
        
        // Project stuff
        public async Task<Project> GetProjectById(string id)
        {
            if (!await CheckProject(id)) throw new ModrinthApiException("Project not found.");
            return await _modrinthClient.GetAsync<Project>($@"/project/{id}");
        }
        
        public async Task<Project> GetProjectBySlug(string slug)
        {
            return await GetProjectById(slug); // goes to same endpoint
        }

        public async Task<Project> GetProjectFromVersion(Version version)
        {
            if (!await CheckProject(version.ProjectId)) throw new ModrinthApiException("Project not found.");
            return await _modrinthClient.GetAsync<Project>($@"/project/{version.ProjectId}");
        }

        /// <summary>
        /// Get the download url for the project's latest version
        /// </summary>
        /// <param name="project">The project</param>
        /// <returns>A ModFile of the project's latest version.</returns>
        public async Task<ModFile> GetProjectDownload(Project project)
        {
            var latest = await GetLatestVersion(project.Id);
            return await GetPrimaryFileFromVersion(latest);
        }
        
        // search stuff
        // todo need to make a search method that uses filters
        
        /// <summary>
        /// Search Modrinth for a list of mods
        /// </summary>
        /// <param name="query">The main search query </param>
        /// <param name="indexType">How results are indexed</param>
        /// <param name="limit">The maximum amount of results to retrieve</param>
        /// <param name="offset">Start from a specific page</param>
        /// <param name="facets">A list of facets to filter results (ex: new Facet(FacetType.Versions, "1.19.4); )</param>
        /// <returns>A list of SearchResult objects</returns>
        /// <exception cref="ModrinthApiException"></exception>
        public async Task<SearchResults> Search(string query, IndexType indexType, int limit = 10, int offset = 0,
            List<Facet> facets = null)
        {
            if (string.IsNullOrEmpty(query)) throw new ModrinthApiException("Invalid query");
            var endpoint = $"/search?query={Uri.UnescapeDataString(query)}&index={indexType.ToString().ToLower()}&limit={limit}&offset={offset}";
            if (facets != null) endpoint += $"&facets={Facet.ToJsArray(facets)}";

            return await _modrinthClient.GetAsync<SearchResults>(endpoint);
        }
        
        // Version stuff
        
        /// <summary>
        /// Gets a version from a file hash
        /// </summary>
        /// <param name="hash">The hash of the file, either SHA1 or SHA512</param>
        /// <returns></returns>
        public async Task<Version> GetVersionFromHash(string hash)
        {
            return await _modrinthClient.GetAsync<Version>($@"/version_file/{hash}");
        }

        /// <summary>
        /// Gets a list of versions for a project
        /// </summary>
        /// <param name="id">The id of the project</param>
        /// <param name="loaders">Filter by Mod Loader type (fabric, forge, etc)</param>
        /// <param name="versions">Filter by Minecraft version</param>
        /// <returns></returns>
        /// <exception cref="ModrinthApiException"></exception>
        public async Task<List<Version>> GetVersions(string id, List<string> loaders = null, List<string> versions = null)
        {
            if (!await CheckProject(id)) throw new ModrinthApiException("Project not found.");
            var url = $@"/project/{id}/version?";
            if (loaders != null) url += "&loaders=" + Utils.ConvertToJsArray(loaders);
            if (versions != null) url += "&game_versions=" + Utils.ConvertToJsArray(versions);
            Console.WriteLine(url);
            VersionSearch vs = await _modrinthClient.GetAsync<VersionSearch>(url);
            if (vs == null) throw new ModrinthApiException("Error parsing version information.");
            return vs.ToList();
        }

        /// <summary>
        /// Gets the latest version for a project
        /// </summary>
        /// <param name="id">The id of the project</param>
        /// <param name="loaders">Filter by Mod Loader type (fabric, forge, etc)</param>
        /// <param name="versions">Filter by Minecraft version</param>
        /// <returns></returns>
        public async Task<Version> GetLatestVersion(string id, List<string> loaders = null, List<string> versions = null)
        {
            var versionList = await GetVersions(id, loaders, versions);
            return versionList.First();
        }

        /// <summary>
        /// Gets the "primary" ModFile for a version
        /// </summary>
        /// <param name="version">A project version</param>
        /// <returns>ModFile for the version's parent mod</returns>
        /// <exception cref="ModrinthApiException">No primary file exists, shouldn't ever happen</exception>
        public async Task<ModFile> GetPrimaryFileFromVersion(Version version)
        {
            return await Task.Run(() =>
            {
                foreach (var asset in version.Files.Where(asset => asset.Primary)) return asset;
                throw new ModrinthApiException("Version has no primary ModFile.");
            });
        }
        
        
        
        // dependency stuff
        
        /// <summary>
        /// Gets all the dependencies required by a project
        /// </summary>
        /// <param name="id">The project's id</param>
        /// <returns>A DependencySearch containing all dependencies required by the project</returns>
        /// <exception cref="ModrinthApiException">The specified project wasn't found</exception>
        private async Task<DependencySearch> GetDependencies(string id)
        {
            if (!await CheckProject(id)) throw new ModrinthApiException("Project not found.");
            return await _modrinthClient.GetAsync<DependencySearch>($@"/project/{id}/dependencies");
        }

        public async Task<DependencySearch> GetDependencies(Project project)
        {
            return await GetDependencies(project.Id);
        }

    }
}