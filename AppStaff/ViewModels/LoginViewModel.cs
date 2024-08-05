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
    public class LoginViewModel : ViewModelBase
    {
        private Login _loginUser;
        private readonly bool _canCloseWindow;

        public LoginViewModel(bool canCloseWindow)
        {
            CloseCommand = new RelayCommand(Close);
            ConfirmCommand = new RelayCommand(Confirm);
            _loginUser = new Login();
            _canCloseWindow = canCloseWindow;
        }
        public Login LoginUser
        {
            get
            {
                return _loginUser;
            }
            set
            {
                _loginUser = value;
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
            if (!LoginUser.IsValid)
                CloseWindow(obj as Window);

            if (!LoginUser.IsPasswordUserMatch)
                return;
            
            CloseWindow(obj as Window);
                
        }


    }
}
