using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CitizenFX.Core;
using CitizenFX.Core.Native;

using ScaleformUI.Menu;

namespace FirstResponseMP.Client.Menus
{
    public class MenuBase : BaseScript
    {
        public static string frmp_txds = "frmp_core";
        public static string frmp_txns = "banner";

        public MenuBase()
        {
            var frmp_txd = API.CreateRuntimeTxd(frmp_txds);
            var frmp_dui = API.CreateDui("https://gtapolicemods.com/uploads/pages_media/menu_banner.png", 288, 130);
            API.CreateRuntimeTextureFromDuiHandle(frmp_txd, frmp_txns, API.GetDuiHandle(frmp_dui));
        }
    }
}
