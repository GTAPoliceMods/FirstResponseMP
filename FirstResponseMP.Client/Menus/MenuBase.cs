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
    public class MenuBase
    {
        public static string frmp_txd = "frmp_core";

        public static string frmp_txn_banner = "menu_banner";
        public static string frmp_txn_leo = "leo_icon";
        public static string frmp_txn_fire = "fire_icon";
        public static string frmp_txn_ems = "ems_icon";

        public static UIDetailImage frmp_detail = new UIDetailImage()
        {
            Txd = frmp_txd,
            Txn = "",
            Size = new SizeF(60, 60),
            Pos = new PointF(30, 60),
        };
    }
}
