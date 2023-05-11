﻿using KP.db.context;
using KP.dbClasses;
using KP.View.login;
using KP.View.Registration;
using KP.ViewModel.LoginViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace KP
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>

    public partial class App : Application
    {
        DbAppContext db;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            /*var regView = new Registration();
            regView.ShowDialog();
            if (regView.IsLoaded && regView.IsVisible == false)
            {
                regView.Close();
                var loginView = new login();
                loginView.Show();
                loginView.IsVisibleChanged += (s, ev) =>
                {
                    if (loginView.IsVisible == false && loginView.IsLoaded)
                    {*/
                        var mainWindow = new MainWindow();
                        mainWindow.Show();
                        /*loginView.Close();
                    }
                };
            }*/
        }
    }
}