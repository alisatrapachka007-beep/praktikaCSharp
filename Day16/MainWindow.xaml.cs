using System;
using System.Windows;
using System.Windows.Threading;
using Day15.Services;
using Day15.ViewModels;

namespace Day15
{
    public partial class MainWindow : Window
    {
        private readonly AuthService _authService;
        private readonly DataService _dataService;
        private readonly NamedPipeService _pipeService;
        private readonly NotificationService _notificationService;
        private ChatWindow _chatWindow;

        public MainWindow(AuthService authService, DataService dataService)
        {
            InitializeComponent();
            _authService = authService;
            _dataService = dataService;

            _pipeService = new NamedPipeService(Dispatcher);
            _notificationService = new NotificationService(Dispatcher);

            if (_authService.IsDoctor)
            {
                _ = _pipeService.StartServerAsync();
            }

            _notificationService.StartListening();
            _notificationService.NotificationReceived += OnNotificationReceived;

            DataContext = new MainViewModel(_dataService, _authService, _notificationService);

            UpdateStatusBar();
        }

        private void UpdateStatusBar()
        {
            if (_authService.IsAuthenticated)
            {
                string roleText = _authService.IsDoctor ? "Врач" : "Пациент";
                UserInfoTextBlock.Text = $"{roleText} | {_authService.CurrentUser.FullName}";
                StatusTextBlock.Text = $"Подключен как: {_authService.CurrentUser.Login}";
            }
        }

        private void OnNotificationReceived(string notification)
        {
            Dispatcher.Invoke(() =>
            {
                var notificationWindow = new NotificationWindow(notification);
                notificationWindow.Show();

                StatusTextBlock.Text = notification;

                var timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(3);
                timer.Tick += (s, e) =>
                {
                    StatusTextBlock.Text = "Готов";
                    timer.Stop();
                };
                timer.Start();
            });
        }

        private void OpenChatMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (_chatWindow == null || !_chatWindow.IsVisible)
            {
                _chatWindow = new ChatWindow(_authService, _dataService, _pipeService);
                _chatWindow.Closed += (s, args) => _chatWindow = null;
                _chatWindow.Show();
            }
            else
            {
                _chatWindow.Activate();
            }
        }

        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "Медицинские записи\nВерсия 2.0\n\n" +
                "Горячие клавиши:\n" +
                "Ctrl+N - Новый пациент\n" +
                "Ctrl+R - Добавить запись\n" +
                "Ctrl+E - Редактировать запись\n" +
                "Ctrl+Shift+C - Открыть чат\n" +
                "Del - Удалить запись",
                "О программе",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _pipeService.StopServer();
            _notificationService.StopListening();
        }
    }
}