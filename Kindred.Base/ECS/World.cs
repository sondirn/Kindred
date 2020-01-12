using Kindred.Base.ECS.Systems;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Collections;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kindred.Base.ECS
{
    public class World : SimpleDrawableGameComponent
    {
        private readonly Bag<IDrawSystem> _drawSystems;

        internal World()
        {
            _drawSystems = new Bag<IDrawSystem>();
        }
        public override void Draw(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
