using KP.db.context;
using KP.dbClasses;
using KP.View.login;
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
            var loginView = new login();
            loginView.Show();
            loginView.IsVisibleChanged += (s, ev) =>
            {
                if (loginView.IsVisible == false && loginView.IsLoaded)
                {
                    var mainWindow = new MainWindow();
                    mainWindow.Show();
                    loginView.Close();
                }
            };
        }




        /* //private NavigationStore _navigationStores;
         App()
         {
           //  _navigationStores=new NavigationStore();
         }
         protected override void OnStartup(StartupEventArgs e)
         {
             MainWindow = new MainWindow();
             MainWindow.Show();
             *//*{
                 DataContext     
             }*//*
         }*/

        /* public partial class MainWindow : Window
         {
             public MainWindow()
             {
                 InitializeComponent();
             }
         }*/


    }
}
