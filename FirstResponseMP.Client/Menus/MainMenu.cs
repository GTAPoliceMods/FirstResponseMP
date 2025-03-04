using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CitizenFX.Core;
using CitizenFX.Core.Native;

using FirstResponseMP.Client.MenuItems;
using FirstResponseMP.Shared.Enums;

using ScaleformUI.Elements;
using ScaleformUI.Menu;

namespace FirstResponseMP.Client.Menus
{
    public class MainMenu : MenuBase
    {
        internal static UIMenu mainMenu;

        public MainMenu(string _UnitName, string _UnitStatus, string _UnitDivision)
        {
            mainMenu = new MenuHead("~b~Main Menu~s~", frmp_txds, frmp_txns).Init();

            UIMenuDetailsWindow currentStats = new UIMenuDetailsWindow($"{_UnitName}", $"Current Status: {(_UnitStatus == "On Duty" ? "~g~On Duty~s~" : "~r~Off Duty~s~")}\nCurrent Division: {(_UnitDivision == "Law Enforcement" ? "~b~Law Enforcement~s~" : _UnitDivision == "Medical Service" ? "~g~Medical Service~s~" : _UnitDivision == "Fire & Emergency" ? "~r~Fire & Emergency~s~" : "")}", "");

            UIMenuSeparatorItem currentDivision = new UIMenuSeparatorItem("Current Division:", true);
            currentDivision.SetRightLabel("~b~Law Enforcement~s~");
            currentDivision.Enabled = false;

            UIMenuItem changeDutyStatus = new UIMenuListItem("Change Status", UnitDutyStatus.Values, UnitDutyStatus.Values.IndexOf(_UnitStatus));
            changeDutyStatus.SetRightLabel(">>>");

            UIMenuItem changeDivision = new UIMenuListItem("Change Division", UnitDivision.Values, UnitDivision.Values.IndexOf(_UnitDivision));
            changeDivision.SetRightLabel(">>>");

            mainMenu.AddWindow(currentStats);
            mainMenu.AddItem(changeDutyStatus);
            mainMenu.AddItem(changeDivision);
        }

        public static UIMenu Menu()
        {
            return mainMenu;
        }
    }
}
