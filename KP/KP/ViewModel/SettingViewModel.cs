using KP.ViewModel.BaseModels;
/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;*/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace KP.ViewModel
{
    internal class SettingViewModel:ViewModelBase
    {
        public ICommand SwitchLangToRuCommand { get; }
        public ICommand SwitchLangToEnCommand { get; }
        public SettingViewModel()
        {
            SwitchLangToRuCommand = new ViewModelCommandBase(SwitchLangToRu);
            SwitchLangToEnCommand = new ViewModelCommandBase(SwitchLangToEn);
        }

        private void SwitchLangToEn(object obj)
        {
            /*System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("en-US");*/
            /*Application.Current.Resources.MergedDictionaries[1].Source = new Uri("Resources/Localization/lang.en-US.xaml", UriKind.Relative);*/


            try
            {
                Application.Current.Resources.MergedDictionaries[1].Source = new Uri("Resources/Localization/lang.en-US.xaml", UriKind.Relative);
            }
            catch (Exception ex)
            {
                MessageBox.Show("change lang Err");
            }
        }

        private void SwitchLangToRu(object obj)
        {
            try
            {
                Application.Current.Resources.MergedDictionaries[1].Source = new Uri("Resources/Localization/lang.ru-RU.xaml", UriKind.Relative);
            }
            catch (Exception ex)
            {
                MessageBox.Show("change lang Err");
            }
        }
    }
}
