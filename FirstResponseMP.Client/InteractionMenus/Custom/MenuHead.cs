using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ScaleformUI.Menu;

namespace FirstResponseMP.Client.InteractionMenus.Custom
{
    public class MenuHead
    {
        private static string MenuDescription;
        private static string MenuBannerTxd;
        private static string MenuBannerTxn;

        public MenuHead(string _MenuDescription, string _MenuBannerTxd, string _MenuBannerTxn)
        {
            MenuDescription = _MenuDescription;
            MenuBannerTxd = _MenuBannerTxd;
            MenuBannerTxn = _MenuBannerTxn;
        }

        public UIMenu Init()
        {
            var menu = new UIMenu("", $"~b~{MenuDescription}~s~", GetMenuOffset(), MenuBannerTxd, MenuBannerTxn, true, true);

            menu.SetMouse(false, false, false, false, true);

            return menu;
        }

        internal PointF GetMenuOffset()
        {
            if (Main.MenuAlign == Shared.Enums.MenuAlign.Left)
            {
                return new PointF(20, 20);
            }
            else
            {
                return new PointF(970, 20);
            }
        }
    }
}
