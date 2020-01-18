﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kindred.Base.Graphics.LightSystem
{
    public class Light
    {
        public string BayerMask { get; set; }
        public int Radius { get; set; }
        public float Intensity { get; set; }
        public Vector3 Color { get; set; }
        public Vector2 Position { get; set; }
    }
}
