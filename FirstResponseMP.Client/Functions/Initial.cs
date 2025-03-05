using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CitizenFX.Core;
using CitizenFX.Core.Native;

using FirstResponseMP.Client.Menus;
using FirstResponseMP.Shared.Objects;

namespace FirstResponseMP.Client.Functions
{
    public class Initial : BaseScript
    {
        public class Player
        {
            public static void LoadInfo()
            {
                var _name = UnitFunctions.GetPlayerUnitName();
                var _division = UnitFunctions.GetPlayerUnitDivision();

                UnitFunctions.PlayerUnit = new PlayerUnit()
                {
                    Name = _name,
                    Status = "Off Duty",
                    Division = _division
                };
            }
        }

        public class Menus
        {
            public static void CreateAll()
            {
                _ = new MainMenu();
            }
        }

        public class Commands
        {
            public static void RegisterAll()
            {
                API.RegisterKeyMapping("frmp_core:ToggleMenu", "Toggle FRMP Menu", "keyboard", "F6");
                API.RegisterCommand("frmp_core:ToggleMenu", CommandInputs.ToggleMenuCommand, false);
            }
        }
    }
}
