using KP.DBMethods.UnitOfWork;
using KP.View.login;
using KP.ViewModel.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KP.ViewModel.LoginViewModel
{
    internal class LoginViewModel:ViewModelBase
    {
        UnitOfWork unit;
        private string _username;
        private SecureString _password;
        private string _errorMessage;
        private bool _isViewVisible = true;
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged("Username");
            }
        }
        public SecureString Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
        }

        public bool IsViewVisible
        {
            get
            {
                return _isViewVisible;
            }
            set
            {
                _isViewVisible = value;
                OnPropertyChanged("IsViewVisible");
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand RecoverPasswordCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public ICommand RememberPasswordCommand { get; }

        public LoginViewModel()
        {
            unit = new UnitOfWork();
            LoginCommand = new ViewModelCommandBase(ExecuteLoginCommand,CanExecuteLoginCommand);
            RecoverPasswordCommand = new ViewModelCommandBase(ExecuteRecoverPasswordCommand);

        }
        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData = true;
            if(string.IsNullOrEmpty(Username) || Password == null || Password.Length<3)
                validData = false;
            return validData;
        }
        private void ExecuteLoginCommand(object obj)
        {
            bool isValid = unit.Users.AuthenticateUser(new NetworkCredential(Username,Password));
            if (isValid)
            {  
                //контекст безопасности потока, класс аутентифицированного пользователя
                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(Username),null);
                IsViewVisible = false;
                App.Login = Username;
            }
            else
            {
                ErrorMessage = "* Неправильный логин или пароль";
            }

        }


        private void ExecuteRecoverPasswordCommand(object obj)
        {
            throw new NotImplementedException();
        }

       
    }
}
