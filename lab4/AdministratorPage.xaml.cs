using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace lab4 {
    /// <summary>
    /// Interaction logic for AdministratorPage.xaml
    /// </summary>
    public partial class AdministratorPage : Page {
        private List<RegisteredEvent> registredEvents = DatabaseHandler.ReadAll<RegisteredEvent>();

        public AdministratorPage() {
            InitializeComponent();
            DisplayEvents();
        }

        private void OnAddUser(object sender, RoutedEventArgs e) {
            string fname = addUserFnameTextBox.Text;
            string lname = addUserLnameTextBox.Text;
            string login = addUserLoginTextBox.Text;
            string password = addUserPasswordTextBox.Text;
            string email = addUserEmailTextBox.Text;
            string permissions = addUserPermComboBox.Text;
            DateTime date = DateTime.Now;
            string dateToString = date.ToString("dd-MM-yyyy");
            User newUser = new User(fname, lname, login, password, email, permissions, dateToString);
            DatabaseHandler.Create(newUser);
            addUserFnameTextBox.Text = "";
            addUserLnameTextBox.Text = "";
            addUserLoginTextBox.Text = "";
            addUserPasswordTextBox.Text = "";
            addUserEmailTextBox.Text = "";
        }

        private void OnDelUser(object sender, RoutedEventArgs e) {
            string login = delUserLoginTextBox.Text;
            User user = DatabaseHandler.ReadUserWithLogin(login);
            if(user != null) {
                DatabaseHandler.Delete(user);
                delUserLoginTextBox.Text = "";
            } else {
                popup.IsOpen = true;
                popupText.Content = "Nie ma użytkownika o takim loginie";
            }
        }

        private void OnResetPwd(object sender, RoutedEventArgs e) {
            string login = resetPwdLoginTextBox.Text;
            string newPwd = newPwdTextBox.Text;
            User user = DatabaseHandler.ReadUserWithLogin(login);
            if(user != null) {
                DatabaseHandler.ResetPassword(user, newPwd);
                resetPwdLoginTextBox.Text = "";
                newPwdTextBox.Text = "";
            } else {
                popup.IsOpen = true;
                popupText.Content = "Nie ma użytkownika o takim loginie";
            }
        }

        private void OnClosePopup(object sender, RoutedEventArgs e) {
            popup.IsOpen = false;
        }

        private void OnAddEvent(object sender, RoutedEventArgs e) {
            string eventName = addEventNameTextBox.Text;
            string eventAgenda = addEventAgendaTextBox.Text;
            Event newEvent = new Event(eventName, eventAgenda);
            DatabaseHandler.Create(newEvent);
            addEventNameTextBox.Text = "";
            addEventAgendaTextBox.Text = "";
        }

        private void OnDelEvent(object sender, RoutedEventArgs e) {
            string eventName = delEventNameTextBox.Text;
            Event eventToDelete = DatabaseHandler.ReadEventWithName(eventName);
            if(eventToDelete != null) {
                DatabaseHandler.Delete(eventToDelete);
                delEventNameTextBox.Text = "";
            } else {
                popup.IsOpen = true;
                popupText.Content = "Nie ma mydarzenia o takiej nazwie";
            }
        }

        private void OnModifyEvent(object sender, RoutedEventArgs e) {
            string eventName = modEventNameTextBox.Text;
            string newEventName = modEventNewNameTextBox.Text;
            string newAgenda = modEventNewAgendaTextBox.Text;
            Event searchedEvent = DatabaseHandler.ReadEventWithName(eventName);
            if(searchedEvent != null) {
                DatabaseHandler.ModifyEvent(searchedEvent, newEventName, newAgenda);
                modEventNameTextBox.Text = "";
                modEventNewNameTextBox.Text = "";
                modEventNewAgendaTextBox.Text = "";
            } else {
                popup.IsOpen = true;
                popupText.Content = "Nie mie wydarzenia o takiej nazwie";
            }
        }

        private void DisplayEvents() {
            foreach (var item in registredEvents) {
                Border border = new Border();
                border.BorderThickness = new Thickness(1);
                border.BorderBrush = Brushes.White;
                WrapPanel eventWrapPanel = new WrapPanel();
                eventWrapPanel.Orientation = Orientation.Horizontal;

                Label eventIdLabel = new Label();
                eventIdLabel.Content = "Id: " + item.Id;
                eventIdLabel.FontSize = 12;
                eventIdLabel.FontFamily = new System.Windows.Media.FontFamily("Arial");
                eventIdLabel.Foreground = Brushes.White;

                CheckBox acceptCheckBox = new CheckBox();
                acceptCheckBox.FontSize = 12;
                acceptCheckBox.FontFamily = new System.Windows.Media.FontFamily("Arial");
                acceptCheckBox.Foreground = Brushes.White;
                acceptCheckBox.Content = "Potwierdź";
                acceptCheckBox.Checked += OnAcceptCheckBoxChanged;
                acceptCheckBox.Unchecked += OnAcceptCheckBoxChanged;
                acceptCheckBox.Tag = item.Id;
                acceptCheckBox.IsChecked = item.IsAccepted;

                eventWrapPanel.Children.Add(eventIdLabel);
                eventWrapPanel.Children.Add(acceptCheckBox);

                border.Child = eventWrapPanel;

                allEventsWrapPanel.Children.Add(border);
            }
        }

        private void OnAcceptCheckBoxChanged(object sender, EventArgs e) {
            if (sender is CheckBox checkBox) {
                int eventID = (int)checkBox.Tag;
                RegisteredEvent registeredEvent = DatabaseHandler.ReadRegisteredEventWithId(eventID);
                if (registeredEvent != null) {
                    if (checkBox.IsChecked.GetValueOrDefault()) {
                        DatabaseHandler.ModifyRegisteredEvent(registeredEvent, true);
                    } else {
                        DatabaseHandler.ModifyRegisteredEvent(registeredEvent, false);
                    }
                }
            }
        }

        private void OnLogOut(object sender, RoutedEventArgs e) {
            Window mainWindow = Application.Current.MainWindow;
            Frame mainFrame = ((MainWindow)mainWindow).Main;
            mainFrame.Navigate(new Uri("LoginPage.xaml", UriKind.Relative));
        }
    }

}
