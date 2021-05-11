using RestApiCRUDDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiCRUDDemo.EmployeeData
{
    public class MockEmployeeData : IEmployeeData
    {

        private List<Employee> employees = new List<Employee>()
        {
            new Employee()
            {
                Id = Guid.NewGuid(),
                Name = "Israel Zapata"
            },

             new Employee()
            {
                Id = Guid.NewGuid(),
                Name = "Rodrigo"
            },

             new Employee()
            {
                Id = Guid.NewGuid(),
                Name = "Mercedes"
            }
        };

        public Employee AddEmployee(Employee employee)
        {
            employee.Id = Guid.NewGuid();
            employees.Add(employee);

            return employee;
        }

        public void DeleteEmployee(Employee employee)
        {
            employees.Remove(employee);
        }

        public Employee EditEmployee(Employee employee)
        {
            var existingEmployee = GetEmployee(employee.Id);
            existingEmployee.Name = employee.Name;
            return existingEmployee;
        }

        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Employee GetEmployee(Guid id)
        {
            return employees.SingleOrDefault(x => x.Id == id);        
        
        }

        public Task<Employee> GetEmployeeAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetEmployees()
        {
            return employees;
        }

        public Task<List<Employee>> GetEmployeesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
