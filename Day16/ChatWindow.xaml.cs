using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using Day15.Models;
using Day15.Services;

namespace Day15
{
    public partial class ChatWindow : Window
    {
        private readonly AuthService _authService;
        private readonly DataService _dataService;
        private readonly NamedPipeService _pipeService;
        private readonly DispatcherTimer _refreshTimer;
        private ObservableCollection<ChatMessage> _messages;

        public ChatWindow(AuthService authService, DataService dataService, NamedPipeService pipeService)
        {
            InitializeComponent();
            _authService = authService;
            _dataService = dataService;
            _pipeService = pipeService;
            _messages = new ObservableCollection<ChatMessage>();
            MessagesListBox.ItemsSource = _messages;

            _pipeService.MessageReceived += OnMessageReceived;

            _refreshTimer = new DispatcherTimer();
            _refreshTimer.Interval = TimeSpan.FromSeconds(2);
            _refreshTimer.Tick += (s, e) => LoadMessages();
            _refreshTimer.Start();

            LoadMessages();

            Title = $"Чат - {_authService.CurrentUser?.FullName}";
        }

        private void LoadMessages()
        {
            var currentUserId = _authService.CurrentUser?.Id ?? 0;
            var messages = _dataService.ChatMessages
                .Where(m => m.FromUserId == currentUserId || m.ToUserId == currentUserId)
                .OrderBy(m => m.Timestamp)
                .ToList();

            Dispatcher.Invoke(() =>
            {
                _messages.Clear();
                foreach (var m in messages)
                    _messages.Add(m);

                if (MessagesListBox.Items.Count > 0)
                    MessagesListBox.ScrollIntoView(MessagesListBox.Items[MessagesListBox.Items.Count - 1]);
            });
        }

        private void OnMessageReceived(string message)
        {
            Dispatcher.Invoke(() =>
            {
                var parts = message.Split('|');
                if (parts.Length >= 3)
                {
                    var newMessage = new ChatMessage
                    {
                        Id = _dataService.ChatMessages.Count + 1,
                        FromUserId = int.Parse(parts[0]),
                        FromUserName = parts[1],
                        Message = parts[2],
                        Timestamp = DateTime.Now,
                        IsRead = false
                    };
                    _dataService.ChatMessages.Add(newMessage);
                    _dataService.SaveMessages();
                    LoadMessages();
                }
            });
        }

        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MessageTextBox.Text))
                return;

            var message = $"{_authService.CurrentUser.Id}|{_authService.CurrentUser.FullName}|{MessageTextBox.Text.Trim()}";
            await _pipeService.SendMessageAsync(message);

            var newMessage = new ChatMessage
            {
                Id = _dataService.ChatMessages.Count + 1,
                FromUserId = _authService.CurrentUser.Id,
                FromUserName = _authService.CurrentUser.FullName,
                ToUserId = 0,
                Message = MessageTextBox.Text.Trim(),
                Timestamp = DateTime.Now,
                IsRead = true
            };
            _dataService.ChatMessages.Add(newMessage);
            _dataService.SaveMessages();

            MessageTextBox.Clear();
            LoadMessages();
        }

        protected override void OnClosed(EventArgs e)
        {
            _refreshTimer.Stop();
            base.OnClosed(e);
        }
    }
}