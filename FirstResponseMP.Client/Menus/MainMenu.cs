using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CitizenFX.Core;
using CitizenFX.Core.Native;

using FirstResponseMP.Client.Functions;
using FirstResponseMP.Client.MenuItems;
using FirstResponseMP.Shared.Enums;

using ScaleformUI.Elements;
using ScaleformUI.Menu;

namespace FirstResponseMP.Client.Menus
{
    public class MainMenu : MenuBase
    {
        internal static UIMenu mainMenu;

        public MainMenu()
        {
            mainMenu = new MenuHead("~b~Main Menu~s~", frmp_txd, frmp_txn_banner).Init();

            frmp_detail.Txn = UnitFunctions.PlayerUnit.Division == "Police" ? frmp_txn_leo : UnitFunctions.PlayerUnit.Division == "Medical" ? frmp_txn_ems : UnitFunctions.PlayerUnit.Division == "Fire" ? frmp_txn_fire : "";

            UIMenuDetailsWindow currentStats = new UIMenuDetailsWindow(
                $"{UnitFunctions.PlayerUnit.Name}", 
                $"Current Status: {(UnitFunctions.PlayerUnit.Status == "On Duty" ? "~g~On Duty~s~" : "~r~Off Duty~s~")}", 
                $"Current Division: {(UnitFunctions.PlayerUnit.Division == "Police" ? "~b~Police~s~" : UnitFunctions.PlayerUnit.Division == "Medical" ? "~g~Medical~s~" : UnitFunctions.PlayerUnit.Division == "Fire" ? "~r~Fire~s~" : "")}",
                frmp_detail
            );

            UIMenuItem changeDutyStatus = new UIMenuListItem("Change Status", UnitDutyStatus.Values, UnitDutyStatus.Values.IndexOf(UnitFunctions.PlayerUnit.Status));

            UIMenuItem changeDivision = new UIMenuListItem("Change Division", UnitDivision.Values, UnitDivision.Values.IndexOf(UnitFunctions.PlayerUnit.Division));

            mainMenu.AddWindow(currentStats);
            mainMenu.AddItem(changeDutyStatus);
            mainMenu.AddItem(changeDivision);

            mainMenu.OnListSelect += (sender, item, itemIndex) =>
            {
                if (item == changeDutyStatus)
                {
                    var status = UnitDutyStatus.Values[itemIndex];
                    UnitFunctions.PlayerUnit.Status = status;
                    mainMenu.Visible = false;
                    mainMenu.Clear();
                    _ = new MainMenu();
                    mainMenu.Visible = true;
                    mainMenu.CurrentItem = item;
                }
                else if (item == changeDivision)
                {
                    int oldIndex = itemIndex;

                    var division = UnitDivision.Values[itemIndex];
                    UnitFunctions.SetPlayerUnitDivision(division);
                    UnitFunctions.PlayerUnit.Division = division;
                    mainMenu.Visible = false;
                    mainMenu.Clear();
                    _ = new MainMenu();
                    mainMenu.Visible = true;
                    mainMenu.CurrentItem = item;
                }
            };
        }

        public static UIMenu Menu()
        {
            return mainMenu;
        }
    }
}
