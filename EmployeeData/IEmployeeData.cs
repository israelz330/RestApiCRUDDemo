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
        Task<List<Employee>> GetEmployeesAsync();
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

        #region SaveChanges

        Task<bool> SaveChangesAsync();


            #endregion
    }
}
