using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CitizenFX.Core;
using CitizenFX.Core.Native;

using ScaleformUI.Menu;
using ScaleformUI.Radial;
using ScaleformUI.Radio;

namespace FirstResponseMP.Client.Functions
{
    public class CommandInputs : BaseScript
    {
        public static InputArgument ToggleToggleUnitMenu = new Action<int, List<object>, string>((source, args, rawCommand) =>
        {
            if (ScaleformUI.MenuHandler.CurrentMenu != null)
            {
                ScaleformUI.MenuHandler.CurrentMenu.Visible = !ScaleformUI.MenuHandler.CurrentMenu.Visible;
            }
            else
            {
                Initial.Menus.UnitMainMenu.Menu().Visible = true;
            }
        });

        public static InputArgument TogglePedInteractionMenu = new Action<int, List<object>, string>((source, args, rawCommand) =>
        {
            if (ScaleformUI.MenuHandler.CurrentMenu != null)
            {
                ScaleformUI.MenuHandler.CurrentMenu.Visible = !ScaleformUI.MenuHandler.CurrentMenu.Visible;
            }
            else
            {
                Initial.Menus.PedMainMenu.Menu().Visible = true;
            }
        });
    }
}
