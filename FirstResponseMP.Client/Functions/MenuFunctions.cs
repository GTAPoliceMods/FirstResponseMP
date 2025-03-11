using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CitizenFX.Core;

using FirstResponseMP.Client.Menus;

using ScaleformUI;
using ScaleformUI.Menu;
using ScaleformUI.Menus;

namespace FirstResponseMP.Client.Functions
{
    public class MenuFunctions : BaseScript
    {
        public static void RestartMenu()
        {
            if (MenuHandler.CurrentMenu != null && MenuHandler.CurrentMenu.Visible)
            {
                MenuHandler.CurrentMenu.Visible = false;
                MenuHandler.CloseAndClearHistory();
                Initial.Menus.CreateAll();
                MainMenu.Menu().Visible = true;
            }
            else
            {
                MenuHandler.CloseAndClearHistory();
                Initial.Menus.CreateAll();
            }
        }
    }
}
