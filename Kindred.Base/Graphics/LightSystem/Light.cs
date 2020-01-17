using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kindred.Base.Graphics.LightSystem
{
    public abstract class Light
    {
        public Texture2D LUTAsset { get; set; }
        public float Intensity { get; set; }
        public Color LightColor { get; set; }
    }
}
