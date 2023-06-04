using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Data
{
    internal class Logger
    {
        ConcurrentQueue<string> _queue;
        public Logger() {
            _queue = new ConcurrentQueue<string>();
            WriteToFile();
        }

        public void addToQueue(IBall ball)
        {
            string jsonString = JsonSerializer.Serialize(ball);
            string date = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff");
            string log = "{" + String.Format("\n\t\"Date\": \"{0}\",\n\t\"Info\":{1}\n", date, jsonString) + "}\n";
            _queue.Enqueue(log);
        }

        private void WriteToFile() {
            Task.Run(async () =>
            {
                using StreamWriter _writer = new StreamWriter("logs.json");
                while (true)
                {
                    if (_queue.Count > 0)
                    {
                        while (!_queue.IsEmpty)
                        {
                            if (_queue.TryDequeue(out string item))
                            {
                                _writer.WriteLine(item);
                            }
                        }
                        await _writer.FlushAsync();
                    }
                    else
                    {
                        await Task.Delay(100);
                    }
                }
            });
        }
    }
}
