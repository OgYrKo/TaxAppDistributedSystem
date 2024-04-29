using System.Windows;

namespace MainOfficeClient.Windows.Dialogs
{
    /// <summary>
    /// Interaction logic for CheckDialog.xaml
    /// </summary>
    public partial class CheckDialog : Window
    {
        public CheckDialog()
        {
            InitializeComponent();
        }
        public CheckDialog(string text)
        {
            InitializeComponent();
            Question.Text = text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
