using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CitizenFX.Core;

using FirstResponseMP.Client.Menus;

using ScaleformUI;
using ScaleformUI.Menu;

namespace FirstResponseMP.Client.Functions
{
    public class MenuFunctions : BaseScript
    {
        public async void RestartMenu(UIMenu menu, bool isMainMenu)
        {
            Debug.WriteLine("[DEBUG] Restarting Now");

            await Task.Delay(0);

            menu.Visible = false; Debug.WriteLine("[DEBUG] Menu Hidden");
            menu.Clear(); Debug.WriteLine("[DEBUG] Menu Cleared");
            Initial.Menus.CreateAll(); Debug.WriteLine("[DEBUG] Re-init Menus");
            MainMenu.Menu().Visible = true; Debug.WriteLine("[DEBUG] Main Menu Visible");

            await Task.Delay(500); Debug.WriteLine("[DEBUG] Wait");

            if (!isMainMenu)
            {
                Debug.WriteLine("[DEBUG] Not Main Menu");
                await MainMenu.Menu().SwitchTo(menu, menu.CurrentSelection, true); Debug.WriteLine("[DEBUG] Switched Menu");
            }
            else
            {
                Debug.WriteLine("[DEBUG] Is Main Menu");
                MainMenu.Menu().CurrentSelection = menu.CurrentSelection; Debug.WriteLine("[DEBUG] Changed Selection");
            }
        }
    }
}
