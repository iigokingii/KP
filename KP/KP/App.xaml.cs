using KP.db.context;
using KP.dbClasses;
using KP.View;
using KP.View.login;
using KP.View.Registration;
using KP.ViewModel.LoginViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
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
        public static string Login;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            using (DbAppContext db = new DbAppContext())
            {

            }           
            /*var regView = new Registration();
            regView.ShowDialog();
            if (regView.IsLoaded && regView.IsVisible == false)
            {
                regView.Close();
                if (!string.IsNullOrEmpty(Login))
                {
                    if (Login == "admin")
                    {
                        var mainWindow = new MainWindow();
                        mainWindow.Show();
                    }
                    else
                    {
                        var mainWindow = new MainWindowUser();
                        mainWindow.Show();
                    }
                }
            }*/
        }
    }
}
