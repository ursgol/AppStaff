using AppStaff.Models.Domains;
using AppStaff.Models.Wrappers;
using AppStaff.ViewModels;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AppStaff.Views
{
    /// <summary>
    /// Interaction logic for AddEditEmployee.xaml
    /// </summary>
    public partial class AddEditEmployee : MetroWindow
    {
        
            public AddEditEmployee(EmployeeWrapper employee = null)
            {
                InitializeComponent();
                DataContext = new AddEditViewModel(employee);
            }

        
    }
}
