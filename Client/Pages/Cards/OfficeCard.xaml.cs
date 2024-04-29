using Client.Classes;
using Client.Controls;
using Client.Enums;
using MaterialDesignThemes.Wpf;
using RouterInterfaces;
using RouterLib;
using System;
using System.ComponentModel;
using System.ServiceModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace Client.Pages.Cards
{
    public partial class OfficeCard : Page, INotifyPropertyChanged
    {
        private RouterProxy Proxy;
        private CardState State;
        private EditableStatus cardEditableStatus;
        public EditableStatus CardEditableStatus
        { 
            get => cardEditableStatus;
            set
            {
                cardEditableStatus = value;
                OnPropertyChanged(nameof(IsEditable));
                OnPropertyChanged(nameof(IsNameEditable));
            }
        }


        public string RegionOfficeName { get; set; }
        public string Link { get; set; }
        public bool IsNameEditable { get => IsEditable && (State == CardState.New ? true : false); }
        public bool IsEditable { get => CardEditableStatus == EditableStatus.Editable ? true : false; }
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler OnSave;
        public event EventHandler OnClose;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public OfficeCard(RegionOffice regionOffice)
        {
            InitializeComponent();
            RegionOfficeName = regionOffice.Name;
            Link = regionOffice.URI;
            CardEditableStatus= EditableStatus.NonEditable;
            State = CardState.Exist;
            Init();
        }

        public OfficeCard()
        {
            InitializeComponent();
            State = CardState.New;
            CardEditableStatus = EditableStatus.Editable;
            Init();
        }

        private void Init()
        {
            SetSubMenuButtons();
            DataContext = this;
            Proxy = new RouterProxy();
        }

        private void CheckFields() { }

        private void SetSubMenuButtons()
        {
            SubMenuItem[] subMenuItems = (SubMenuItem[])subMenu.ItemsSource;
            switch (CardEditableStatus)
            {
                case EditableStatus.Editable:
                    subMenuItems[0].Kind = PackIconKind.ContentSave;
                    subMenuItems[0].Text = "Зберегти";
                    subMenuItems[1].IsEnabled = false;
                    break;
                case EditableStatus.NonEditable:
                    subMenuItems[0].Kind = PackIconKind.Edit;
                    subMenuItems[0].Text = "Редагувати";
                    subMenuItems[1].IsEnabled = true;
                    break;
            }
        }

        private void SaveAndEditClick(object sender, MouseButtonEventArgs e)
        {
            
            switch (CardEditableStatus)
            {
                case EditableStatus.Editable:
                    Save();
                    break;

            }
            CardEditableStatus = IsEditable ? EditableStatus.NonEditable : EditableStatus.Editable;
            SetSubMenuButtons();
        }

        private void Save()
        {
            string name = RegionOfficeName;
            string address = Link;
            string binding = DetermineRecommendedBinding(address);

            if (binding == null) throw new Exception("Binding error");
            switch (State)
            {
                case CardState.New:
                    if (!MainWindowComunication.IsAnswerCorrect(Proxy.AddRegionOffice(name, address, binding))) return;
                    State = CardState.Exist;
                    break;
                case CardState.Exist:
                    MainWindowComunication.IsAnswerCorrect(Proxy.EditRegionOffice(name, address, binding));
                    break;
            }
            if(OnSave!=null)OnSave(new RegionOffice() { Name=name,URI=address}, null);
        }

        private void Delete(object sender, MouseButtonEventArgs e)
        {
            string name = RegionOfficeName;
            MainWindowComunication.IsAnswerCorrect(Proxy.DeleteRegionOffice(name));
            MainWindowComunication.ClosePage(this);
            if (OnClose != null) OnClose(null, null);
        }

        private string DetermineRecommendedBinding(string hostAddress)
        {
            // Проверка типа адреса
            if (hostAddress.StartsWith("http://"))
            {
                // Адрес начинается с "http://", рекомендуется использовать привязку HTTP
                return "basicHttpBinding";
            }
            else if (hostAddress.StartsWith("net.tcp://"))
            {
                // Адрес начинается с "net.tcp://", рекомендуется использовать привязку TCP
                return "NetTcpBinding";
            }
            else if (hostAddress.StartsWith("net.pipe://"))
            {
                // Адрес начинается с "net.pipe://", рекомендуется использовать привязку Named Pipes
                return "NetNamedPipeBinding";
            }
            else if (hostAddress.StartsWith("net.msmq://"))
            {
                // Адрес начинается с "net.msmq://", рекомендуется использовать привязку MSMQ
                return "NetMsmqBinding";
            }
            else
            {
                // Неизвестный тип адреса, можно вернуть значение по умолчанию или генерировать исключение
                return null;
            }
        }
    }
}
