using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CitizenFX.Core;
using CitizenFX.Core.Native;
using FirstResponseMP.Client.Menus;

namespace FirstResponseMP.Client.Functions
{
    public class CommandInputs : BaseScript
    {
        public static InputArgument ToggleMenuCommand = new Action<int, List<object>, string>((source, args, rawCommand) =>
        {
            if (ScaleformUI.MenuHandler.CurrentMenu != null)
            {
                ScaleformUI.MenuHandler.CurrentMenu.Visible = !ScaleformUI.MenuHandler.CurrentMenu.Visible;
            }
            else
            {
                Initial.Menus.MainMenu.Menu().Visible = true;
            }
        });
    }
}
