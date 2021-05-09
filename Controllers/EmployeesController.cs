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
        [Route("api/[controller]/GetEmployeesData")]
        [HttpGet]
        public IActionResult GetEmployees()
        {
            return Ok(_employeeData.GetEmployees());
        }

        /// <summary>
        /// Get only the employee names
        /// </summary>
        /// <returns>A list with the names of the Employees</returns>
        [Route("api/[controller]/GetEmployeeNamesOnly")]
        [HttpGet]
        public List<string> GetOrderedEmployees()
        {
            List<string> orderedList = new List<string>();
            foreach (var item in _employeeData.GetEmployees())
            {
                orderedList.Add(item.Name);
            }

            orderedList.Sort();

            return orderedList;
        }

        /// <summary>
        /// Gets all the employees of the company.
        /// </summary>
        [Route("api/[controller]/GetAdminNames")]
        [HttpGet]
        public List<string> GetNames()
        {
            List<string> output = new List<string>();

            output.Add("Israel");
            output.Add("Mercedes");
            output.Add("Rodrigo");

            return output;
        }

        /// <summary>
        /// Gets a single employee given its Id.
        /// </summary>
        /// <param name="id">The unique identifier fot this employee</param>
        /// <returns></returns>
        [Route("api/[controller]/GetByID/{id}")]
        [HttpGet]
        public IActionResult GetEmployee(Guid id)
        {
            var employee = _employeeData.GetEmployee(id);

            if (employee != null)
            {
                return Ok(employee);
            }

            return NotFound($"Employee with Id: {id} was not found");
        }

        [Route("api/[controller]/GetByIDAsync/{id}")]
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
        [Route("api/[controller]/AddNew")]
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
        [Route("api/[controller]/DeleteEmployee/{id}")]
        [HttpDelete]
        public IActionResult DeleteEmployee(Guid id)
        {
            var existingEmployee = _employeeData.GetEmployee(id);

            if (existingEmployee != null)
            {
                _employeeData.DeleteEmployee(existingEmployee);
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
        [Route("api/[controller]/EditEmployee/{id}")]
        [HttpPatch]
        public IActionResult EditEmployee(Guid id, Employee employee)
        {
            var existingEmployee = _employeeData.GetEmployee(id);

            if (existingEmployee != null)
            {
                employee.Id = existingEmployee.Id;
                _employeeData.EditEmployee(employee);
                return Ok();
            }

            return NotFound("ERROR, NOT FOUND :(");
        }

    }
}
