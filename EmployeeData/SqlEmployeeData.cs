using RestApiCRUDDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RestApiCRUDDemo.EmployeeData
{
    public class SqlEmployeeData : IEmployeeData
    {
        private readonly EmployeeContext _employeeContext;


        public SqlEmployeeData(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }
        public Employee AddEmployee(Employee employee)
        {
            employee.Id = Guid.NewGuid();
            _employeeContext.Employees.Add(employee);
            return employee;
        }

        public void DeleteEmployee(Employee employee)
        {
            var existingEmployee = _employeeContext.Employees.Find(employee.Id);

            if (existingEmployee == null) return;
            _employeeContext.Employees.Remove(existingEmployee);
            //_employeeContext.SaveChanges();

        }

        public Employee EditEmployee(Employee employee)
        {
            var existingEmployee = _employeeContext.Employees.Find(employee.Id);

            if (existingEmployee != null)
            {
                existingEmployee.Name = employee.Name;
                _employeeContext.Employees.Update(existingEmployee);
            }

            return employee;
        }

        public async Task<Employee> GetEmployeeAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }

            //var employee = _employeeContext.Employees.Find(id);
            var employee = await _employeeContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            return employee;
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await _employeeContext.Employees.ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _employeeContext.SaveChangesAsync() > 0;
        }
    }
}
