using Client.Pages.Menus;
using System.Windows;
using System.Windows.Controls;

namespace Client.Windows
{
    /// <summary>
    /// Interaction logic for RegionOfficeSelectionWindow.xaml
    /// </summary>
    public partial class RegionOfficeSelectionWindow : Window
    {
        public RegionOfficeSelectionWindow()
        {
            InitializeComponent();
        }
        private void ManagerButtonClick(object sender, RoutedEventArgs e)
        {
            OpenMainWindow(new RegionOfficeManager());

        }

        private void AdminButtonClick(object sender, RoutedEventArgs e)
        {
            OpenMainWindow(new RegionOfficeAdmin());
        }

        private void OpenMainWindow(Page page)
        {
            MainWindow mainAdminWindow = new MainWindow(page);
            mainAdminWindow.Show();
            this.Close();
        }
    }
}
