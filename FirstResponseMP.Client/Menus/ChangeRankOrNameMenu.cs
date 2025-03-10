using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FirstResponseMP.Client.Functions;
using FirstResponseMP.Client.MenuItems;
using FirstResponseMP.Shared.Enums;

using ScaleformUI;
using ScaleformUI.Menu;

namespace FirstResponseMP.Client.Menus
{
    public class ChangeRankOrNameMenu : MenuBase
    {
        public MenuFunctions MenuFunctions = new MenuFunctions();

        private static UIMenu menu;

        public ChangeRankOrNameMenu()
        {
            menu = new MenuHead("~b~Change Rank/Name~s~", frmp_txd, frmp_txn_banner).Init();

            frmp_detail.Txn = UnitFunctions.PlayerUnit.Division == "Police" ? frmp_txn_leo : UnitFunctions.PlayerUnit.Division == "Medical" ? frmp_txn_ems : UnitFunctions.PlayerUnit.Division == "Fire" ? frmp_txn_fire : "";

            UIMenuDetailsWindow currentStats = new UIMenuDetailsWindow(
                $"{UnitFunctions.PlayerUnit.Rank} {UnitFunctions.PlayerUnit.Name}",
                $"Current Status: {(UnitFunctions.PlayerUnit.Status == "On Duty" ? "~g~On Duty~s~" : "~r~Off Duty~s~")}",
                $"Current Division: {(UnitFunctions.PlayerUnit.Division == "Police" ? "~b~Police~s~" : UnitFunctions.PlayerUnit.Division == "Medical" ? "~g~Medical~s~" : UnitFunctions.PlayerUnit.Division == "Fire" ? "~r~Fire~s~" : "")}",
                frmp_detail
            );

            UIMenuItem changeUnitRank = new UIMenuListItem("Change Rank", UnitRank.Values, UnitRank.Values.IndexOf(UnitFunctions.PlayerUnit.Rank));

            UIMenuItem setCustomUnitName = new UIMenuItem("Set Custom Name", "Set your unit name (Custom)");
            UIMenuItem generateRandomUnitName = new UIMenuItem("Generate Random Name", "Set your unit name (Random)");

            menu.AddWindow(currentStats);
            menu.AddItem(changeUnitRank);
            menu.AddItem(setCustomUnitName);
            menu.AddItem(generateRandomUnitName);

            setCustomUnitName.Activated += (sender, i) =>
            {
                SetCustomUnitName();
            };

            generateRandomUnitName.Activated += (sender, i) =>
            {
                UnitFunctions.SetPlayerUnitName();
                UnitFunctions.UpdatePlayerUnitObject();

                MenuFunctions.RestartMenu(Menu(), false);
            };

            menu.OnListSelect += (sender, item, itemIndex) =>
            {
                if (item == changeUnitRank)
                {
                    var rank = UnitRank.Values[itemIndex];
                    UnitFunctions.SetPlayerUnitRank(rank);
                    UnitFunctions.UpdatePlayerUnitObject();

                    MenuFunctions.RestartMenu(Menu(), false);
                }
            };
        }

        public async void SetCustomUnitName()
        {
            var customName = await OverlayFunctions.GetUserInput(windowTitle: "Enter Custom Name");

            if (!string.IsNullOrEmpty(customName))
            {
                UnitFunctions.SetPlayerUnitName(customName);
                UnitFunctions.UpdatePlayerUnitObject();

                MenuFunctions.RestartMenu(Menu(), false);
            }
            else
            {
                Notifications.ShowNotification("Name is invalid and has not been updated.", NotificationColor.Yellow);
            }
        }

        public static UIMenu Menu()
        {
            return menu;
        }
    }
}
