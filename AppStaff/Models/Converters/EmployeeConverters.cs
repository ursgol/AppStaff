using AppStaff.Models.Domains;
using AppStaff.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppStaff.Models.Converters
{
    public static class EmployeeConverter
    {
        public static EmployeeWrapper ToWrapper(this Employee model) => new EmployeeWrapper
        {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Comments = model.Comments,
            Earnings = model.Earnings,
            EmploymentDate = model.EmploymentDate,
            FireDate = model.FireDate,
            Group = new GroupWrapper { Id = model.Group.Id, Name = model.Group.Name },


        };


        public static Employee ToDao(this EmployeeWrapper model)
        {
            return new Employee
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Earnings = model.Earnings,
                GroupId = model.Group.Id,
                Comments = model.Comments,
                EmploymentDate = DateTime.Now,
                FireDate = model.FireDate
            };
        }
    }
}
