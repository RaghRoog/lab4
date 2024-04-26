using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace lab4 {
    /// <summary>
    /// Logika interakcji dla klasy RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page {
        User user;

        public RegistrationPage() {
            InitializeComponent();
        }

        private bool RegisterUser() {
            string fname = fNameTextBox.Text;
            string lname = lNameTextBox.Text;
            string login = loginTextBox.Text;
            string email = emailTextBox.Text;
            string pwd = pwdTextBox.Password.ToString();
            string repeatPwd = repeatPwdTextBox.Password.ToString();
            string permissions = "user";
            DateTime date = DateTime.Now;
            string dateToString = date.ToString("dd-MM-yyyy");

            bool isDataOk = true;
            if (fname == "" || lname == "" || login == "" || email == "" || pwd == "") {
                isDataOk = false;
                popupText.Text += "Dane nie są kompletne\n";
            }
            if (!ValidateEmail(email)) {
                isDataOk = false;
                popupText.Text += "Błędny adres e-mail\n";
            }
            if (!ValidatePassword(pwd)) {
                isDataOk = false;
                popupText.Text += "Błędne hasło (8 znaków, min. 1 wielka litera, 1 mała litera i 1 cyfra)\n";
            }
            if (pwd != repeatPwd) {
                isDataOk = false;
                popupText.Text += "Hasła nie są takie same\n";
            }

            if (isDataOk) {
                user = new User(fname, lname, login, pwd, email, permissions, dateToString);
                DatabaseHandler.Create(user);
                return true;
            } else {
                return false;
            }
        }

        private bool ValidateEmail(string email) {
            string emailRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, emailRegex);
        }

        private bool ValidatePassword(string pwd) {
            string pwdRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$";
            return Regex.IsMatch(pwd, pwdRegex);
        }

        private void OnRegister(object sender, RoutedEventArgs e) {
            Window mainWindow = Application.Current.MainWindow;
            Frame mainFrame = ((MainWindow)mainWindow).Main;
            if (RegisterUser()) {
                EventRegistrationPage eventRegistrationPage = new EventRegistrationPage(user);
                mainFrame.Navigate(eventRegistrationPage);
            } else {
                invalidRegDataPopup.IsOpen = true;
            }
        }

        private void OnHaveAcc(object sender, MouseButtonEventArgs e) {

            Window mainWindow = Application.Current.MainWindow;
            Frame mainFrame = ((MainWindow)mainWindow).Main;
            mainFrame.Navigate(new Uri("LoginPage.xaml", UriKind.Relative));
        }

        private void OnClosePopup(object sender, RoutedEventArgs e) {
            invalidRegDataPopup.IsOpen = false;
            popupText.Text = "";
        }
    }
}
