using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace lab4 {
    /// <summary>
    /// Logika interakcji dla klasy EventRegistrationPage.xaml
    /// </summary>
    public partial class EventRegistrationPage : Page
    {
        private User user;
        public EventRegistrationPage(User user)
        {
            InitializeComponent();
            this.user = user;
            List<Event> events = DatabaseHandler.ReadAll<Event>();
            foreach (Event e in events) {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = e.EventName;
                eventNameComboBox.Items.Add(item);
            }
        }

        private void OnEventNameChanged(object sender, SelectionChangedEventArgs e) {
            if (eventNameComboBox.SelectedItem != null) {
                ComboBoxItem selectedItem = (ComboBoxItem)eventNameComboBox.SelectedItem;
                string eventName = selectedItem.Content.ToString();
                Event selectedEvent = DatabaseHandler.ReadEventWithName(eventName);
                if (selectedEvent != null) {
                    eventAgendaTextBox.Text = selectedEvent.Agenda;
                }
            }
        }

        private void OnRegisterOnEvent(object sender, RoutedEventArgs e) {
            string eventName = eventNameComboBox.Text;
            string eventAgenda = eventAgendaTextBox.Text;
            string participationType = participationTypeComboBox.Text;
            string foodType = foodTypeComboBox.Text;
            DateTime? eventDate = eventDatePicker.SelectedDate;
            string eventDateToString = eventDate?.ToString("dd-MM-yyyy");

            if (string.IsNullOrWhiteSpace(eventName) || string.IsNullOrWhiteSpace(eventAgenda)
                || string.IsNullOrWhiteSpace(participationType) || string.IsNullOrWhiteSpace(foodType)
                || eventDate == null) {
                popup.IsOpen = true;
                popupText.Content = "Uzupełnij wszystkie dane.";
            } else {
                RegisteredEvent newEvent = new RegisteredEvent(eventName, eventAgenda, eventDateToString, 
                    participationType,foodType, false, user.Id);
                DatabaseHandler.Create(newEvent);
                eventNameComboBox.SelectedItem = null;
                eventAgendaTextBox.Text = "";
                participationTypeComboBox.SelectedItem = null;
                foodTypeComboBox.SelectedItem = null;
                eventDatePicker.SelectedDate = null;
                popup.IsOpen = true;
                popupText.Content = "Pomyślnie zarejestrowano na wydarzenie.";
            }
        }

        private void OnClosePopup(object sender, RoutedEventArgs e) {
            popup.IsOpen = false;
        }

        private void OnLogOut(object sender, RoutedEventArgs e) {
            Window mainWindow = Application.Current.MainWindow;
            Frame mainFrame = ((MainWindow)mainWindow).Main;
            mainFrame.Navigate(new Uri("LoginPage.xaml", UriKind.Relative));
        }
    }
}
