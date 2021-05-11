using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApiCRUDDemo.EmployeeData;
using RestApiCRUDDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiCRUDDemo.Controllers
{
    /// <summary>
    /// Gets all the employees of the company.
    /// </summary>
    /// <returns>List of employees...</returns>
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private IEmployeeData _employeeData;

        public EmployeesController(IEmployeeData employeeData)
        {
            _employeeData = employeeData;
        }

        /// <summary>
        /// Gets all the employees of the company.
        /// </summary>
        [Route("GetEmployeesData")]
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            return Ok(await _employeeData.GetEmployeesAsync());
        }

        /// <summary>
        /// Gets name of the developer
        /// </summary>
        [Route("GetDeveloperUserName")]
        [HttpGet]
        public List<string> GetNames()
        {
            List<string> output = new List<string>();

            output.Add("Israel Zapata (israelz330)");

            return output;
        }

        /// <summary>
        /// Gets single employee by entering its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Single Employee</returns>
        [Route("GetById/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetEmployeeAsync(Guid id)
        {
            var employee = await _employeeData.GetEmployeeAsync(id);

            if (employee != null)
            {
                return Ok(employee);
            }

            return NotFound($"Employee with Id: {id} was not found");
        }

        /// <summary>
        /// Add a new employee to the system. Only needs a name.
        /// </summary>
        /// <param name="employee">Name of the new employee...</param>
        /// <returns></returns>
        [Route("AddNew")]
        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            if (employee.Name == null)
            {
                return NotFound("Error with employee name (Cannot ve NULL)");
            }
            _employeeData.AddEmployee(employee);

            await _employeeData.SaveChangesAsync();

            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + employee.Id, employee);
        }

        /// <summary>
        /// Delete employee object from DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns>HTTP result code</returns>
        [Route("DeleteEmployee/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var existingEmployee = await _employeeData.GetEmployeeAsync(id);

            if (existingEmployee != null)
            {
                _employeeData.DeleteEmployee(existingEmployee);
                await _employeeData.SaveChangesAsync();
                return Ok();
            }

            return NotFound("The emplpoyee was not found");
        }

        /// <summary>
        /// Edits the name of an existing Employee
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employee"></param>
        /// <returns>Ok HTTP code result or NotFound</returns>
        [Route("EditEmployee/{id}")]
        [HttpPatch]
        public async Task <IActionResult> EditEmployee(Guid id, Employee employee)
        {
            var existingEmployee = await _employeeData.GetEmployeeAsync(id);

            if (existingEmployee != null)
            {
                employee.Id = existingEmployee.Id;
                _employeeData.EditEmployee(employee);
                await _employeeData.SaveChangesAsync();
                return Ok();
            }

            return NotFound("ERROR, NOT FOUND :(");
        }

    }
}
