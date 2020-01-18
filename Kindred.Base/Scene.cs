using Comora;
using Kindred.Base.Maps;
using Kindred.Base.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kindred.Base
{
    public class Scene
    {
        private readonly MapRenderer mRenderer;
        private GraphicsDevice gd;
        public RenderTarget2D lightsTarget;

        public Scene(GraphicsDevice _gd)
        {
            gd = _gd;
            mRenderer = new MapRenderer();
        }

        public void Initialize()
        {
            Dependencies.CreateCamera(gd);
            Dependencies.GenerateMap("Dungeon1");
            var pp = gd.PresentationParameters;
            lightsTarget = new RenderTarget2D(gd, pp.BackBufferWidth, pp.BackBufferHeight);
        }

        public void DrawLights(SpriteBatch spriteBatch)
        {
            gd.SetRenderTarget(lightsTarget);
            spriteBatch.Begin(Dependencies.GetCamera().Camera, SpriteSortMode.Immediate, BlendState.Additive);
            spriteBatch.FillRectangle(Dependencies.GetCamera().GetScreenRectf(), new Color(7,26,76) * .5f);
            spriteBatch.End();
        }

        public void DrawScene(SpriteBatch spriteBatch)
        {
            mRenderer.Draw(spriteBatch);
        }
    }
}
