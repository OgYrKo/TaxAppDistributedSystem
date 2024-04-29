using Client.Classes;
using Client.Controls;
using Client.Enums;
using DBClassesLibrary;
using InterfacesLibrary;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Client.Pages.Cards
{
    public partial class UserCard : Page, INotifyPropertyChanged
    {
        private ISettings Channel;
        private User User;
        private List<AddititonalClass> user_groups;
        private CardState State;
        private EditableStatus cardEditableStatus;
        public EditableStatus CardEditableStatus
        {
            get => cardEditableStatus;
            set
            {
                cardEditableStatus = value;
                OnPropertyChanged(nameof(IsEditable));
            }
        }
        public bool IsEditable { get => CardEditableStatus == EditableStatus.Editable ? true : false; }
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler OnSave;
        public event EventHandler OnClose;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public UserCard(ISettings channel,User user)
        {
            Channel = channel;
            this.User = user;
            State = CardState.Exist;
            CardEditableStatus = EditableStatus.NonEditable;
            InitializeLists();
            InitializeComponent();
            InitializeBindings();
            FillFields();
            SetSubMenuButtons();
            DataContext = this;
        }

        public UserCard(ISettings channel)
        {
            Channel = channel;
            State = CardState.New;
            CardEditableStatus = EditableStatus.Editable;
            InitializeLists();
            InitializeComponent();
            InitializeBindings();
            SetSubMenuButtons();
            DataContext = this;
        }

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

        private void InitializeLists()
        {
            user_groups = new List<AddititonalClass>() { new AddititonalClass(){ Key = 1, Value = "Адмін" },
                                                    new AddititonalClass() { Key = 2, Value = "Менеджер" } };
        }

        private void InitializeBindings()
        {
            SetItemsSource(Group, user_groups, "Value", "Key");
        }

        private void SetItemsSource<T>(ComboField comboBox, List<T> list, string displayMemberPath, string SelectedValuePath)
        {
            comboBox.ItemsSource = list;
            comboBox.DisplayMemberPath = displayMemberPath;
            comboBox.SelectedValuePath = SelectedValuePath;
        }

        private void FillFields()
        {
            Key.Text = Convert.ToString(User.Usesysid);
            Login.Text = User.Usename;
            Password.Password = User.Passwd;
            if (User.Passwd == "admin")
            {
                MessageBox.Show($"1) Complete {User.Passwd}");
            }
            if (Password.Password == "admin")
            {
                MessageBox.Show($"2) Complete {Password.Password}");
            }
            Group.SelectedValue = User.Rolname == "admin_group" ? 1 : 2;
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
            switch (State)
            {
                case CardState.New:
                    if (!MainWindowComunication.IsAnswerCorrect(AddUser())) return;
                    State = CardState.Exist;
                    break;
                case CardState.Exist:
                    if (!MainWindowComunication.IsAnswerCorrect(EditUser())) return;
                    break;
            }
            if (OnSave != null) OnSave(User, null);
        }

        private string AddUser()
        {
            User = GetUserValues();
            if (Channel.AddUser(User).Equals("-1"))
            {
                return $"Користувач {Login.Text} існує!";
            }
            User = Channel.GetUserByName(User.Usename);
            FillFields();
            return "OK";
        }

        private void Delete(object sender, MouseButtonEventArgs e)
        {
            MainWindowComunication.IsAnswerCorrect(Channel.DeleteUser(User.Usename));
            MainWindowComunication.ClosePage(this);
            if (OnClose != null) OnClose(null, null);
        }

        private User GetUserValues()
        {
            bool check_flag = false;
            Brush brush = Key.BorderBrush;
            User new_user = new User();
            if (Login.Text == "")
            {
                Login.BorderBrush = Brushes.Red;
                check_flag = true;
            }
            else
            {
                Login.BorderBrush = brush;
                new_user.Usename = Login.Text;
            }
            if (Password.Password == "")
            {
                Password.BorderBrush = Brushes.Red;
                check_flag = true;
            }
            else
            {
                new_user.Passwd = Password.Password;
                Password.BorderBrush = brush;
            }
            switch (Group.SelectedValue as int?)
            {
                case 1:
                    new_user.Rolname = "admin_group";
                    break;
                case 2:
                    new_user.Rolname = "manager_group";
                    break;
                default:
                    check_flag = true;
                    break;
            }
            if (check_flag) throw new Exception();
            return new_user;
        }

        private string EditUser()
        {
            User = GetUserValues();
            User.Usesysid = Convert.ToUInt32(Key.Text);
            try
            {
                Channel.UpdateUser(User);
                return "OK";
            }
            catch
            {
                return "Помилка зміни користувача";
            }
        }

    }
}
