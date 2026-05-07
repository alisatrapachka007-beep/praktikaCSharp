using System;
using System.IO.MemoryMappedFiles;
using System.Text;
using System.Threading;
using System.Windows.Threading;

namespace Day15.Services
{
    public class NotificationService
    {
        private const string NotificationMapName = "MedicalRecordsNotifications";
        private MemoryMappedFile _mmf;
        private MemoryMappedViewAccessor _accessor;
        private Timer _timer;
        private readonly Dispatcher _dispatcher;

        public event Action<string> NotificationReceived;

        public NotificationService(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public void StartListening()
        {
            try
            {
                _mmf = MemoryMappedFile.CreateOrOpen(NotificationMapName, 4096);
                _accessor = _mmf.CreateViewAccessor(0, 4096);
                _timer = new Timer(CheckForNotifications, null, 0, 500);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Notification error: {ex.Message}");
            }
        }

        private void CheckForNotifications(object state)
        {
            try
            {
                byte[] buffer = new byte[1024];
                for (int i = 0; i < buffer.Length; i++)
                {
                    buffer[i] = _accessor.ReadByte(i);
                    if (buffer[i] == 0)
                        break;
                }

                string notification = Encoding.UTF8.GetString(buffer).TrimEnd('\0');
                if (!string.IsNullOrEmpty(notification))
                {
                    for (int i = 0; i < buffer.Length; i++)
                        _accessor.Write(i, (byte)0);

                    _dispatcher.Invoke(() => NotificationReceived?.Invoke(notification));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Check error: {ex.Message}");
            }
        }

        public void SendNotification(string message)
        {
            try
            {
                using (var mmf = MemoryMappedFile.OpenExisting(NotificationMapName))
                using (var accessor = mmf.CreateViewAccessor(0, 4096))
                {
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    for (int i = 0; i < data.Length && i < 1024; i++)
                        accessor.Write(i, data[i]);
                    accessor.Write(data.Length, (byte)0);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Send notification error: {ex.Message}");
            }
        }

        public void StopListening()
        {
            _timer?.Dispose();
            _accessor?.Dispose();
            _mmf?.Dispose();
        }
    }
}