using MainOfficeClient.Classes;
using MainOfficeClient.Pages.Lists;
using RouterLib;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MainOfficeClient.Pages.Menus
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
            finally
            {
                MainWindowComunication.OpenPage(this, this);
            }
        }
    }
}
