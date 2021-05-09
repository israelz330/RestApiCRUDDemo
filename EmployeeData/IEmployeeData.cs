using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestApiCRUDDemo.Model;

namespace RestApiCRUDDemo.EmployeeData
{
    public interface IEmployeeData
    {
        #region GetEmployee/s
        List<Employee> GetEmployees();
        Employee GetEmployee(Guid id);
        Task<Employee> GetEmployeeAsync(Guid id);
        #endregion

        #region AddEmployee
        Employee AddEmployee(Employee employee);
        #endregion

        #region DeleteEmployee
        void DeleteEmployee(Employee employee);
        #endregion

        #region EditEmployee
        Employee EditEmployee(Employee employee);
        #endregion

        Task<bool> SaveChangesAsync();
    }

}
