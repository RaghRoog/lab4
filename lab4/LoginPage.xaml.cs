using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace lab4
{
    /// <summary>
    /// Logika interakcji dla klasy LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private User user;
        private int loginAttempts = 0;

        public LoginPage()
        {
            InitializeComponent();
        }

        private bool LogIn() {
            string login = loginTextBox.Text;
            string pwd = passwordBox.Password.ToString();
            user = DatabaseHandler.ReadUserWithLogin(login);

            if (user != null && user.Password == pwd) {
                return true;
            }else {
                return false;
            }
        }
        
        private void OnLogin(object sender, RoutedEventArgs e)
        {
            Window mainWindow = Application.Current.MainWindow;
            Frame mainFrame = ((MainWindow)mainWindow).Main;
            if(loginAttempts < 3) {
                if (LogIn()) {
                    if(user.Permissions == "user") {
                        EventRegistrationPage eventRegistrationPage = new EventRegistrationPage(user);
                        mainFrame.Navigate(eventRegistrationPage);
                    }else if(user.Permissions == "admin") {
                        mainFrame.Navigate(new Uri("AdministratorPage.xaml", UriKind.Relative));
                    }
                } else {
                    loginAttempts++;
                    popupText.Content = "Nieprawidłowe dane logowania.";
                    invalidLoginData.IsOpen = true;
                }
            } else {
                popupText.Content = "Przekroczono maksymalną liczbę prób logowania.";
                invalidLoginData.IsOpen = true;
            }
        }

        private void OnNoAcc(object sender, MouseButtonEventArgs e)
        {
            Window mainWindow = Application.Current.MainWindow;
            Frame mainFrame = ((MainWindow)mainWindow).Main;
            mainFrame.Navigate(new Uri("RegistrationPage.xaml", UriKind.Relative));
        }

        private void OnShowPasswordChecked(object sender, RoutedEventArgs e)
        {
            string password = passwordBox.Password;
            passwordBox.Visibility = Visibility.Collapsed;
            passwordVisibleBox.Visibility = Visibility.Visible;
            passwordVisibleBox.Text = password;
        }

        private void OnShowPasswordUnchecked(object sender, RoutedEventArgs e)
        {
            string password = passwordVisibleBox.Text;
            passwordBox.Visibility = Visibility.Visible;
            passwordVisibleBox.Visibility = Visibility.Collapsed;
            passwordBox.Password = password;
        }

        private void OnClosePopup(object sender, RoutedEventArgs e) {
            invalidLoginData.IsOpen = false;
        }
    }
}
