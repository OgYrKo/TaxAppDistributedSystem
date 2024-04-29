using DBClassesLibrary;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client.Pages.Lists
{
    /// <summary>
    /// Interaction logic for PropertyList.xaml
    /// </summary>
    public partial class PropertyList : Page
    {
        ObservableCollection<Realpropertyfull> Realproperty;
        int SelectedIndex;

        public PropertyList(ObservableCollection<Realpropertyfull> properties)
        {
            InitializeComponent();
            Realproperty = properties;
            Properties.ItemsSource = Realproperty;
        }
        public PropertyList()
        {
            InitializeComponent();
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
