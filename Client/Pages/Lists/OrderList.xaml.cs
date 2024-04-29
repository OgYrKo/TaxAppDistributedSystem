using Client.Classes;
using Client.Pages.Cards;
using CounterpartyInfo;
using DBClassesLibrary;
using InterfacesLibrary;
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

namespace Client.Pages.Lists
{
    public partial class OrderList : Page
    {
        ObservableCollection<Counterpartyorder> Orders;
        private int SelectedIndex;
        private IOrder Chanel;
        private string FileName;
        public event EventHandler OnClose;

        public OrderList(string fileName)
        {
            Connection<IOrder> connection = new Connection<IOrder>("IOrder");
            Chanel = connection.channel;

            FileName = fileName;
            InitializeComponent();
            SetItemSource();
        }
        
        private void SetItemSource()
        {
            Orders = GetOrdersFromServer();
            Data.ItemSource = Orders;
        }

        private ObservableCollection<Counterpartyorder> GetOrdersFromServer()
        {
            OrdersFromFile ordersFromFile =  Chanel.GetFile(FileName);
            ObservableCollection<Counterpartyorder> counterpartyorders = new ObservableCollection<Counterpartyorder>(ordersFromFile.Orders);
            return counterpartyorders;
        }

        private void UpdateOrdersFromServer()
        {
            OrdersFromFile ordersFromFile = new OrdersFromFile();
            ordersFromFile.Orders = Orders.ToList();
            ordersFromFile.FileName = FileName;
            try
            {
                OrdersFromFile newOrdersFromFile = Chanel.AddOrders(ordersFromFile);
                if (newOrdersFromFile == null)
                {
                    ClosePage();
                }
                else
                {
                    Orders = new ObservableCollection<Counterpartyorder>(newOrdersFromFile.Orders);
                    Data.ItemSource = Orders;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RowInTableClick(object o)
        {
            var row = o as DataGridRow;
            Counterpartyorder gridOrder = row.DataContext as Counterpartyorder;
            SelectedIndex = Orders.IndexOf(gridOrder);
            OrderCard orderCard = new OrderCard(gridOrder);
            orderCard.OnClose += OnCloseOrderCard;
            orderCard.OnSave += OnSaveOrderCard;
            MainWindowComunication.OpenPage(orderCard, this);
        }

        private void ConfirmClick(object sender, MouseButtonEventArgs e)
        {
            UpdateOrdersFromServer();
        }

        private void SetColumns(DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.Column.Header.ToString())
            {
                case "Itn"://ідентифікатор платника податків
                    e.Column.Header = "Номер";
                    break;
                case "Purpose"://ім'я
                    e.Column.Header = "Назва";
                    break;
                case "Amount"://сума
                    e.Column.Header = "Сума";
                    break;
                default:
                    e.Column.Visibility = Visibility.Collapsed;//id
                    break;
            }
        }

        private void OnSaveOrderCard(object sender, EventArgs e)
        {
            Orders[SelectedIndex] = sender as Counterpartyorder;
        }

        private void OnCloseOrderCard(object sender, EventArgs e)
        {
            // Получаем текущий элемент навигации
            if (sender == null) Orders.RemoveAt(SelectedIndex);
        }

        private void ClosePage()
        {
            if (OnClose != null)OnClose(null, EventArgs.Empty);
            MainWindowComunication.ClosePage(this);
        }
    }
}
