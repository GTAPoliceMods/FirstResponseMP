using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using FirstResponseMP.Statistics.Attributes;
using FirstResponseMP.Statistics.Managers;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using static FirstResponseMP.Shared.Objects.ServerStatistics;

namespace FirstResponseMP.Statistics.Controllers
{
    public class ServerStatisticsHub : ControllerBase
    {
        [Route("/serverStatisticsHub")]
        [Auth]
        public async Task Get()
        {
            if (!HttpContext.Request.Headers.ContainsKey("Authorization") || string.IsNullOrWhiteSpace(HttpContext.Request.Headers["Authorization"]))
            {
                HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await HttpContext.Response.WriteAsync("Invalid authorization header");
                HttpContext.Abort();
                return;
            }

            if (HttpContext.Request.Headers["Authorization"] != "McLeakingItHasGoneOofBecauseHeMadAtMeSadface")
            {
                HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await HttpContext.Response.WriteAsync("Invalid authorization header");
                HttpContext.Abort();
                return;
            }

            if (!HttpContext.WebSockets.IsWebSocketRequest)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                await HttpContext.Response.WriteAsync("This endpoint only supports WebSocket requests.");
                HttpContext.Abort();
                return;
            }

            string connectionId = Guid.NewGuid().ToString();

            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            ConnectionManager.AddConnection(connectionId, webSocket);
            await WebSocketRequest(connectionId, webSocket);
        }

        private static async Task WebSocketRequest(string ConnectionId, WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];

            while (webSocket.State == WebSocketState.Open)
            {
                var receiveResult = await webSocket.ReceiveAsync(
                    new ArraySegment<byte>(buffer), CancellationToken.None);

                if (receiveResult.MessageType == WebSocketMessageType.Close)
                {
                    ConnectionManager.RemoveConnection(ConnectionId);
                    StatisticsStore.RemoveStats(ConnectionId);
                    await webSocket.CloseAsync(receiveResult.CloseStatus.Value, receiveResult.CloseStatusDescription, CancellationToken.None);
                    return;
                }

                var message = Encoding.UTF8.GetString(buffer, 0, receiveResult.Count);

                if (message.StartsWith("sendServerStats"))
                {
                    var data = message.Replace("sendServerStats(", "").Replace(");", "");

                    try
                    {
                        ServerStats stats = JsonConvert.DeserializeObject<ServerStats>(data);

                        StatisticsStore.UpdateStats(ConnectionId, stats);

                        var response = Encoding.UTF8.GetBytes("Sent statistics!");

                        await webSocket.SendAsync(new ArraySegment<byte>(response), WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                    catch (Exception ex)
                    {
                        var response = Encoding.UTF8.GetBytes($"{{ \"error\": \"Error parsing statistics\" }}");
                        await webSocket.SendAsync(new ArraySegment<byte>(response), WebSocketMessageType.Text, true, CancellationToken.None);
                        continue;
                    }
                }
                else
                {
                    var response = Encoding.UTF8.GetBytes($"{{ \"error\": \"Unknown Message `{message}`\" }}");
                    await webSocket.SendAsync(new ArraySegment<byte>(response), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }
    }
}
