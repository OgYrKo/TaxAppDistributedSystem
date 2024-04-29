using Client.Classes;
using Client.Pages.Lists;
using InterfacesLibrary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Client.Controls
{
    public partial class BankOrders : UserControl
    {
        IOrder Chanel;
        List<string> Items;


        public BankOrders()
        {
            InitializeComponent();
        }

        private string SetConnection(string usernameMail, string passwordMail)
        {
            string usernameDB = CurrentUser.Instance().Login;
            string passwordDB = CurrentUser.Instance().Password;
            try
            {
                Connection<IOrder> connection = new Connection<IOrder>("IOrder");
                Chanel = connection.channel;
                return Chanel.Login(usernameMail, passwordMail, usernameDB, passwordDB);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task Update()
        {
            try
            {
                await Task.Run(() => { UpdateItems(); });
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            SetNewListView();

        }

        private void SetNewListView()
        {
            Orders.Items.Clear();

            for (int i = 0; i < Items.Count; i++)
            {
                string itemText = $"{i + 1}) {Items[i]}";
                ListViewItem listViewItem = new ListViewItem();
                TextBlock textBlock = new TextBlock()
                {
                    Text = itemText,
                    Style = (Style)FindResource("TextBlockStyle")
                };
                listViewItem.Content = textBlock;
                Orders.Items.Add(listViewItem);
            }
        }

        private void UpdateItems()
        {
            Items = Chanel.GetActiveFilesOrders();
        }

        private void Orders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CheckSelectedIndex(sender)) return;

            OrderList orderList = new OrderList(Items[((ListView)sender).SelectedIndex]);
            orderList.OnClose += OnCloseOrderList;
            MainWindowComunication.OpenPage(orderList, this);

            SetDefaultSelectedIndex(sender);
        }

        private async void UpdateClick(object sender, SelectionChangedEventArgs e)
        {
            if (CheckSelectedIndex(sender)) return;
            progressBar.Visibility = Visibility.Visible;
            await Update();
            progressBar.Visibility = Visibility.Collapsed;
            SetDefaultSelectedIndex(sender);
        }

        private bool CheckSelectedIndex(object sender) => ((ListView)sender).SelectedIndex == -1;
        private void SetDefaultSelectedIndex(object sender) => ((ListView)sender).SelectedIndex = -1;

        private async void LoginClick(object sender, RoutedEventArgs e)
        {
            progressBar.Visibility = Visibility.Visible;
            string usernameMail = LoginTxt.Text;
            string passwordMail = PasswordTxt.Password;
            await Task.Run(() =>
            {
                string message = TryConnect(usernameMail, passwordMail);
                if (message.Equals("OK"))
                {
                    Dispatcher.Invoke(() =>
                    {
                        Message.Content = "Wait";
                        _ = Update();
                        Login.Visibility = Visibility.Collapsed;
                        Orders.Visibility = Visibility.Visible;
                        UpdateCommand.Visibility = Visibility.Visible;
                    });
                }
                else
                {
                    Dispatcher.Invoke(() =>
                    {
                        Message.Content = message;
                    });
                }

                Dispatcher.Invoke(() =>
                {
                    progressBar.Visibility = Visibility.Collapsed;
                });
            });
        }


        private string TryConnect(string usernameMail, string passwordMail)
        {
            //usernameMail = "maksym.halchynskyi@stud.onu.edu.ua";
            //passwordMail = "20onum20";
            string message = SetConnection(usernameMail, passwordMail);
            return message;
        }

        private void OnCloseOrderList(object sender, EventArgs e)
        {
            _ = Update();
        }
    }
}
