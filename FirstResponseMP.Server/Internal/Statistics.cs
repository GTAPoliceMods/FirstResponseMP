using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace FirstResponseMP.Server.Internal
{
    class Statistics : BaseScript
    {
        public Statistics()
        {
            Tick += OnStatisticsTick;
        }

        public async Task OnStatisticsTick()
        {
            await Task.Delay(5000);

            using (var httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage customNamesRes = await httpClient.PostAsync("https://example.com/api/statistics", new StringContent($"{{'serverHost': '{Dns.GetHostName()}', 'onlinePlayers': 32, 'ondutyPlayers': 10}}"));
                }
                catch (Exception e)
                {
                    // DO Nothing Loser
                }
            }
        }
    }
}
