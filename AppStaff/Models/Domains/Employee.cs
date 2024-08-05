using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppStaff.Models.Domains
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Comments { get; set; }
        public double Earnings { get; set; }
        public DateTime EmploymentDate { get; set; }
        public DateTime ?FireDate { get; set; }

        public int GroupId { get; set; }

        public Group Group { get; set; }
    }
}
