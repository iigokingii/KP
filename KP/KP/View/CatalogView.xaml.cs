using KP.dbClasses;
using KP.DBMethods.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

namespace KP.View
{
    /// <summary>
    /// Логика взаимодействия для CatalogView.xaml
    /// </summary>
    public partial class CatalogView : UserControl
    {
        

        public CatalogView()
        {
            InitializeComponent();
        }
        private void ListBox_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - e.Delta / 3);
        }
    }
}
