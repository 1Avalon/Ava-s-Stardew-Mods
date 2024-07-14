﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using StardewValley.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicNPCPaintings.UI
{
    public class SelectBackgroundUI : IClickableMenu
    {
        private Customiser customiser;

        public List<ClickableTextureComponent> validBackgrounds = new List<ClickableTextureComponent>();

        private string hoverText = "";
        public SelectBackgroundUI(Customiser customiser) 
        {
            int width = 960;
            int height = 720;
            this.customiser = customiser;
            base.initialize(Game1.viewport.Width / 2 - width / 2, Game1.viewport.Height / 2 - height / 2, width, height);

            int startPositionX = xPositionOnScreen + 50;
            int startPositionY = yPositionOnScreen + 110;
            int backgroundScale = 4;

            upperRightCloseButton = new ClickableTextureComponent(new Rectangle(xPositionOnScreen + width - 50, yPositionOnScreen + 69, 48, 48), Game1.mouseCursors, new Rectangle(337, 494, 12, 12), 4f);

            foreach (var kvp in ModEntry.backgroundImages)
            {
                int bgWidth = kvp.Value.Width;
                int bgHeight = kvp.Value.Height;

                ClickableTextureComponent component = new ClickableTextureComponent(new Rectangle(startPositionX, startPositionY, bgWidth * backgroundScale, bgHeight * backgroundScale), kvp.Value, new Rectangle(0, 0, kvp.Value.Width, kvp.Value.Height), backgroundScale);
                component.name = kvp.Key;

                if (kvp.Value.Width < customiser.picture.frame.spaceWidth || kvp.Value.Height < customiser.picture.frame.spaceHeight)
                    continue;
                
                validBackgrounds.Add(component);

                startPositionX += bgWidth * backgroundScale + 5;
                if (startPositionX > xPositionOnScreen + this.width - 16 * backgroundScale)
                {
                    startPositionX = xPositionOnScreen + 30;
                    startPositionY += 32 * backgroundScale + 5;
                }
            }
            exitFunction = () => { Game1.activeClickableMenu = customiser; };
        }

        public override void receiveLeftClick(int x, int y, bool playSound = true)
        {
            if (upperRightCloseButton.containsPoint(x, y))
                Game1.activeClickableMenu = customiser;
            else
            {
                foreach (ClickableTextureComponent component in validBackgrounds)
                {
                    if (component.containsPoint(x, y))
                    {
                        customiser.picture.background = new Framework.Background(0, 0, component.texture);
                        customiser.UpdatePreview();
                        Game1.activeClickableMenu = customiser;
                    }
                }
            }
        }

        public override void performHoverAction(int x, int y)
        {
            base.performHoverAction(x, y);
            hoverText = "";
            foreach (ClickableTextureComponent component in validBackgrounds)
            {
                if (component.containsPoint(x, y))
                {
                    hoverText = component.name;
                }
            }
        }
        public override void draw(SpriteBatch b)
        {
            b.Draw(Game1.fadeToBlackRect, Game1.graphics.GraphicsDevice.Viewport.Bounds, Color.Black * 0.75f);
            Game1.drawDialogueBox(xPositionOnScreen, yPositionOnScreen, width, height, false, true);
            foreach (ClickableTextureComponent component in validBackgrounds)
                component.draw(b);

            upperRightCloseButton.draw(b);
            drawHoverText(b, hoverText, Game1.smallFont);
            drawMouse(b);
        }
    }
}
