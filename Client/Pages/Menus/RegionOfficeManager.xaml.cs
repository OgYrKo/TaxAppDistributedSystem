using Client.Classes;
using Client.Pages.Lists;
using DBClassesLibrary;
using InterfacesLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// <summary>
    /// Interaction logic for RegionOfficeManager.xaml
    /// </summary>
    public partial class RegionOfficeManager : Page
    {
        IFullData Channel;
        ObservableCollection<Landplotfull> Lands;
        ObservableCollection<Realpropertyfull> Properties;
        Mutex connectionMutex = new Mutex();
        AutoResetEvent SetLandsEvent = new AutoResetEvent(false);
        bool IsSetLandsEventComplete = false;
        AutoResetEvent SetCounterpartiesEvent = new AutoResetEvent(false);
        bool IsSetCounterpartiesEventComplete = false;
        AutoResetEvent SetPropertiesEvent = new AutoResetEvent(false);
        bool IsSetPropertiesEventComplete = false;

        public RegionOfficeManager()
        {
            new Thread(InitConnection).Start();
            GetDataFromServer();
            InitializeComponent();
        }

        private void InitConnection()
        {
            connectionMutex.WaitOne();
            Connection<IFullData> connection = new Connection<IFullData>("IFullData");
            Channel = connection.channelWithUser;
            connectionMutex.ReleaseMutex();
        }

        private void GetDataFromServer()
        {
            try
            {
                new Thread(SetLands).Start();
                new Thread(SetCounterparties).Start();
                new Thread(SetProperties).Start();
            }
            catch (Exception ex)
            {
                MainWindowComunication.ShowError(ex.Message);
            }
        }

        private void WaitConnectionMutex()
        {
            connectionMutex.WaitOne();
            connectionMutex.ReleaseMutex();
        }

        private void SetLands()
        {
            WaitConnectionMutex();
            Lands = new ObservableCollection<Landplotfull>(Channel.GetAllLands());
            IsSetLandsEventComplete = true;
            SetLandsEvent.Set();
        }
        private void SetCounterparties()
        {
            WaitConnectionMutex();
            //Lands = new ObservableCollection<Landplotfull>(Channel.GetAllLands());
            IsSetCounterpartiesEventComplete = true;
            SetCounterpartiesEvent.Set();
        }
        private void SetProperties()
        {

            WaitConnectionMutex();
            Properties = new ObservableCollection<Realpropertyfull>(Channel.GetAllRealProperties());
            IsSetPropertiesEventComplete = true;
            SetPropertiesEvent.Set();
        }

        private void OpenLandList(object sender, RoutedEventArgs e)
        {
            if (IsSetLandsEventComplete)
                MainWindowComunication.OpenPage(new LandList(Lands), this);
            else
                _ = MainWindowComunication.OpenPageWithWait(SetLandsEvent, async () => new LandList(Lands), this);
        }

        private void OpenTaskList(object sender, RoutedEventArgs e)
        {
            _ = MainWindowComunication.OpenPageWithWait(async () => new TaskList(), this);
        }
        private void OpenPropertyList(object sender, RoutedEventArgs e)
        {
            if (IsSetPropertiesEventComplete)
                MainWindowComunication.OpenPage(new PropertyList(Properties), this);
            else
                _ = MainWindowComunication.OpenPageWithWait(SetPropertiesEvent, async () => new PropertyList(Properties), this);
        }
        private void OpenCounterpartyList(object sender, RoutedEventArgs e)
        {

        }
    }
}
