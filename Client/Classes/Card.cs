using Client.Controls;
using Client.Enums;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Client.Classes
{
    abstract public partial class Card: Page, INotifyPropertyChanged
    {
        protected abstract SubMenu subMenu { get; set; }
        protected CardState State;
        protected EditableStatus cardEditableStatus;
        public virtual EditableStatus CardEditableStatus
        {
            get => cardEditableStatus;
            set
            {
                cardEditableStatus = value;
                OnPropertyChanged(nameof(IsEditable));
            }
        }


        public bool IsNameEditable { get => IsEditable && (State == CardState.New ? true : false); }
        public bool IsEditable { get => CardEditableStatus == EditableStatus.Editable ? true : false; }
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler OnSave;
        public event EventHandler OnClose;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected abstract void CheckFields();

        protected virtual void SetSubMenuButtons()
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

        protected virtual void SaveAndEditClick(object sender, MouseButtonEventArgs e)
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

        protected abstract void Save();

        protected abstract void Delete(object sender, MouseButtonEventArgs e);

    }
}
