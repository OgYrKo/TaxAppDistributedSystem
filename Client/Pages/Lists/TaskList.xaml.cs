using Client.Classes;
using Client.Windows.Dialogs;
using InterfacesLibrary;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client.Pages.Lists
{
    /// <summary>
    /// Interaction logic for TaskList.xaml
    /// </summary>
    public partial class TaskList : Page
    {
        private ILandFunc land_func_channel;
        public TaskList()
        {
            InitializeComponent();
            Connection<ILandFunc> connection_to_land_func = new Connection<ILandFunc>("ILandFunc");
            land_func_channel = connection_to_land_func.channel;
        }

        private void ListItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            CheckDialog checkDialog = new CheckDialog("Виконати нарахування податку на землю?");
            if (checkDialog.ShowDialog() == true)
            {
                string s = land_func_channel.ChargeTaxesToAll();
            }
        }
    }
}
