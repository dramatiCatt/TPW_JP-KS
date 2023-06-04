using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Data
{
    internal class Logger
    {
        public Logger() {
            WriteToFile();
        }

        string fileName = "logs.json";
        private object _lockFile = new object();
        CancellationToken token;
        private ObservableCollection<Ball> _balls = new ObservableCollection<Ball>();
        private void WriteToFile() {
            Task.Run(async () =>
            {
                System.IO.File.WriteAllText(fileName, string.Empty);
                while (true)
                {
                    var options = new JsonSerializerOptions { WriteIndented = true };
                    string jsonString = JsonSerializer.Serialize(_balls, options);
                    string jsonString2 = "[ \"Date/Time\": \"" + DateTime.Now.ToString() + "\",\n  \"Balls\": " + jsonString + " ]\n";
                    lock (_lockFile)
                    {
                        File.AppendAllText(fileName, jsonString2);
                    }
                    try { token.ThrowIfCancellationRequested(); }
                    catch (System.OperationCanceledException) { break; }
                    await Task.Delay(2000);
                }
            });
        }
    }
}
