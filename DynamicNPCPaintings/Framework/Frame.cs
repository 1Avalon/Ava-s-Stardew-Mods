﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DynamicNPCPaintings.Framework
{
    public class Frame
    {
        public int startX;

        public int startY;

        public int endX;

        public int endY;

        public int spaceWidth;

        public int spaceHeight;

        public string textureName;

        private Texture2D _frameTexture;

        public Texture2D frameTexture
        {
            get
            {
                return _frameTexture ?? ModEntry.instance.Helper.GameContent.Load<Texture2D>(textureName);
            }
        }
        public static Frame GetFrameWithTexture(int startX, int startY, int endX, int endY, Texture2D frameTexture)
        {
            return new Frame(startX, startY, endX, endY, frameTexture);
        }
        private Frame(int startX, int startY, int endX, int endY, Texture2D frameTexture)
        {
            this.startX = startX;
            this.startY = startY;
            this.endX = endX;
            this.endY = endY;
            _frameTexture = frameTexture;
            spaceWidth = endX - startX; 
            spaceHeight = endY - startY;
        }
        public Frame(int startX, int startY, int endX, int endY, string textureName)
        {
            this.startX = startX;
            this.startY = startY;
            this.endX = endX;
            this.endY = endY;
            this.textureName = textureName;
            spaceWidth = endX - startX;
            spaceHeight = endY - startY;
        }

        public static Frame GetDefaultFrame()
        {
            return new Frame(4, 5, 45, 25, ModEntry.frame);
        }
    }
}
