using CS.Staff.Models;
using CS.Staff.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CS.Staff.ApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Employee>> GetEmployeesAsync(string filter)
        {
            return await employeeRepository.GetItemsAsync(filter).ConfigureAwait(false);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Employee> GetEmployeeById(string id, [FromQuery][Required] string partitionKey)
        {
            return await employeeRepository.FindItemAsync(id, partitionKey).ConfigureAwait(false);
        }

        [HttpPost]
        public async Task<Employee> CreateEmployeeAsync([FromBody] Employee employee)
        {

            return await employeeRepository.AddItemAsync(employee, employee.Email).ConfigureAwait(false);
        }

        [HttpPut]
        public async Task<Employee> UpdateEmployeeAsync([FromBody] Employee employee)
        {
            return await employeeRepository.UpdateItemAsync(employee, employee.Id, employee.Etag, employee.Email).ConfigureAwait(false);
        }

        [HttpDelete]
        public async Task<bool> RemoveEmployeeAsync(string id, [FromQuery][Required] string partitionKey)
        {
            return await employeeRepository.RemoveItemAsync(id, partitionKey).ConfigureAwait(false);
        }
    }
}
