using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CitizenFX.Core;
using CitizenFX.Core.Native;

using FirstResponseMP.Client.Menus;

using ScaleformUI.Menu;

namespace FirstResponseMP.Client
{
    public class Main : BaseScript
    {
        public Main()
        {
            Debug.WriteLine("FirstResponseMP.Client loaded.");

            API.RegisterKeyMapping("frmp_core:ToggleMenu", "Toggle FRMP Menu", "keyboard", "F6");

            API.RegisterCommand("frmp_core:ToggleMenu", ToggleMenuCommand, false);
        }

        private InputArgument ToggleMenuCommand = new Action<int, List<object>, string>((source, args, rawCommand) =>
        {
            if (ScaleformUI.MenuHandler.CurrentMenu != null)
            {
                ScaleformUI.MenuHandler.CurrentMenu.Visible ^= ScaleformUI.MenuHandler.CurrentMenu.Visible;
            }
            else
            {
                MainMenu.mainMenu.Visible = true;
            }
        });
    }
}
