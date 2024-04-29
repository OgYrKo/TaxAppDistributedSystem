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
using System.Windows.Shapes;

namespace Client.Windows.Dialogs
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
