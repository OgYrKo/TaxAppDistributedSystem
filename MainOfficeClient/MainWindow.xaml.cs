using MainOfficeClient.Classes;
using MainOfficeClient.Pages.Menus;
using MainOfficeClient.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MainOfficeClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Page StartPage;

        public MainWindow()
        {
            InitializeComponent();
            StartPage = new MainOfficeAdmin();
            Main.Content = StartPage;
        }

        private void PreviousPage(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (Main.CanGoBack) 
                {
                    Main.GoBack();
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
                case "ItemExit":
                    OpenLoginWindow();
                    break;
                default:
                    break;
            }
            ((ListView)sender).SelectedIndex = -1;
        }

        private void OpenLoginWindow()
        {
            CurrentUser.ResetUser();
            new Login().Show();
            this.Close();
        }
    } 
}
