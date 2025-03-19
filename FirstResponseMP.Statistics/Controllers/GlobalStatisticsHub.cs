using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

namespace FirstResponseMP.Statistics.Controllers
{
    public class GlobalStatisticsHub : ControllerBase
    {
        [Route("/globalStatisticsHub")]
        public async Task Get()
        {
            if (!HttpContext.WebSockets.IsWebSocketRequest)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                await HttpContext.Response.WriteAsync("This endpoint only supports WebSocket requests.");
                HttpContext.Abort();
                return;
            }

            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            await WebSocketRequest(webSocket);
        }

        private static async Task WebSocketRequest(WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];

            while (webSocket.State == WebSocketState.Open)
            {
                var receiveResult = await webSocket.ReceiveAsync(
                    new ArraySegment<byte>(buffer), CancellationToken.None);

                if (receiveResult.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(receiveResult.CloseStatus.Value, receiveResult.CloseStatusDescription, CancellationToken.None);
                    break;
                }

                var message = Encoding.UTF8.GetString(buffer, 0, receiveResult.Count);

                if (message == "getStatistics")
                {
                    var stats = StatisticsStore.GetStatistics();
                    var statsJson = JsonConvert.SerializeObject(stats);
                    var response = Encoding.UTF8.GetBytes(statsJson);

                    await webSocket.SendAsync(new ArraySegment<byte>(response), WebSocketMessageType.Text, true, CancellationToken.None);
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
