using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AppStaff.Commands;
using AppStaff.Models.Domains;
using AppStaff.Models.Wrappers;
using AppStaff.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Diagnostics;

namespace AppStaff.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private Repository _repository = new Repository();

        public MainViewModel()
        {
            RefreshStaffCommand = new RelayCommand(RefreshStaff);
            AddEmployeeCommand = new RelayCommand(AddEditEmployee);
            EditEmployeeCommand = new RelayCommand(AddEditEmployee, CanEditFireEmployee);
            FireCommand = new AsyncRelayCommand(FireEmployee, CanEditFireEmployee);
            SettingsCommand = new RelayCommand(SettingsCom);

           
            LoadedWindow(null);




        }

        private async void LoadedWindow(object obj)
        {
           


            if (!IsValidConnectionToDatabase())
            {
                var metroWindow = Application.Current.MainWindow as MetroWindow;
                var dialog = await metroWindow.ShowMessageAsync("Błąd połączenia", $"Nie można połączyć się" +
                    $"z bazą danych. Czy chcesz zmienić ustawienia? ", MessageDialogStyle.AffirmativeAndNegative);

                if (dialog == MessageDialogResult.Negative)
                {
                    Application.Current.Shutdown();
                }
                else
                {
                    var settingsWindow = new SettingsViewSet(false);
                    settingsWindow.ShowDialog();
                }
            }
            else
            {
                var loginWindow = new LoginView(false);
                loginWindow.ShowDialog();
                RefreshStaff(null);
                InitGroups();
           
            }
        }



        public ICommand RefreshStaffCommand { get; set; }
        public ICommand AddEmployeeCommand { get; set; }
        public ICommand EditEmployeeCommand { get; set; }
        public ICommand FireCommand { get; set; }
        public ICommand SettingsCommand { get; set; }


        private EmployeeWrapper _selectedEmployee;

        public EmployeeWrapper SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<EmployeeWrapper> _employees;

        public ObservableCollection<EmployeeWrapper> Employees
        {
            get { return _employees; }
            set
            {
                _employees = value;
                OnPropertyChanged();
            }
        }

        private int _selectedGroupId;
        public int SelectedGroupId
        {
            get { return _selectedGroupId; }
            set
            {
                _selectedGroupId = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Group> _group;

        public ObservableCollection<Group> Groups
        {
            get { return _group; }
            set
            {
                _group = value;
                OnPropertyChanged();
            }
        }

        private void InitGroups()
        {
            var groups = _repository.GetGroups();
            groups.Insert(0, new Group { Id = 0, Name = "Wszystkie" });

            Groups = new ObservableCollection<Group>(groups);

            SelectedGroupId = 0;
        }



        private void AddEditEmployee(object obj)
        {
            var addEditEmployeeWindow = new AddEditEmployee(obj as EmployeeWrapper);
            addEditEmployeeWindow.Closed += AddEditEmployeeWindow_Closed;
            addEditEmployeeWindow.ShowDialog();
        }

        private void AddEditEmployeeWindow_Closed(object sender, EventArgs e)
        {
            RefreshStaff(null);
        }

        private bool CanEditFireEmployee(object obj)
        {
            return SelectedEmployee != null;
        }

        private void RefreshStaff(object obj)
        {
            Employees = new ObservableCollection<EmployeeWrapper>(_repository.GetEmployees(SelectedGroupId));

        }

        private async Task FireEmployee(object arg)
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var dialog = await metroWindow.ShowMessageAsync("Zwalnianie pracownika",
                $"Czy na pewno chcesz zwolnic pracownika " +
                $"{SelectedEmployee.FirstName} {SelectedEmployee.LastName}?",
                MessageDialogStyle.AffirmativeAndNegative);

            if (dialog != MessageDialogResult.Affirmative)
            {
                return;
            }

            //zwolnienie pracownika w bazie
            _repository.FireEmployee(SelectedEmployee.Id);
            RefreshStaff(null);
        }

        private void SettingsCom(object obj)
        {
            var settingsWindow = new SettingsViewSet(true);
            settingsWindow.ShowDialog();
        }

        private bool IsValidConnectionToDatabase()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    context.Database.Connection.Open();
                    context.Database.Connection.Close();

                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

    }


}
