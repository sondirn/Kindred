using Kindred.Base.Maps;
using Kindred.Base.Utils;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kindred.Base
{
    public class Scene
    {
        private readonly MapRenderer mRenderer;

        public Scene()
        {
            mRenderer = new MapRenderer();
        }

        public void Initialize(GraphicsDevice gd)
        {
            Dependencies.CreateCamera(gd);
            Dependencies.GenerateMap("Dungeon1");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            mRenderer.Draw(spriteBatch);
        }
    }
}
