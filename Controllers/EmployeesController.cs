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
        public async Task<IActionResult> GetEmployees()
        {
            return Ok(await _employeeData.GetEmployeesAsync());
        }

        /// <summary>
        /// Get only the employee names
        /// </summary>
        /// <returns>A list with the names of the Employees</returns>
        //[Route("api/[controller]/GetEmployeeNamesOnly")]
        //[HttpGet]
        //public async Task<List<string>> GetOrderedEmployees()
        //{
        //    List<string> orderedList = new List<string>();
        //    foreach (var item in _employeeData.GetEmployeesAsync())
        //    {
        //        orderedList.Add(item.Name);
        //    }

        //    orderedList.Sort();

        //    return orderedList;
        //}

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


        [Route("api/[controller]/GetById/{id}")]
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
        [Route("api/[controller]/EditEmployee/{id}")]
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
