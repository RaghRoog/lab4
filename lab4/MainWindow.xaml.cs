using System.Windows;


namespace lab4 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            Main.Content = new LoginPage();
            new DatabaseHandler();
        }
    }
}
