 using Client.Classes;
using Client.Pages.Cards;
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
    /// <summary>
    /// Interaction logic for LandList.xaml
    /// </summary>
    public partial class LandList : Page
    {
        ObservableCollection<Landplotfull> Lands;
        int SelectedIndex;
        public LandList(ObservableCollection<Landplotfull> lands)
        {

            InitializeComponent();
            Lands = lands;
            Data.ItemSource = Lands;
        }

        private void Handler(DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.Column.Header.ToString())
            {
                case "Landplotkey":
                    e.Column.Header = "Ключ";
                    break;
                case "Landplotname":
                    e.Column.Header = "Найменування";
                    break;
                case "Cadastralnumber":
                    e.Column.Header = "Кадастровий номер";
                    break;
                case "Usesysid":
                    e.Column.Header = "Ключ";
                    break;
                case "Counterpartynames":
                    e.Column.Header = "Власник";
                    break;
                case "Square":
                    e.Column.Header = "Площа";
                    break;
                case "Sectionkey":
                    e.Column.Header = "Код цільового призначення";
                    break;
                case "City":
                    e.Column.Header = "Місто";
                    break;
                case "Street":
                    e.Column.Header = "Вулиця";
                    break;
                case "Area":
                    e.Column.Header = "Зона";
                    break;
                case "Location":
                    e.Column.Header = "Розташування";
                    break;
                case "Type":
                    e.Column.Header = "Форма власності";
                    break;
                case "Standartvaluation":
                    e.Column.Header = "НГО";
                    break;
                case "Monetaryvaluation":
                    e.Column.Header = "РГО";
                    break;
                case "Withouttax":
                    e.Column.Header = "Налог";
                    break;
                default:
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
            }

        }

        private void Data_OnClick(object o)
        {
            var row = o as DataGridRow;
            var emp = row.DataContext as Landplotfull;
            if (emp != null)
            {
                SelectedIndex = Lands.IndexOf(emp);
                LandCard landCard = new LandCard(emp);
                landCard.OnClose += OnCloseCard;
                landCard.OnSave += OnSaveCard;
                MainWindowComunication.OpenPage(landCard, this);
            }
        }

        private void AddCard(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SelectedIndex = -1;
            LandCard landCard = new LandCard();
            landCard.OnClose += OnCloseCard;
            landCard.OnSave += OnSaveCard;

            MainWindowComunication.OpenPage(landCard, this);
        }

        private void OnSaveCard(object sender, EventArgs e)
        {
            Landplotfull land = sender as Landplotfull;
            if (SelectedIndex == -1)
            {
                Lands.Add(land);
                SelectedIndex = Lands.Count - 1;
            }
            else
            {
                Lands[SelectedIndex] = land;
            }
            Data.InvalidateVisual();
        }

        private void OnCloseCard(object sender, EventArgs e)
        {
            // Получаем текущий элемент навигации
            if (sender == null)
            {
                Lands.RemoveAt(SelectedIndex);
                Data.InvalidateVisual();
            }
        }
    }
}
