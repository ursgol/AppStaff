using AppStaff.Commands;
using AppStaff.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AppStaff.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private UserSettings _userSettings;
        private readonly bool _canCloseWindow;

        public SettingsViewModel(bool canCloseWindow)
        {
            CloseCommand = new RelayCommand(Close);
            ConfirmCommand = new RelayCommand(Confirm);
            _userSettings = new UserSettings();
            _canCloseWindow = canCloseWindow;
        }
        public UserSettings UserSettings
        {
            get
            {
                return _userSettings;
            }
            set
            {
                _userSettings = value;
                OnPropertyChanged();
            }
        }



        public ICommand CloseCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }


        private void Close(object obj)
        {
            if (_canCloseWindow)
            {
                CloseWindow(obj as Window);
            }
            else
            {
                Application.Current.Shutdown();
            }
        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }

        private void Confirm(object obj)
        {
            if (!UserSettings.IsValid)
                return;

            UserSettings.Save();
            RestartApplication();
        }

        private void RestartApplication()
        {
            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
    }
}
