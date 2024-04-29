using Client.Classes;
using Client.Controls;
using Client.Enums;
using Client.Pages.Cards;
using RouterInterfaces;
using RouterLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace Client.Pages.Lists
{
    public partial class OfficeList : Page
    {
        List<RegionOffice> RegionOffices;
        ObservableCollection<ListItem> ListItems;
        private RouterProxy Proxy;
        private int SelectedIndex;

        public OfficeList(RouterProxy proxy, ref List<RegionOffice> offices)
        {
            this.Proxy = proxy;
            this.RegionOffices = offices;
            InitializeComponent();
            SetItemSource();
        }

        public OfficeList()
        {
            InitChannel();
            InitializeComponent();
            RegionOffices = Proxy.GetRegionOffices();
            SetItemSource();
        }

        private void InitChannel()
        {
            Proxy = new RouterProxy();
        }

        private void SetItemSource()
        {
            ListItems=new ObservableCollection<ListItem>();
            foreach (var item in RegionOffices)
            {
                AddRegionOfficeToXAML(item);
            }
            Data.ItemsSource = ListItems;
        }

        private void AddRegionOfficeToXAML(RegionOffice regionOffice)
        {
            ListItem item= new ListItem();
            item.Text=regionOffice.Name;
            item.MouseUp += OpenCard;
            ListItems.Add(item);
        }

        private void UpdateRegionOffice(RegionOffice regionOffice)
        {
            RegionOffices[SelectedIndex] = regionOffice;
        }

        private void OnSaveCard(object sender, EventArgs e)
        {
            RegionOffice regionOffice = sender as RegionOffice;
            if (SelectedIndex == -1)
            {
                RegionOffices.Add(regionOffice);
                AddRegionOfficeToXAML(regionOffice);
                Data.InvalidateVisual();
                SelectedIndex = RegionOffices.Count - 1;
            }
            else
            {
                UpdateRegionOffice(regionOffice);
            }
        }

        private void OnCloseCard(object sender, EventArgs e)
        {
            // Получаем текущий элемент навигации
            if (sender == null) 
            { 
                RegionOffices.RemoveAt(SelectedIndex);
                ListItems.RemoveAt(SelectedIndex);
            }
        }

        private void AddCard(object sender, MouseButtonEventArgs e)
        {
            SelectedIndex = -1;
            OpenCard(new OfficeCard());
        }

        private void OpenCard(object sender, MouseButtonEventArgs e)
        {
            ListItem item = (ListItem)sender;
            SelectedIndex = RegionOffices.FindIndex(r => r.Name.Equals(item.Text));
            OpenCard(new OfficeCard(RegionOffices[SelectedIndex]));
        }

        private void OpenCard(OfficeCard officeCard)
        {
            officeCard.OnClose += OnCloseCard;
            officeCard.OnSave += OnSaveCard;
            MainWindowComunication.OpenPage(officeCard, this);
        }

    }
}
