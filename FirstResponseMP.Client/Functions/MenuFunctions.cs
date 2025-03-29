using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CitizenFX.Core;

using ScaleformUI;
using ScaleformUI.Menu;
using ScaleformUI.Menus;

namespace FirstResponseMP.Client.Functions
{
    public class MenuFunctions : BaseScript
    {
        public static void ToggleMenu(MenuBase menu)
        {
            if (MenuHandler.CurrentMenu != null)
            {
                MenuHandler.CurrentMenu.Visible = !MenuHandler.CurrentMenu.Visible;
            }
            else
            {
                menu.Visible = true;
            }
        }

        public static void RestartUnitMenu(UIMenu currentMenu)
        {
            MenuHandler.CurrentMenu.Visible = false;
            MenuHandler.CloseAndClearHistory();
            Initial.Menus.CreateAll();

            Initial.Menus.UnitMainMenu.Menu().Visible = true;

            if (!currentMenu.Subtitle.Contains("Main Menu"))
            {
                Initial.Menus.UnitMainMenu.Menu().SwitchTo(Initial.Menus.UnitChangeRankOrNameMenu.Menu(), currentMenu.CurrentSelection, false, null);
            }
            else
            {
                Initial.Menus.UnitMainMenu.Menu().CurrentSelection = currentMenu.CurrentSelection;
            }
        }
    }
}
