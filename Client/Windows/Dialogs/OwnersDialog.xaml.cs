using Client.Classes;
using DBClassesLibrary;
using InterfacesLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Client.Windows.Dialogs
{
    public partial class OwnersDialog : Window
    {
        int landkey;
        private ICounterpartyFunc counterparty_func_channel;
        private ILandFunc landplot_func_channel;
        public ObservableCollection<Contractorsland> contractorslands { get; set; }
        public List<Counterparty> counterparties { get; set; }

        public OwnersDialog(int landplotkey)
        {
            landkey = landplotkey;
            InitializeComponent();
            DataContext = this;
            InitializeConnections();
            InitializeLists(landplotkey);
            InitializeComponent();
            InitializeBindings();
        }
        public void InitializeConnections()
        {
            Connection<ICounterpartyFunc> connection = new Connection<ICounterpartyFunc>("ICounterpartyFunc");
            counterparty_func_channel = connection.channelWithUser;

            Connection<ILandFunc> connection2 = new Connection<ILandFunc>("ILandFunc");
            landplot_func_channel = connection2.channelWithUser;
        }

        private void InitializeLists(int Landplotkey)
        {
            contractorslands = new ObservableCollection<Contractorsland>(counterparty_func_channel.GetAllContractorslandWithFilter(null, new List<int>() { Landplotkey }, null, null, null, null, 0));
            counterparties = counterparty_func_channel.GetAllCounterparties();
        }

        private void InitializeBindings()
        {
            foreach (var contractorsland in contractorslands)
            {
                contractorsland.Contractorslanddate = DateTime.Now;
            }
            Owners.ItemsSource = contractorslands;
        }

        private void SetItemsSource<T>(ComboBox comboBox, List<T> list, string displayMemberPath, string SelectedValuePath)
        {
            comboBox.ItemsSource = list;
            comboBox.DisplayMemberPath = displayMemberPath;
            comboBox.SelectedValuePath = SelectedValuePath;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Owner> owners = new List<Owner>();
            foreach (var contractorsland in contractorslands)
            {
                owners.Add(new Owner() { Counterpartykey = contractorsland.Counterpartykey, Share = Convert.ToDouble(contractorsland.Share), Withouttax = Convert.ToBoolean(contractorsland.Withouttax) });
            }

            string message = landplot_func_channel.UpdateLandplotOwners(landkey, owners);
            MessageBox.Show(message, " Інформація", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            this.DialogResult = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text != "." && IsNumber(e.Text) == false)
            {
                e.Handled = true;
            }
            else if (e.Text == ".")
            {
                if (((TextBox)sender).Text.IndexOf(e.Text) > -1)
                {
                    e.Handled = true;
                }
            }
        }
        private bool IsNumber(string Text)
        {
            int output;
            return int.TryParse(Text, out output);
        }

        private void PackIcon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Contractorsland c = (sender as WrapPanel).DataContext as Contractorsland;
            //Contractorsland c = (sender as MaterialDesignThemes.Wpf.PackIcon).DataContext as Contractorsland;
            contractorslands.Remove(c);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Contractorsland contractorsland = new Contractorsland();
            contractorsland.Withouttax = false;
            contractorsland.Contractorslanddate = DateTime.Now;
            contractorslands.Add(contractorsland);
        }
    }
}
