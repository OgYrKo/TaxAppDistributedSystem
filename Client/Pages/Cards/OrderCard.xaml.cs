using Client.Classes;
using Client.Controls;
using Client.Windows.Dialogs;
using DBClassesLibrary;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace Client.Pages.Cards
{
    public partial class OrderCard : Page, INotifyPropertyChanged
    {

        private bool isEditable;

        public bool IsEditable
        {
            get => isEditable;
            set
            {
                if (isEditable != value)
                {
                    isEditable = value;
                    OnPropertyChanged(nameof(IsEditable));
                }
            }
        }

        public event EventHandler OnClose;
        public event EventHandler OnSave;
        public Counterpartyorder LocalOrder { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public OrderCard(Counterpartyorder order)
        {
            LocalOrder = order;
            IsEditable = false;
            InitializeComponent();
            DataContext = this;
        }


        private void ChangeEnabled()
        {
            IsEditable = !IsEditable;
        }

        private void ChangeButtons(SubMenuItem item)
        {
            if (IsEditable)
            {
                item.Kind = PackIconKind.ContentSave;
                item.Text = "Зберегти";
            }
            else
            {
                item.Kind = PackIconKind.Edit;
                item.Text = "Редагувати";
            }
        }

        

        private void Edit_Save(object sender, MouseButtonEventArgs e)
        {
            if (IsEditable)
                Save();
            ChangeEnabled();
            ChangeButtons((SubMenuItem)sender);
        }

        private void Delete(object sender, MouseButtonEventArgs e)
        {
            if(new CheckDialog().ShowDialog() == true)
            {
                LocalOrder = null;
                Close();
            }
            
        }

        private void Save()
        {
            if(OnSave != null)OnSave(LocalOrder,EventArgs.Empty);
        }

        private void Close()
        {
            if (OnClose != null)
                OnClose(LocalOrder, EventArgs.Empty);
            MainWindowComunication.ClosePage(this);
        }
    }
}
