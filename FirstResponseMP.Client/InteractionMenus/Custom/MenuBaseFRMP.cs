using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CitizenFX.Core;

using ScaleformUI.Menu;

namespace FirstResponseMP.Client.InteractionMenus.Custom
{
    public abstract class MenuBaseFRMP : ScaleformUI.Menus.MenuBase
    {
        public abstract UIMenu menu { get; set; }

        public readonly static string frmp_txd = "frmp_core";

        public readonly static string frmp_txn_banner = "menu_banner";
        public readonly static string frmp_txn_leo = "leo_icon";
        public readonly static string frmp_txn_fire = "fire_icon";
        public readonly static string frmp_txn_ems = "ems_icon";

        public static UIDetailImage frmp_detail = new UIDetailImage()
        {
            Txd = frmp_txd,
            Txn = "",
            Size = new SizeF(60, 60),
            Pos = new PointF(30, 15),
        };

        public abstract UIMenu Menu();
    }
}
