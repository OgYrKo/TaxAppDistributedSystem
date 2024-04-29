using Client.Classes;
using DBClassesLibrary;
using InterfacesLibrary;
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

namespace Client.Pages.Lists
{
    /// <summary>
    /// Interaction logic for DebtList.xaml
    /// </summary>
    public partial class DebtList : Page
    {
        public DebtList()
        {
            Connection<IFullData> connection = new Connection<IFullData>("IFullData");
            var channel = connection.channelWithUser;
            CurrentUser user = CurrentUser.Instance();
            InitializeComponent();
            List<Debt> debts = channel.GetAllDebts();
            Data.ItemsSource = debts;
        }

        private void ListActions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
