using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kindred.Base.ECS.Systems
{
    public interface IUpdateSystem : ISystem
    {
        void Update(GameTime gameTime);
    }
    public abstract class UpdateSystem
    {
    }
}
