using System.Windows;
using Day15.Services;

namespace Day15
{
    public partial class LoginWindow : Window
    {
        private AuthService _authService;
        private DataService _dataService;

        public LoginWindow()
        {
            InitializeComponent();
            _dataService = new DataService();
            _authService = new AuthService(_dataService);
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text.Trim();
            string password = PasswordBox.Password;

            if (_authService.Login(login, password))
            {
                var mainWindow = new MainWindow(_authService, _dataService);
                mainWindow.Show();
                Close();
            }
            else
            {
                ErrorTextBlock.Text = "Неверный логин или пароль!";
                ErrorTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void TestDataButton_Click(object sender, RoutedEventArgs e)
        {
            LoginTextBox.Text = "doctor";
            PasswordBox.Password = "123";
            LoginButton_Click(sender, e);
        }
    }
}