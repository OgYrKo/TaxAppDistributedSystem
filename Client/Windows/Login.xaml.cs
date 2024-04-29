using Client.Classes;
using Client.Enums;
using Client.Pages.Menus;
using InterfacesLibrary;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Client.Windows
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void buttonConfirm_Click(object sender, RoutedEventArgs e)
        {
            errorMessage.Visibility = Visibility.Hidden;
            progressBar.Visibility = Visibility.Visible;
            string password = txtPassword.Password;
            string username = txtUsername.Text;

            Task.Run(() => CheckUser(username, password));
        }
        private void CheckUser(string username, string password)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                try
                {
                    Connection<ISettings> connection = new Connection<ISettings>("ISettings");
                    var channel = connection.channel;
                    string response = channel.SetConnectionString(username, password);

                    this.Dispatcher.Invoke(() =>
                    {
                        if (response.Equals("-1"))
                        {
                            txtPassword.Password = "";
                            errorMessage.Content = "Логін або пароль невірні!";
                            errorMessage.Visibility = Visibility.Visible;

                        }
                        else
                        {

                            UserPosition userPosition = UserPosition.None;
                            if (response.Equals("admin_group"))
                            {
                                RegionOfficeSelectionWindow selectionWindow = new RegionOfficeSelectionWindow();
                                userPosition = UserPosition.Admin;
                                selectionWindow.Show();
                            }
                            else if (response.Equals("manager_group"))
                            {
                                MainWindow mainManagerWindow = new MainWindow(new RegionOfficeManager());
                                userPosition = UserPosition.Manager;
                                mainManagerWindow.Show();
                            }

                            CurrentUser.Instance(txtUsername.Text, txtPassword.Password, userPosition);
                            this.Close();
                        }
                    });
                }
                catch (System.ServiceModel.EndpointNotFoundException)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        errorMessage.Content = "Сервер не відповідає!";
                        errorMessage.Visibility = Visibility.Visible;
                    });
                }
                catch (Exception ex)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        errorMessage.Content = ex.Message;
                        errorMessage.Visibility = Visibility.Visible;
                    });
                }
            }
            else
            {
                this.Dispatcher.Invoke(() =>
                {
                    errorMessage.Content = "Дані не введено!";
                    errorMessage.Visibility = Visibility.Visible;
                });
            }
            this.Dispatcher.Invoke(() =>
            {
                progressBar.Visibility = Visibility.Collapsed;
            });
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                buttonConfirm_Click(this, e);
            }
        }
    }
}
