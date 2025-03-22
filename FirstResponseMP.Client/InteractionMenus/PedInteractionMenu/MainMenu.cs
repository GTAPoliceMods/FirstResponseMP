using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CitizenFX.Core;
using CitizenFX.Core.Native;

using FirstResponseMP.Client.Functions;
using FirstResponseMP.Client.InteractionMenus.Custom;
using FirstResponseMP.Shared.Enums;

using ScaleformUI;
using ScaleformUI.Elements;
using ScaleformUI.Menu;
using ScaleformUI.Radial;
using ScaleformUI.Radio;

namespace FirstResponseMP.Client.InteractionMenus.PedInteractionMenu
{
    public class MainMenu : RadialMenuBaseFRMP
    {
        public override RadialMenu menu { get; set; }

        public MainMenu()
        {
            menu = new RadialMenu();
            menu.Enable3D = true;

            SegmentItem releasePed = new SegmentItem("Free The Person", "", frmp_txd, frmp_txn_release, 60, 60, SColor.HUD_Green);
            SegmentItem askForId = new SegmentItem("Ask For Identification", "", frmp_txd, frmp_txn_identification, 60, 60, SColor.HUD_Blue);
            SegmentItem toggleFollowMe = new SegmentItem("Follow Me", "", frmp_txd, frmp_txn_follow, 60, 60, SColor.HUD_Blue);
            SegmentItem toggleFollowMe2 = new SegmentItem("Stop Following", "", frmp_txd, frmp_txn_follow, 60, 60, SColor.HUD_Blue);
            SegmentItem friskPerson = new SegmentItem("Frisk Person", "", frmp_txd, frmp_txn_frisk, 60, 60, SColor.HUD_Blue);
            SegmentItem breathTestPerson = new SegmentItem("Breath Test Person", "", frmp_txd, frmp_txn_alcohol, 60, 60, SColor.HUD_Blue);
            SegmentItem drugTestPerson = new SegmentItem("Drug Test Person", "", frmp_txd, frmp_txn_drugs, 60, 60, SColor.HUD_Blue);
            SegmentItem toggleGrabPerson = new SegmentItem("Grab Person", "", frmp_txd, frmp_txn_grab, 60, 60, SColor.HUD_Blue);
            SegmentItem toggleGrabPerson2 = new SegmentItem("Release Person", "", frmp_txd, frmp_txn_grab, 60, 60, SColor.HUD_Blue);
            SegmentItem toggleCuffPerson = new SegmentItem("Cuff Person", "", frmp_txd, frmp_txn_cuffs, 60, 60, SColor.HUD_Red);
            SegmentItem toggleCuffPerson2 = new SegmentItem("Uncuff Person", "", frmp_txd, frmp_txn_cuffs, 60, 60, SColor.HUD_Red);

            menu.Segments[0].AddItem(releasePed);
            menu.Segments[1].AddItem(askForId);
            menu.Segments[2].AddItem(toggleFollowMe);
            menu.Segments[3].AddItem(friskPerson);
            menu.Segments[4].AddItem(breathTestPerson);
            menu.Segments[5].AddItem(drugTestPerson);
            menu.Segments[6].AddItem(toggleGrabPerson);
            menu.Segments[7].AddItem(toggleCuffPerson);
        }

        public override RadialMenu Menu()
        {
            return menu;
        }
    }
}
