﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kindred.Base.ECS.Systems
{
    public interface IDrawSystem: ISystem
    {
        void Draw(GameTime gameTime);
    }
    public abstract class DrawSystem : IDrawSystem
    {
        public virtual void Dispose() { }
        public virtual void Initialize(World world) { }
        public abstract void Draw(GameTime gameTime);
    }
}
