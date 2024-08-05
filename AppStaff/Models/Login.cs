using AppStaff.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppStaff.Models
{
    public class Login: IDataErrorInfo
    {

        public string LUser
        {
            get
            {
                return Settings.Default.LUser;
            }
            set
            {
                Settings.Default.LUser = value;
            }
        }
        public string LoginPassword
        {
            get
            {
                return Settings.Default.LoginPassword;
            }
            set
            {
                Settings.Default.LoginPassword = value;
            }
        }

        public bool IsLoginValid()
        {
            if (LoginPassword == "a" && LUser == "admin")
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool IsPasswordUserMatch => IsLoginValid();


        private bool _isPasswordValid;
        private bool _isUserValid;

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(LUser):
                        if (string.IsNullOrWhiteSpace(LUser))
                        {
                            Error = "Pole uzytkownika jest wymagane.";
                            _isUserValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isUserValid = true;
                        }
                        break;
                    case nameof(LoginPassword):
                        if (string.IsNullOrWhiteSpace(LoginPassword))
                        {
                            Error = "Pole hasło jest wymagane.";
                            _isPasswordValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isPasswordValid = true;
                        }
                        break;
                    default:
                        break;
                }
                return Error;
            }
        }

        public string Error { get; set; }

        public bool IsValid
        {
            get
            {
                return _isPasswordValid && _isUserValid;

            }
        }
    }
}
