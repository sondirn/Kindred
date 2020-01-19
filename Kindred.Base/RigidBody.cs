using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kindred.Base.ECS.Components
{
    public class RigidBody
    {
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
    }
}

