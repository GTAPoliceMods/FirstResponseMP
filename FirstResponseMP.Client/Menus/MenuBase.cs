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
        public static string frmp_txd = "frmp_core";

        public static string frmp_txn_banner = "menu_banner";
        public static string frmp_txn_leo = "leo_icon";
        public static string frmp_txn_fire = "fire_icon";
        public static string frmp_txn_ems = "ems_icon";

        public static UIDetailImage frmp_detail_leo = new UIDetailImage()
        {
            Txd = frmp_txd,
            Txn = frmp_txn_leo,
            Size = new SizeF(64, 64)
        };

        public static UIDetailImage frmp_detail_fire = new UIDetailImage()
        {
            Txd = frmp_txd,
            Txn = frmp_txn_fire,
            Size = new SizeF(64, 64)
        };

        public static UIDetailImage frmp_detail_ems = new UIDetailImage()
        {
            Txd = frmp_txd,
            Txn = frmp_txn_ems,
            Size = new SizeF(64, 64)
        };

        public static UIDetailImage frmp_detail_blank = new UIDetailImage()
        {
            Txd = "",
            Txn = "",
        };

        public MenuBase()
        {
            var frmp_rtxd = API.CreateRuntimeTxd(frmp_txd);

            var frmp_dui = API.CreateDui("https://frmp-ui.gtapolicemods.com/menu/banner.png", 288, 130);
            API.CreateRuntimeTextureFromDuiHandle(frmp_rtxd, frmp_txn_banner, API.GetDuiHandle(frmp_dui));

            var frmp_dui_leo = API.CreateDui("https://frmp-ui.gtapolicemods.com/menu/leo_icon.png", 64, 64);
            API.CreateRuntimeTextureFromDuiHandle(frmp_rtxd, frmp_txn_leo, API.GetDuiHandle(frmp_dui_leo));

            var frmp_dui_fire = API.CreateDui("https://frmp-ui.gtapolicemods.com/menu/fire_icon.png", 64, 64);
            API.CreateRuntimeTextureFromDuiHandle(frmp_rtxd, frmp_txn_fire, API.GetDuiHandle(frmp_dui_fire));

            var frmp_dui_ems = API.CreateDui("https://frmp-ui.gtapolicemods.com/menu/ems_icon.png", 64, 64);
            API.CreateRuntimeTextureFromDuiHandle(frmp_rtxd, frmp_txn_ems, API.GetDuiHandle(frmp_dui_ems));
        }
    }
}
