using Client.Classes;
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
    /// Interaction logic for AddressList.xaml
    /// </summary>
    public partial class AddressList : Page
    {
        public AddressList()
        {
            Connection<IAddressFunc> connection = new Connection<IAddressFunc>("IAddressFunc");
            var channel = connection.channel;

            InitializeComponent();
            Addresses.ItemsSource = channel.GetAllAddresses();
        }

        private void AddCard(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //SelectedIndex = -1;
            //UserCard userCard = new UserCard(Channel);
            //userCard.OnClose += OnCloseCard;
            //userCard.OnSave += OnSaveCard;

            //MainWindowComunication.OpenPage(userCard, this);
        }
    }
}
