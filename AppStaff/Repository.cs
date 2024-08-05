using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using AppStaff.Models.Domains;
using AppStaff.Models.Converters;
using AppStaff.Models.Wrappers;

namespace AppStaff
{
    public class Repository
    {

        public List<Group> GetGroups()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Groups.ToList();
            }
        }

        public List<EmployeeWrapper> GetEmployees(int groupId)
        {
            using (var context = new ApplicationDbContext())
            {
                var employees = context
                    .Employees
                    .Include(x => x.Group)
                    .AsQueryable();

                if (groupId != 0)
                    employees = employees.Where(x => x.GroupId == groupId);


                return employees
                    .ToList()
                    .Select(x => x.ToWrapper())
                    .ToList();
            }
        }

        public void AddEmployee(EmployeeWrapper employeeWrapper)
        {
            var employee = employeeWrapper.ToDao();
            using (var context = new ApplicationDbContext())
            {
                var dbEmployee = context.Employees.Add(employee);
                employee.GroupId = 2;
                employee.FireDate = null;
                context.SaveChanges();
            }
        }

        public void UpdateEmployee(EmployeeWrapper employeeWrapper)
        {
            var employee = employeeWrapper.ToDao();

            using (var context = new ApplicationDbContext())
            {
                UpdateStudentProperties(context, employee);

                context.SaveChanges();


            }
        }

        private static void UpdateStudentProperties(ApplicationDbContext context, Employee employee)
        {
            var employeeToUpdate = context.Employees.Find(employee.Id);
            employeeToUpdate.Comments = employee.Comments;
            employeeToUpdate.FirstName = employee.FirstName;
            employeeToUpdate.LastName = employee.LastName;
            employeeToUpdate.GroupId = employee.GroupId;
            employeeToUpdate.Earnings = employee.Earnings;
            employeeToUpdate.EmploymentDate = employee.EmploymentDate;
            employeeToUpdate.FireDate = employee.EmploymentDate;
        }

        public void FireEmployee(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var studentToFire = context.Employees.Find(id);
                studentToFire.FireDate = DateTime.Now;
                studentToFire.GroupId = 1;
                context.SaveChanges();
            }
        }

    }
}
