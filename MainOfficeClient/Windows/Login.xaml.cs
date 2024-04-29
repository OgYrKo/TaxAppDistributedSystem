using MainOfficeClient;
using MainOfficeClient.Classes;
using System.Threading;
using System;
using System.Windows;
using System.Windows.Input;
using InterfacesLibrary;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace MainOfficeClient.Windows
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
                    RouterProxy Proxy = new RouterProxy();
                    string response = Proxy.CheckUser(username, password);
                    if (response.Equals("main_admin_group"))
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            MainWindow mainWindow = new MainWindow();
                            mainWindow.Show();
                            CurrentUser.Instance(username, password, response);
                            this.Close();
                        });
                    }
                    else
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            txtPassword.Password = "";
                            errorMessage.Content = response;
                            errorMessage.Visibility = Visibility.Visible;
                        });
                    }
                }
                catch (System.ServiceModel.EndpointNotFoundException)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        errorMessage.Content = "Сервіс маршрутизації недоступний";
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
