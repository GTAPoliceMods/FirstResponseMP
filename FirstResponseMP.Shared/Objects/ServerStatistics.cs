using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstResponseMP.Shared.Objects
{
    public class ServerStatistics
    {
        public class Summary
        {
            public int TotalServers { get; set; }
            public int TotalPlayers { get; set; }
            public int TotalPolice { get; set; }
            public int TotalFire { get; set; }
            public int TotalMedical { get; set; }
        }

        public class ServerStats
        {
            public OnlinePlayers OnlinePlayers { get; set; }
            public string ResourceVersion { get; set; } = string.Empty;
            public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
        }

        public class OnlinePlayers
        {
            public int Total { get; set; }
            public OnDutyUnits OnDuty { get; set; }
        }

        public class OnDutyUnits
        {
            public int PoliceCount { get; set; }
            public int FireCount { get; set; }
            public int MedicalCount { get; set; }
        }
    }
}
