using Client.Classes;
using Client.Pages.Lists;
using System;
using System.Collections.Generic;
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

namespace Client.Pages.Menus
{
    /// <summary>
    /// Interaction logic for RegionOfficeAdmin.xaml
    /// </summary>
    public partial class RegionOfficeAdmin : Page
    {
        public RegionOfficeAdmin()
        {
            InitializeComponent();
        }

        private void UsersClick(object sender, RoutedEventArgs e)
        {
            _ = MainWindowComunication.OpenPageWithWait(async () => new UserList(), this);
        }
        private void TablesCLick(object sender, RoutedEventArgs e)
        {

        }
        private void ReportsClick(object sender, RoutedEventArgs e)
        {
            _ = MainWindowComunication.OpenPageWithWait(async () => new DebtList(), this);
        }
        private void StatsClick(object sender, RoutedEventArgs e)
        {
            _ = MainWindowComunication.OpenPageWithWait(async () => new StatList(), this);
        }
    }
}
