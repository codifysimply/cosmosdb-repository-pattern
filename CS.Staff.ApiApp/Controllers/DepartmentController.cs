using CS.Staff.Models;
using CS.Staff.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace CS.Staff.ApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Department>> GetDepartmentsAsync(string filter)
        {
            return await departmentRepository.GetItemsAsync(filter).ConfigureAwait(false); 
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Department> GetDepartmentById(string id, [FromQuery][Required] string partitionKey)
        {
            return await departmentRepository.FindItemAsync(id, partitionKey).ConfigureAwait(false);
        }

        [HttpPost]
        public async Task<Department> CreateDepartmentAsync([FromBody] Department department)
        {
            if (department == null || department.Etag != null)
            {
                return null;
            }

            return await departmentRepository.AddItemAsync(department, department.Location).ConfigureAwait(false);
        }

        [HttpPut]
        public async Task<Department> UpdateDepartmentAsync([FromBody] Department department)
        {
            return await departmentRepository.UpdateItemAsync(department, department.Id,department.Etag, department.Location ).ConfigureAwait(false);
        }

        [HttpDelete] 
        public async Task<bool> RemoveDepartmentAsync(string id, [FromQuery][Required] string partitionKey)
        {
            return await departmentRepository.RemoveItemAsync(id, partitionKey).ConfigureAwait(false);
        }
    }
}
