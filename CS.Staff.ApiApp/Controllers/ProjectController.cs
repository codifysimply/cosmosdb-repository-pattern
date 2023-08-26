using CS.Staff.Models;
using CS.Staff.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CS.Staff.ApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository projectRepository;

        public ProjectController(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Project>> GetProjectsAsync(string filter)
        {
            return await projectRepository.GetItemsAsync(filter).ConfigureAwait(false);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Project> GetProjectById(string id, [FromQuery][Required] string partitionKey)
        {
            return await projectRepository.FindItemAsync(id, partitionKey).ConfigureAwait(false);
        }

        [HttpPost]
        public async Task<Project> CreateProjectAsync([FromBody] Project project)
        {

            return await projectRepository.AddItemAsync(project, project.Id).ConfigureAwait(false);
        }

        [HttpPut]
        public async Task<Project> UpdateProjectAsync([FromBody] Project project)
        {
            return await projectRepository.UpdateItemAsync(project, project.Id, project.Etag, project.Id).ConfigureAwait(false);
        }

        [HttpDelete]
        public async Task<bool> RemoveProjectAsync(string id, [FromQuery][Required] string partitionKey)
        {
            return await projectRepository.RemoveItemAsync(id, partitionKey).ConfigureAwait(false);
        }
    }
}
