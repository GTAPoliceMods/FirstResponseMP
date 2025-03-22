using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CitizenFX.Core;
using CitizenFX.Core.Native;

using FirstResponseMP.Client.InteractionMenus.Custom;
using FirstResponseMP.Shared.Objects;

namespace FirstResponseMP.Client.Functions
{
    public class Initial : BaseScript
    {
        public class Player
        {
            public static void LoadInfo()
            {
                var _rank = UnitFunctions.GetPlayerUnitRank();
                var _name = UnitFunctions.GetPlayerUnitName();
                var _division = UnitFunctions.GetPlayerUnitDivision();

                UnitFunctions.PlayerUnit = new PlayerUnit()
                {
                    Rank = _rank,
                    Name = _name,
                    Status = "Off Duty",
                    Division = _division
                };
            }
        }

        public class Menus
        {
            public static MenuBaseFRMP UnitChangeRankOrNameMenu = null;
            public static MenuBaseFRMP UnitMainMenu = null;

            public static RadialMenuBaseFRMP PedMainMenu = null;

            public static void CreateAll()
            {
                // Unit Menu //
                UnitChangeRankOrNameMenu = new InteractionMenus.UnitMenu.ChangeRankOrNameMenu();
                UnitMainMenu = new InteractionMenus.UnitMenu.MainMenu();

                // Ped Radial Menu //
                PedMainMenu = new InteractionMenus.PedInteractionMenu.MainMenu();
            }
        }

        public class Commands
        {
            public static void RegisterAll()
            {
                // MENU INTERACTIONS //
                API.RegisterKeyMapping("frmp_core:ToggleUnitMenu", "Toggle Unit Menu", "keyboard", "F11");
                API.RegisterCommand("frmp_core:ToggleUnitMenu", CommandInputs.ToggleToggleUnitMenu, false);

                API.RegisterKeyMapping("frmp_core:TogglePedInteractionMenu", "Toggle Ped Interaction menu", "keyboard", "N");
                API.RegisterCommand("frmp_core:TogglePedInteractionMenu", CommandInputs.TogglePedInteractionMenu, false);

                // PED INTERACTIONS //
                API.RegisterKeyMapping("frmp_core:InteractWithPed", "Interact with Ped", "keyboard", "E");
                //API.RegisterCommand("frmp_core:InteractWithPed", CommandInputs.InteractWithPed, false);
            }
        }
    }
}
