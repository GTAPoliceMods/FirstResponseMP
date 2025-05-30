﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CitizenFX.Core;
using CitizenFX.Core.Native;

using FirstResponseMP.Client.Functions;
using FirstResponseMP.Shared.Enums;

namespace FirstResponseMP.Client
{
    public class Main : BaseScript
    {
        public static MenuAlign MenuAlign = MenuAlign.Left;

        public Main()
        {
            Initial.Commands.RegisterAll();
            Initial.Player.LoadInfo();
            Initial.Menus.CreateAll();

            Debug.WriteLine("FirstResponseMP.Client loaded.");
        }
    }
}
