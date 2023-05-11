﻿using KP.DBMethods.UnitOfWork;
using KP.ViewModel.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Text.RegularExpressions;
using KP.dbClasses;
using System.Runtime.InteropServices;
using KP.View.login;

namespace KP.ViewModel.Registration
{
    internal class RegistrationViewModel:ViewModelBase
    {
        UnitOfWork unit;
        private string _username;
        private SecureString _password;
        private string _email;
        private string _errorMessage;
        private bool _isViewVisible = true;
        public string UserMail
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged("UserMail");
            }
        }

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

        public ICommand RegistrationCommand { get; }
        public ICommand AuthorizationCommand { get; }

        public RegistrationViewModel()
        {
            unit = new UnitOfWork();
            RegistrationCommand = new ViewModelCommandBase(ExecuteLoginCommand, CanExecuteLoginCommand);
            AuthorizationCommand = new ViewModelCommandBase(ExecuteAuthorizationCommand);

        }

        private void ExecuteAuthorizationCommand(object obj)
        {
            IsViewVisible = false;
            var loginView = new login();
            loginView.Show();
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData = true;
            if (string.IsNullOrEmpty(Username) || Password == null || Password.Length < 3 || !EmailValid())
                validData = false;
            return validData;
        }

        private bool EmailValid()
        {
            string template = "^([a-z0-9_-]+\\.)*[a-z0-9_-]+@[a-z0-9_-]+(\\.[a-z0-9_-]+)*\\.[a-z]{2,6}$";
            Regex regex = new Regex(template);
            MatchCollection match = regex.Matches(_email);
            if (match.Count == 1)
                return true;
            return false;
        }
        //securestring-->string
        String SecureStringToString(SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }



        private void ExecuteLoginCommand(object obj)
        {
            UserProfile user = new UserProfile();
            user.Email = UserMail;
            user.Login = Username;
            string s = SecureStringToString(Password);
            user.Password = s;
            user.Avatar = null;
            unit.Users.Add(user);
            unit.Users.Save();
            IsViewVisible = false;
            var loginView = new login();
            loginView.Show();
        }
    }
}