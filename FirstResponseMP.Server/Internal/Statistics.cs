using System;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using WebSocket4Net;
using System.Security.Authentication;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using CitizenFX.Core;
using CitizenFX.Core.Native;

using static CitizenFX.Core.Native.API;
using static FirstResponseMP.Shared.Objects.ServerStatistics;
using System.Collections.Generic;

namespace FirstResponseMP.Server.Internal
{
    class Statistics : BaseScript
    {
        private static WebSocket connection;

        public Statistics()
        {
            var buffer = new byte[1024 * 4];

            connection = new WebSocket(uri: "wss://frmpstats.gtapolicemods.com/serverStatisticsHub", customHeaderItems: new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("Authorization", "McLeakingItHasGoneOofBecauseHeMadAtMeSadface")
            }, userAgent: "FirstResponseMP:InternalStatsServ", version: WebSocketVersion.Rfc6455);

            ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            connection.Security.EnabledSslProtocols = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12;
            connection.Security.AllowUnstrustedCertificate = true;
            connection.Security.AllowNameMismatchCertificate = true;

            StartServer();

            async void StartServer()
            {
                await Task.Delay(0);

                try
                {
                    connection.Open();
                    Debug.WriteLine("Stats WebSocket connection established.");

                    StartRecieving();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error during stats websocket connection: {ex.Message}");
                }
            }

            void StartRecieving()
            {
                connection.MessageReceived += async (sender, e) =>
                {
                    var message = e.Message;

                    if (message == "pingStatsRequest")
                    {
                        await OnStatisticsRequest();
                    }
                };

                connection.Closed += (sender, e) =>
                {
                    Debug.WriteLine("Stats WebSocket connection closed.");
                };
            }
        }

        public async Task OnStatisticsRequest()
        {
            await Task.Delay(0);

            Debug.WriteLine("Statistics requested.");

            try
            {
                ServerStats stats = new ServerStats
                {
                    //ResourceVersion = GetResourceMetadata(GetCurrentResourceName(), "version", 0),
                    ResourceVersion = "1.0.0",
                    OnlinePlayers = new OnlinePlayers
                    {
                        Total = Players.ToList().Count,
                        OnDuty = new OnDutyUnits
                        {
                            PoliceCount = 10,
                            FireCount = 50,
                            MedicalCount = 69
                        }
                    }
                };

                Debug.WriteLine("Statistics object created.");

                var statsJson = Newtonsoft.Json.JsonConvert.SerializeObject(stats);

                Debug.WriteLine("Statistics object stringified.");

                connection.Send($"sendServerStats({statsJson});");
                Debug.WriteLine("Statistics sent.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Something happened while sending Statistics.\n{ex.Message}");
            }
        }
    }
}
