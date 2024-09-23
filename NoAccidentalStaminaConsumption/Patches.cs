﻿using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.TerrainFeatures;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoAccidentalStaminaConsumption
{
    public static class Patches
    {
        public static void WateringCan_Prefix_DoFunction(WateringCan __instance, GameLocation location, int x , int y, int power, Farmer who)
        {
            if (Game1.currentLocation.CanRefillWateringCanOnTile(x / 64, y / 64))
                return;

            power = who.toolPower;
            List<Vector2> tileLocations = ModEntry.instance.Helper.Reflection.GetMethod(__instance, "tilesAffected").Invoke<List<Vector2>>(new Vector2(x / 64, y / 64), power, who);
            int wateredCrops = 0;
            int dirtTiles = 0;
            foreach(Vector2 tileLocation in tileLocations)
            {
                if (location.terrainFeatures.TryGetValue(tileLocation, out var terrainFeature))
                {
                    if (terrainFeature is HoeDirt dirt)
                    {
                        dirtTiles++;

                        if (dirt.isWatered())
                        {
                            wateredCrops++;
                        }
                    }
                }

            }

            if (wateredCrops == dirtTiles)
            {
                float stamina = (float)(2 * (power + 1)) - (float)who.FarmingLevel * 0.1f;
                who.Stamina += stamina;
            }
        }
    }
}
