using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ModrinthSharp.util;


namespace ModrinthSharp
{
    public class API
    {

        private APIClient _apiClient;

        public API()
        {
            _apiClient = new APIClient();
        }

        private async Task<bool> CheckProject(string id)
        {
            try
            {
                var check = await _apiClient.GetAsync<ProjectCheck>($@"/project/{id}/check");
                return !string.IsNullOrEmpty(check.Id);
            }
            catch (WebException)
            {
                return false;
            }
        }
        
        
        public async Task<Project> GetProjectById(string id)
        {
            if (!await CheckProject(id)) throw new APIException("Project not found.");
            return await _apiClient.GetAsync<Project>($@"/project/{id}");
        }
        
        public async Task<Project> GetProjectBySlug(string slug)
        {
            return await GetProjectById(slug); // goes to same endpoint
        }

        // todo need to make a search method that uses filters
        
        public async Task<SearchResults> Search(string query, IndexType indexType, int limit = 10, int offset = 0,
            List<Facet> facets = null)
        {
            if (string.IsNullOrEmpty(query)) throw new APIException("Invalid query");
            var endpoint = $"/search?query={Uri.UnescapeDataString(query)}&index={indexType.ToString().ToLower()}&limit={limit}&offset={offset}";
            if (facets != null) endpoint += $"&facets={Facet.ToJsArray(facets)}";

            return await _apiClient.GetAsync<SearchResults>(endpoint);
        }
        
        public async Task<Version> GetVersionFromHash(string hash)
        {
            return await _apiClient.GetAsync<Version>($@"/version_file/{hash}");
        }


        private async Task<DependencySearch> GetDependencies(string id)
        {
            if (!await CheckProject(id)) throw new APIException("Project not found.");
            return await _apiClient.GetAsync<DependencySearch>($@"/project/{id}/dependencies");
        }

        public async Task<DependencySearch> GetDependencies(Project project)
        {
            return await GetDependencies(project.Id);
        }

    }
}