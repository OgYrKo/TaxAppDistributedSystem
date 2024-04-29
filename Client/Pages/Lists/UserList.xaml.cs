using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Client.Classes;
using Client.Pages.Cards;
using DBClassesLibrary;
using InterfacesLibrary;

namespace Client.Pages.Lists
{

    public partial class UserList : Page
    {
        ISettings Channel;
        ObservableCollection<User> Users;
        int SelectedIndex;
        public UserList()
        {
            
            InitializeComponent();
            Connection<ISettings> connection = new Connection<ISettings>("ISettings");
            Channel = connection.channel;
            Users = new ObservableCollection<User>(Channel.GetAllUsers());
            Data.ItemSource = Users;
        }

        private void Handler(DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.Column.Header.ToString())
            {
                case "Usesysid":
                    e.Column.Header = "Ключ";
                    break;
                case "Usename":
                    e.Column.Header = "Логін";
                    break;
                case "Rolname":
                    e.Column.Header = "Роль";
                    break;
                default:
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
            }

        }

        private void Data_OnClick(object o)
        {
            var row = o as DataGridRow;
            var emp = row.DataContext as User;
            if (emp != null)
            {
                SelectedIndex = Users.IndexOf(emp);
                UserCard userCard = new UserCard(Channel, emp);
                userCard.OnClose += OnCloseCard;
                userCard.OnSave += OnSaveCard;
                MainWindowComunication.OpenPage(userCard, this);
            }
        }

        private void AddCard(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SelectedIndex = -1;
            UserCard userCard = new UserCard(Channel);
            userCard.OnClose += OnCloseCard;
            userCard.OnSave += OnSaveCard;

            MainWindowComunication.OpenPage(userCard, this);
        }

        private void OnSaveCard(object sender, EventArgs e)
        {
            User user = sender as User;
            if (SelectedIndex == -1)
            {
                Users.Add(user);
                SelectedIndex = Users.Count - 1;
            }
            else
            {
                Users[SelectedIndex] = user;
            }
            Data.InvalidateVisual();
        }

        private void OnCloseCard(object sender, EventArgs e)
        {
            // Получаем текущий элемент навигации
            if (sender == null)
            {
                Users.RemoveAt(SelectedIndex);
                Data.InvalidateVisual();
            }
        }

    }
}
