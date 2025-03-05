using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CitizenFX.Core;
using CitizenFX.Core.Native;

using FirstResponseMP.Shared.Constants;
using FirstResponseMP.Shared.Objects;

namespace FirstResponseMP.Client.Functions
{
    public class UnitFunctions : BaseScript
    {
        public static PlayerUnit PlayerUnit;

        public static string GetPlayerUnitName()
        {
            if (API.GetResourceKvpString(KvpStrings.KVP_PlayerUnitName) == null)
            {
                SetPlayerUnitName("Officer", "McChickenBorgor");
            }

            return API.GetResourceKvpString(KvpStrings.KVP_PlayerUnitName);
        }

        public static void SetPlayerUnitName(string _playerUnitRank, string _playerUnitName)
        {
            API.SetResourceKvp(KvpStrings.KVP_PlayerUnitName, $"{_playerUnitRank} {_playerUnitName}");
        }

        public static string GetPlayerUnitDivision()
        {
            if (API.GetResourceKvpString(KvpStrings.KVP_PlayerUnitDivision) == null)
            {
                SetPlayerUnitDivision("Police");
            }

            return API.GetResourceKvpString(KvpStrings.KVP_PlayerUnitDivision);
        }

        public static void SetPlayerUnitDivision(string _playerUnitDivision)
        {
            API.SetResourceKvp(KvpStrings.KVP_PlayerUnitDivision, _playerUnitDivision);
        }
    }
}
