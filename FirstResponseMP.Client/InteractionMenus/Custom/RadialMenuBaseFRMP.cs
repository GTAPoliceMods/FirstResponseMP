using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CitizenFX.Core;

using ScaleformUI.Menu;
using ScaleformUI.Radial;
using ScaleformUI.Radio;

namespace FirstResponseMP.Client.InteractionMenus.Custom
{
    public abstract class RadialMenuBaseFRMP : ScaleformUI.Menus.MenuBase
    {
        public abstract RadialMenu menu { get; set; }

        public readonly static string frmp_txd = "frmp_core";

        public readonly static string frmp_txn_release = "ped_radial_release_icon";
        public readonly static string frmp_txn_identification = "ped_radial_identification_icon";
        public readonly static string frmp_txn_cuffs = "ped_radial_cuffs_icon";
        public readonly static string frmp_txn_alcohol = "ped_radial_alcohol_icon";
        public readonly static string frmp_txn_drugs = "ped_radial_drugs_icon";
        public readonly static string frmp_txn_follow = "ped_radial_follow_icon";
        public readonly static string frmp_txn_grab = "ped_radial_grab_icon";
        public readonly static string frmp_txn_frisk = "ped_radial_frisk_icon";

        public abstract RadialMenu Menu();
    }
}
