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
        
        public async Task<SearchResults> Search(string query, IndexType indexType, int limit = 10, int offset = 0,
            List<Facet> facets = null)
        {
            if (string.IsNullOrEmpty(query)) throw new ModrinthApiException("Invalid query");
            var endpoint = $"/search?query={Uri.UnescapeDataString(query)}&index={indexType.ToString().ToLower()}&limit={limit}&offset={offset}";
            if (facets != null) endpoint += $"&facets={Facet.ToJsArray(facets)}";

            return await _modrinthClient.GetAsync<SearchResults>(endpoint);
        }
        
        // Version stuff
        public async Task<Version> GetVersionFromHash(string hash)
        {
            return await _modrinthClient.GetAsync<Version>($@"/version_file/{hash}");
        }

        public async Task<List<Version>> GetVersions(string id)
        {
            if (!await CheckProject(id)) throw new ModrinthApiException("Project not found.");
            VersionSearch vs = await _modrinthClient.GetAsync<VersionSearch>($@"/project/{id}/version");
            if (vs == null) throw new ModrinthApiException("Error parsing version information.");
            return vs.Versions;
        }

        public async Task<Version> GetLatestVersion(string id)
        {
            var versions = await GetVersions(id);
            return versions.First();
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