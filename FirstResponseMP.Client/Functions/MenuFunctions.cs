using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CitizenFX.Core;

using ScaleformUI;
using ScaleformUI.Menu;

namespace FirstResponseMP.Client.Functions
{
    public class MenuFunctions : BaseScript
    {
        public static void RestartMenu(UIMenu currentMenu)
        {
            MenuHandler.CurrentMenu.Visible = false;
            MenuHandler.CloseAndClearHistory();
            Initial.Menus.CreateAll();

            Initial.Menus.MainMenu.Menu().Visible = true;

            if (!currentMenu.Subtitle.Contains("Main Menu"))
            {
                Initial.Menus.MainMenu.Menu().SwitchTo(Initial.Menus.ChangeRankOrNameMenu.Menu(), currentMenu.CurrentSelection, false, null);
            }
            else
            {
                Initial.Menus.MainMenu.Menu().CurrentSelection = currentMenu.CurrentSelection;
            }
        }
    }
}
