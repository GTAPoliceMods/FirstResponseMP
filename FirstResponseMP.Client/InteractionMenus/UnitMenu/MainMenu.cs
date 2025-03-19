using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CitizenFX.Core;
using CitizenFX.Core.Native;

using FirstResponseMP.Client.Functions;
using FirstResponseMP.Client.InteractionMenus.Custom;
using FirstResponseMP.Shared.Enums;

using ScaleformUI;
using ScaleformUI.Elements;
using ScaleformUI.Menu;

namespace FirstResponseMP.Client.InteractionMenus.UnitMenu
{
    public class MainMenu : MenuBaseFRMP
    {
        public override UIMenu menu { get; set; }

        public MainMenu()
        {
            menu = new MenuHead("Main Menu", frmp_txd, frmp_txn_banner).Init();

            frmp_detail.Txn = UnitFunctions.PlayerUnit.Division == "Police" ? frmp_txn_leo : UnitFunctions.PlayerUnit.Division == "Medical" ? frmp_txn_ems : UnitFunctions.PlayerUnit.Division == "Fire" ? frmp_txn_fire : "";

            UIMenuDetailsWindow currentStats = new UIMenuDetailsWindow(
                $"{UnitFunctions.PlayerUnit.Rank} {UnitFunctions.PlayerUnit.Name}", 
                $"Current Status: {(UnitFunctions.PlayerUnit.Status == "On Duty" ? "~g~On Duty~s~" : "~r~Off Duty~s~")}", 
                $"Current Division: {(UnitFunctions.PlayerUnit.Division == "Police" ? "~b~Police~s~" : UnitFunctions.PlayerUnit.Division == "Medical" ? "~g~Medical~s~" : UnitFunctions.PlayerUnit.Division == "Fire" ? "~r~Fire~s~" : "")}",
                frmp_detail
            );

            UIMenuItem changeDutyStatus = new UIMenuListItem("Change Status", UnitDutyStatus.Values, UnitDutyStatus.Values.IndexOf(UnitFunctions.PlayerUnit.Status));

            UIMenuItem changeDivision = new UIMenuListItem("Change Division", UnitDivision.Values, UnitDivision.Values.IndexOf(UnitFunctions.PlayerUnit.Division));

            UIMenuItem changeRankOrName = new UIMenuItem("Change Rank/Name", "Change your unit Rank or Name");
            changeRankOrName.SetRightLabel(">>>");

            UIMenuItem versionText = new UIMenuItem("~r~FR:MP Version~c~", $"FirstResponseMP Version");
            versionText.SetRightLabel($"~b~v{API.GetResourceMetadata(API.GetCurrentResourceName(), "version", 0)}~c~");

            menu.AddWindow(currentStats);
            menu.AddItem(changeDutyStatus);
            menu.AddItem(changeDivision);
            menu.AddItem(changeRankOrName);
            menu.AddItem(versionText);

            changeRankOrName.Activated += (sender, i) =>
            {
                sender.SwitchTo(Initial.Menus.UnitChangeRankOrNameMenu.Menu(), inheritOldMenuParams: true);
            };

            menu.OnListSelect += (sender, item, itemIndex) =>
            {
                if (item == changeDutyStatus)
                {
                    var status = UnitDutyStatus.Values[itemIndex];
                    UnitFunctions.SetPlayerUnitStatus(status);
                    UnitFunctions.UpdatePlayerUnitObject();

                    MenuFunctions.RestartUnitMenu(sender);
                }
                else if (item == changeDivision)
                {
                    int oldIndex = itemIndex;

                    var division = UnitDivision.Values[itemIndex];
                    UnitFunctions.SetPlayerUnitDivision(division);
                    UnitFunctions.UpdatePlayerUnitObject();

                    MenuFunctions.RestartUnitMenu(sender);
                }
            };
        }

        public override UIMenu Menu()
        {
            return menu;
        }
    }
}
