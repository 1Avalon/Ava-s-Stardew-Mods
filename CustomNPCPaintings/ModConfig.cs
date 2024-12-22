﻿using StardewModdingAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomNPCPaintings
{
    public class ModConfig
    {
        public SButton openCustomizerButton { get; set; }

        public bool enableAllNPCs = false;

        public bool exportPaintingsLocally = false;

        public bool enableFarmerSprite = true;
    }
}
