using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FirstResponseMP.Statistics.Managers
{
    public class ConnectionManager
    {
        private static readonly ConcurrentDictionary<string, WebSocket> _clients = new();
        private static readonly Timer _timer;

        static ConnectionManager()
        {
            _timer = new Timer(async _ => await BroadcastMessageAsync("pingStatsRequest"), null, TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(10));
        }

        public static void AddConnection(string connectionId, WebSocket webSocket)
        {
            _clients[connectionId] = webSocket;
        }

        public static void RemoveConnection(string connectionId)
        {
            if (_clients.ContainsKey(connectionId))
            {
                _clients.Remove(connectionId, out _);
            }
        }

        public static async Task BroadcastMessageAsync(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            var tasks = new List<Task>();

            foreach (var client in _clients.Values)
            {
                if (client.State == WebSocketState.Open)
                {
                    tasks.Add(client.SendAsync(new ArraySegment<byte>(data), WebSocketMessageType.Text, true, CancellationToken.None));
                }
            }

            await Task.WhenAll(tasks);
        }
    }
}
