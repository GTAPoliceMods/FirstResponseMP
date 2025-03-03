using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CitizenFX.Core;
using CitizenFX.Core.Native;

using ScaleformUI.Elements;
using ScaleformUI.Menu;

namespace FirstResponseMP.Client.Menus
{
    public class MainMenu : MenuBase
    {
        public static UIMenu mainMenu;

        public MainMenu()
        {
            mainMenu = new UIMenu(" ", "~b~Main Menu~s~", new PointF(376, 50), frmp_txds, frmp_txns, true, true, MenuAlignment.RIGHT);

            CreateMenu(mainMenu);
        }

        public override void CreateMenu(UIMenu menu)
        {
            UIMenuItem currentDutyStatus = new UIMenuItem("Current Status:", "Your current unit status.");
            currentDutyStatus.SetRightLabel("~g~On Duty~s~");
            currentDutyStatus.Enabled = false;

            UIMenuItem currentDivision = new UIMenuItem("Current Division:", "Your current unit division.");
            currentDivision.SetRightLabel("~b~Law Enforcement~s~");
            currentDivision.Enabled = false;

            UIMenuItem changeDutyStatus = new UIMenuItem("Change Status", "Change your current unit statuc.");
            changeDutyStatus.SetRightLabel(">>>");

            UIMenuItem changeDivision = new UIMenuItem("Change Division", "Change your current unit division.");
            changeDivision.SetRightLabel(">>>");

            menu.AddItem(currentDutyStatus);
            menu.AddItem(currentDivision);
            menu.AddItem(changeDutyStatus);
            menu.AddItem(changeDivision);
        }
    }
}
