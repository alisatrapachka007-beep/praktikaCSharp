using System;
using System.IO.Pipes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Day15.Services
{
    public class NamedPipeService
    {
        private const string PipeName = "MedicalRecordsChat";
        private NamedPipeServerStream _serverStream;
        private readonly Dispatcher _dispatcher;

        public event Action<string> MessageReceived;

        public NamedPipeService(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public async Task StartServerAsync()
        {
            await Task.Run(async () =>
            {
                try
                {
                    _serverStream = new NamedPipeServerStream(PipeName, PipeDirection.InOut, 10);
                    _serverStream.WaitForConnection();

                    byte[] buffer = new byte[4096];
                    while (_serverStream.IsConnected)
                    {
                        int bytesRead = await _serverStream.ReadAsync(buffer, 0, buffer.Length);
                        if (bytesRead > 0)
                        {
                            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                            _dispatcher.Invoke(() => MessageReceived?.Invoke(message));
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Server error: {ex.Message}");
                }
            });
        }

        public async Task SendMessageAsync(string message)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var client = new NamedPipeClientStream(".", PipeName, PipeDirection.Out))
                    {
                        client.Connect(1000);
                        byte[] data = Encoding.UTF8.GetBytes(message);
                        client.Write(data, 0, data.Length);
                        client.Flush();
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Send error: {ex.Message}");
                }
            });
        }

        public void StopServer()
        {
            _serverStream?.Dispose();
        }
    }
}