using Client.Classes;
using Client.Enums;
using Client.Pages.Lists;
using Client.Pages.Menus;
using Client.Windows;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Page StartPage;
        private Dictionary<string, Func<Task<Page>>> PageNames;

        public MainWindow()
        {
            InitializeComponent();
            StartPage = new RegionOfficeManager();
            Init();
        }

        public MainWindow(Page startPage)
        {
            InitializeComponent();
            StartPage = startPage;
            Init();
        }

        private void Init()
        {
            Main.Content = StartPage;
            SetByPosition();
            PreviousPageBtn.Visibility = Visibility.Hidden;
            NextPageBtn.Visibility = Visibility.Hidden;
        }

        private void SetByPosition()
        {
            switch(CurrentUser.Instance().Postition)
            {
                case UserPosition.Admin:
                    changeCenterBtn.Visibility = Visibility.Visible;
                    SetAdminSuggestions();
                    break;
                case UserPosition.Manager:
                    SetManagerSuggestions();
                    break;
                default:
                    break;
            }

        }

        private void SetAdminSuggestions()
        {
            PageNames=new Dictionary<string, Func<Task<Page>>>();
            PageNames["Користувачі"] = async () => new UserList();
            PageNames["Адреса"] = async () => new AddressList();
            //PageNames["Контрагенти"] = async () => new UserList();

        }

        private void SetManagerSuggestions()
        {
            
        }

        private void PreviousPage(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (Main.CanGoBack) 
                {
                    Main.GoBack();
                    NextPageBtn.Visibility= Visibility.Visible;
                    if (Main.CanGoBack)
                        PreviousPageBtn.Visibility = Visibility.Visible;
                    else
                        PreviousPageBtn.Visibility = Visibility.Hidden;
                }
            }
        }

        private void NextPage(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (Main.CanGoForward) 
                {
                    Main.GoForward();
                    PreviousPageBtn.Visibility = Visibility.Visible;
                    if (Main.CanGoForward)
                        NextPageBtn.Visibility = Visibility.Visible;
                    else
                        NextPageBtn.Visibility = Visibility.Hidden;

                }
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ListView)sender).SelectedIndex == -1) return;
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemHome":
                    MainWindowComunication.OpenPage(StartPage, this);
                    break;
                case "ItemAmes":
                    break;
                case "ItemSaved":
                    break;
                default:
                    break;
            }
            ((ListView)sender).SelectedIndex = -1;
        }

        private void suggestionsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (suggestionsListBox.SelectedIndex != -1)
            {
                string selectedSuggestion = suggestionsListBox.SelectedItem.ToString();
                searchTextBox.Text = "";
                _ = MainWindowComunication.OpenPageWithWait(PageNames[selectedSuggestion], (Page)Main.Content);
                suggestionsPopup.IsOpen = false;
            }
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = searchTextBox.Text;
            List<string> searchSuggestions = new List<string>();
            foreach (var item in PageNames)
            {
                if (item.Key.ToLower().StartsWith(searchText.ToLower()))
                    searchSuggestions.Add(item.Key);
            }

            if (searchSuggestions.Count > 0)
            {
                suggestionsListBox.ItemsSource = searchSuggestions;
                suggestionsPopup.IsOpen = true;
            }
            else
            {
                suggestionsPopup.IsOpen = false;
            }
        }

        private void ChangeCenterClick(object sender, RoutedEventArgs e)
        {
            new RegionOfficeSelectionWindow().Show();
            this.Close();
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            CurrentUser.ResetUser();
            new Login().Show();
            this.Close();
        }
    }
}
