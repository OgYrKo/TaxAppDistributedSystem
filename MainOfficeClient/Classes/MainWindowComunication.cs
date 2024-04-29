
using MainOfficeClient.Pages;
using MainOfficeClient.Windows.Dialogs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MainOfficeClient.Classes
{
    internal class MainWindowComunication
    {
        private static WaitPage WaitPage = new WaitPage();

        public static void OpenPage(Page page, DependencyObject dependency)
        {
            var mainWindow = Window.GetWindow(dependency);
            Frame mainFrame = mainWindow.FindName("Main") as Frame;
            if (mainFrame.Content == page) return;
            if (mainFrame != null)
            {
                mainFrame.Content = page;
            }
        }


        public static async Task OpenPageWithWait(Func<Task<Page>> pageInitializer, DependencyObject dependency)
        {

            OpenWait(dependency);
            await Task.Delay(100);
            try
            {
                var page = await pageInitializer.Invoke();
                OpenPage(page, WaitPage);
                await Task.Delay(100);

                page.NavigationService.RemoveBackEntry();
            }
            catch (System.ServiceModel.EndpointNotFoundException ex)
            {
                ShowError("Сервіс недоступний");
                ClosePage(WaitPage);
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
                ClosePage(WaitPage);
            }
        }

        public static void OpenWait(DependencyObject dependency)
        {
            var mainWindow = Window.GetWindow(dependency);
            Frame mainFrame = mainWindow.FindName("Main") as Frame;
            if (mainFrame != null)
            {
                mainFrame.Content = WaitPage;
            }
        }

        public static void ClosePage(Page page)
        {
            if (page.NavigationService.CanGoBack)
            {
                page.NavigationService.GoBack();
                page.NavigationService.RemoveBackEntry();
            }
        }

        public static bool IsAnswerCorrect(string answer)
        {
            List<string> positiveAnswers = new List<string>() { "OK", "ОК" };
            if (!positiveAnswers.Exists(positiveAnswer => positiveAnswer.ToLower().Equals(answer.ToLower())))
            {
                MessageBox.Show(answer);
                return false;
            }
            return true;
        }

        public static void ShowError(string message)
        {
            new ErrorDialog(message).ShowDialog();
            //MessageBox.Show(message, "Error",MessageBoxButton.OK,MessageBoxImage.Error);
        }

    }
}
