using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiCRUDDemo.Model
{
    public class EmployeeContext : DbContext
    {

        /*
         * This gives us a constructor and also calls the base constructor with the passed options
         */
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {

        }
        //DbSet represents the set of entities.In a database, a group of similar entities is called an Entity Set.
        //The DbSet enables the user to perform various operations like add, remove, update, etc.on the entity set.
        //This will act as a Table
        public DbSet<Employee> Employees { get; set; }

    }
}
