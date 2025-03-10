using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using CitizenFX.Core;
using CitizenFX.Core.Native;

using FirstResponseMP.Shared.Constants;
using FirstResponseMP.Shared.Objects;

using Newtonsoft.Json;

using RandomNameGeneratorLibrary;

namespace FirstResponseMP.Client.Functions
{
    public class UnitFunctions : BaseScript
    {
        public static PlayerUnit PlayerUnit;

        public static readonly string CustomNamesFileUrl = "https://frmp-ui.gtapolicemods.com/data/customnames.json";

        public static void UpdatePlayerUnitObject()
        {
            PlayerUnit = new PlayerUnit()
            {
                Rank = GetPlayerUnitRank(),
                Name = GetPlayerUnitName(),
                Status = GetPlayerUnitStatus(),
                Division = GetPlayerUnitDivision()
            };
        }

        public static string GetPlayerUnitRankAndName()
        {
            return $"{GetPlayerUnitRank()} {GetPlayerUnitName()}";
        }

        public static string GetPlayerUnitRank()
        {
            if (API.GetResourceKvpString(KvpStrings.KVP_PlayerUnitRank) == null)
            {
                SetPlayerUnitRank("Deputy");
            }

            return API.GetResourceKvpString(KvpStrings.KVP_PlayerUnitRank);
        }

        public static string GetPlayerUnitName()
        {
            if (API.GetResourceKvpString(KvpStrings.KVP_PlayerUnitName) == null)
            {
                SetPlayerUnitName();
            }

            return API.GetResourceKvpString(KvpStrings.KVP_PlayerUnitName);
        }

        public static async Task<string> GetRandomCustomName()
        {
            var randomName = new Random(DateTime.Now.Second);

            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage customNamesRes = await httpClient.GetAsync(CustomNamesFileUrl);

                await Task.Delay(100);

                if (customNamesRes.IsSuccessStatusCode)
                {
                    var customNamesStr = await customNamesRes.Content.ReadAsStringAsync();
                    var customNames = JsonConvert.DeserializeObject<List<string>>(customNamesStr);

                    int randomNameIndex = randomName.Next(customNames.Count);

                    return $"{customNames[randomNameIndex]}";
                }
                else
                {
                    return GetRandomNormalName();
                }
            }
        }

        public static string GetRandomNormalName()
        {
            var randomName = new Random(DateTime.Now.Second);

            var firstInitial = randomName.GenerateRandomFirstName()[0];
            var lastName = randomName.GenerateRandomLastName();

            return $"{firstInitial}. {lastName}";
        }

        public static void SetPlayerUnitRank(string _playerUnitRank)
        {
            API.SetResourceKvp(KvpStrings.KVP_PlayerUnitRank, $"{_playerUnitRank}");
        }

        public static async void SetPlayerUnitName(string _playerUnitName = null)
        {
            if (_playerUnitName == null)
            {
                var randomChance = new Random();

                await Task.Delay(100);

                if (randomChance.Next(1, 501) == 1)
                {
                    var randomName = await GetRandomCustomName();

                    _playerUnitName = $"{randomName}";
                }
                else
                {
                    var randomName = GetRandomNormalName();

                    _playerUnitName = $"{randomName}";
                }
            }

            API.SetResourceKvp(KvpStrings.KVP_PlayerUnitName, $"{_playerUnitName}");
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

        public static string GetPlayerUnitStatus()
        {
            if (API.GetResourceKvpString(KvpStrings.KVP_PlayerUnitStatus) == null)
            {
                SetPlayerUnitDivision("Off Duty");
            }

            return API.GetResourceKvpString(KvpStrings.KVP_PlayerUnitStatus);
        }

        public static void SetPlayerUnitStatus(string _playerUnitStatus)
        {
            API.SetResourceKvp(KvpStrings.KVP_PlayerUnitStatus, _playerUnitStatus);
        }
    }
}
