using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kindred.Base.ECS.Components
{
    public class SpriteComponent
    {
        public string Texture { get; set; }
        public Vector2 Origin { get; set; }
    }
}
