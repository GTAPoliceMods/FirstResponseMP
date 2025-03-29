using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
            MenuFunctions.ToggleMenu(Initial.Menus.UnitMainMenu.Menu());
        });

        public static InputArgument TogglePedInteractionMenu = new Action<int, List<object>, string>((source, args, rawCommand) =>
        {
            if (InteractionFunctions.CurrentPedInteraction == null) return;

            MenuFunctions.ToggleMenu(Initial.Menus.PedMainMenu.Menu());
        });
    }
}
