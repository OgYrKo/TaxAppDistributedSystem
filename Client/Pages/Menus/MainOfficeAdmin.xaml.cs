using Client.Classes;
using Client.Pages.Lists;
using RouterLib;
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

namespace Client.Pages.Menus
{
    public partial class MainOfficeAdmin : Page
    {
        List<RegionOffice> RegionOffices;
        private RouterProxy Proxy;

        public MainOfficeAdmin()
        {
            InitializeComponent();
        }

        private void OpenOfficeList(object sender, RoutedEventArgs e)
        {
            if (RegionOffices == null) ProxyInit();
            if (RegionOffices == null) return;
            _ = MainWindowComunication.OpenPageWithWait(async () => new OfficeList(Proxy, ref RegionOffices), this);

        }

        private void OpenReportList(object sender, RoutedEventArgs e)
        {

        }

        private void OpenStatsList(object sender, RoutedEventArgs e)
        {
            if (RegionOffices == null) ProxyInit();
            if (RegionOffices == null) return;

            List<string> offices = new List<string>();
            foreach (var item in RegionOffices)
            {
                offices.Add(item.Name);
            }
            _ = MainWindowComunication.OpenPageWithWait(async () => new MainStatList(offices), this);

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ProxyInit();
        }

        private void ProxyInit()
        {
            try
            {
                Proxy = new RouterProxy();
                RegionOffices = Proxy.GetRegionOffices();
            }
            catch (System.ServiceModel.EndpointNotFoundException ex)
            {
                MainWindowComunication.ShowError("Сервіс маршрутизації недоступний");
            }
            catch (Exception ex)
            {
                MainWindowComunication.ShowError(ex.Message);
            }
        }
    }
}
