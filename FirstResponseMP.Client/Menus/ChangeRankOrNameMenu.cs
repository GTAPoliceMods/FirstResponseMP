using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FirstResponseMP.Client.Functions;
using FirstResponseMP.Client.MenuItems;
using FirstResponseMP.Client.Menus.Custom;
using FirstResponseMP.Shared.Enums;

using ScaleformUI;
using ScaleformUI.Menu;

namespace FirstResponseMP.Client.Menus
{
    public class ChangeRankOrNameMenu : MenuBase
    {
        public override UIMenu menu { get; set; }

        public ChangeRankOrNameMenu()
        {
            menu = new MenuHead("Change Rank/Name", frmp_txd, frmp_txn_banner).Init();

            frmp_detail.Txn = UnitFunctions.PlayerUnit.Division == "Police" ? frmp_txn_leo : UnitFunctions.PlayerUnit.Division == "Medical" ? frmp_txn_ems : UnitFunctions.PlayerUnit.Division == "Fire" ? frmp_txn_fire : "";

            UIMenuDetailsWindow currentStats = new UIMenuDetailsWindow(
                $"{UnitFunctions.PlayerUnit.Rank} {UnitFunctions.PlayerUnit.Name}",
                $"Current Status: {(UnitFunctions.PlayerUnit.Status == "On Duty" ? "~g~On Duty~s~" : "~r~Off Duty~s~")}",
                $"Current Division: {(UnitFunctions.PlayerUnit.Division == "Police" ? "~b~Police~s~" : UnitFunctions.PlayerUnit.Division == "Medical" ? "~g~Medical~s~" : UnitFunctions.PlayerUnit.Division == "Fire" ? "~r~Fire~s~" : "")}",
                frmp_detail
            );

            UIMenuItem changeUnitRank = new UIMenuListItem("Change Rank", UnitRank.Values, UnitRank.Values.IndexOf(UnitFunctions.PlayerUnit.Rank));

            List<dynamic> ChangeNameValues = new List<dynamic>
            {
                "Custom",
                "Random"
            };

            UIMenuItem changeUnitName = new UIMenuListItem("Change Name", ChangeNameValues, 0);

            menu.AddWindow(currentStats);
            menu.AddItem(changeUnitRank);
            menu.AddItem(changeUnitName);

            menu.OnListSelect += (sender, item, itemIndex) =>
            {
                if (item == changeUnitRank)
                {
                    var rank = UnitRank.Values[itemIndex];
                    UnitFunctions.SetPlayerUnitRank(rank);
                    UnitFunctions.UpdatePlayerUnitObject();

                    MenuFunctions.RestartMenu(sender);
                }
                else if (item == changeUnitName)
                {
                    var type = ChangeNameValues[itemIndex];

                    if (type == "Custom")
                    {
                        SetCustomUnitName(sender);
                    }
                    else
                    {
                        UnitFunctions.SetPlayerUnitName();
                        UnitFunctions.UpdatePlayerUnitObject();

                        MenuFunctions.RestartMenu(sender);
                    }
                }
            };
        }

        public static async void SetCustomUnitName(UIMenu sender)
        {
            var customName = await OverlayFunctions.GetUserInput(windowTitle: "Enter Custom Name");

            if (!string.IsNullOrEmpty(customName))
            {
                UnitFunctions.SetPlayerUnitName(customName);
                UnitFunctions.UpdatePlayerUnitObject();

                MenuFunctions.RestartMenu(sender);
            }
            else
            {
                Notifications.ShowNotification("Name is invalid and has not been updated.", NotificationColor.Yellow);
            }
        }

        public override UIMenu Menu()
        {
            return menu;
        }
    }
}
