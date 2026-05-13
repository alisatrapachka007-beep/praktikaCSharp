using System;
using System.Windows;
using System.Windows.Threading;

namespace Day15
{
    public partial class NotificationWindow : Window
    {
        public NotificationWindow(string message)
        {
            InitializeComponent();
            MessageTextBlock.Text = message;

            var desktopWorkingArea = SystemParameters.WorkArea;
            Left = desktopWorkingArea.Right - Width - 10;
            Top = desktopWorkingArea.Bottom - Height - 10;

            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += (s, e) =>
            {
                timer.Stop();
                Close();
            };
            timer.Start();
        }
    }
}