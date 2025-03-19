using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using DSharpPlus.Entities;

using FirstResponseMP.Shared.Objects;

using static FirstResponseMP.Shared.Objects.ServerStatistics;

namespace FirstResponseMP.Statistics
{
    public class StatisticsStore
    {
        public static readonly ConcurrentDictionary<string, ServerStats> OnlineServersList = [];
        private static readonly Timer _timer;

        static StatisticsStore()
        {
            _timer = new Timer(_ => RemoveStaleClients(), null, TimeSpan.FromSeconds(0), TimeSpan.FromMinutes(1));
        }

        public static void UpdateStats(string connectionId, ServerStats stats)
        {
            stats.LastUpdated = DateTime.UtcNow;
            OnlineServersList[connectionId] = stats;
        }

        public static void RemoveStats(string connectionId)
        {
            if (OnlineServersList.ContainsKey(connectionId))
            {
                OnlineServersList.Remove(connectionId, out _);
            }
        }

        public static Summary GetStatistics()
        {
            return new Summary
            {
                TotalServers = OnlineServersList.Count,
                TotalPlayers = OnlineServersList.Values.Sum(s => s.OnlinePlayers.Total),
                TotalPolice = OnlineServersList.Values.Sum(s => s.OnlinePlayers.OnDuty.PoliceCount),
                TotalFire = OnlineServersList.Values.Sum(s => s.OnlinePlayers.OnDuty.FireCount),
                TotalMedical = OnlineServersList.Values.Sum(s => s.OnlinePlayers.OnDuty.MedicalCount)
            };
        }

        public static void RemoveStaleClients()
        {
            var now = DateTime.UtcNow;
            foreach (var key in OnlineServersList.Keys.ToList())
            {
                if ((now - OnlineServersList[key].LastUpdated).TotalSeconds > 10)
                {
                    OnlineServersList.Remove(key, out _);
                }
            }
        }
    }
}
