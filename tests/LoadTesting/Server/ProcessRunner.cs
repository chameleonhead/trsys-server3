using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace LoadTesting.Server
{
    class ProcessRunner : IDisposable
    {
        private readonly Process _process;
        private bool disposedValue;

        public Action OnShotdown { get; set; }

        public ProcessRunner(string command, string argsuments)
        {
            _process = Process.Start(new ProcessStartInfo()
            {
                WorkingDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),
                FileName = command,
                Arguments = argsuments,
                UseShellExecute = true,
            });
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    OnShotdown?.Invoke();
                    _process.Kill();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}