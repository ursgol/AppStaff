using AppStaff.Models.Configurations;
using AppStaff.Models.Domains;
using AppStaff.Properties;
using System;
using System.Data.Entity;
using System.Linq;

namespace AppStaff
{
    public class ApplicationDbContext : DbContext
    {
        private static string _conString =
       $@"Server=({Settings.Default.ServerName})\{Settings.Default.AddressName};
         Database={Settings.Default.DbName};
        User Id = {Settings.Default.user}; 
        Password={Settings.Default.password};";

        public ApplicationDbContext()
            : base(_conString)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Group> Groups { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EmployeeConfiguration());
            modelBuilder.Configurations.Add(new GroupConfiguration());


        }

    }

}