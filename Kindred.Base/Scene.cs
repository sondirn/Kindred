using Comora;
using Kindred.Base.ECS.Components;
using Kindred.Base.ECS.Systems.DrawSystems;
using Kindred.Base.Graphics.LightSystem;
using Kindred.Base.Maps;
using Kindred.Base.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kindred.Base
{
    public class Scene
    {
        private readonly MapRenderer mRenderer;
        private readonly GraphicsDevice gd;
        private World _world;
        private Entity entity;
        private Entity entity2;
        SpriteBatch sb;
        private Texture2D mask;

        //ToBeRemoved


        public Scene(GraphicsDevice _gd)
        {
            gd = _gd;
            sb = new SpriteBatch(gd);
            mRenderer = new MapRenderer();
            _world = new WorldBuilder().AddSystem(new LightsSystem(gd)).Build();
            entity = _world.CreateEntity();
            entity2 = _world.CreateEntity();
            entity.Attach(new Light
            {
                BayerMask = "BayerMatrix1024",
                Radius = 364,
                Intensity = 1f,
                Color = new Vector3(255, 0, 0),
                InnerIntensity = 2,
                InnerRadius = 1
            }) ;
            entity.Attach(new Position2D
            {
                Position = new Vector2(100, 100)
            });
            entity2.Attach(new Light
            {
                BayerMask = "BayerMatrix1024",
                Radius = 364,
                Intensity = .7f,
                Color = new Vector3(255, 255, 255),
                InnerIntensity = 2,
                InnerRadius = 1
            });
            entity2.Attach(new Position2D
            {
                Position = new Vector2(132, 132)
            });


        }
        public void Initialize()
        {
            Dependencies.CreateCamera(gd);
            Dependencies.GenerateMap("Dungeon1");
            Assets.AddTexture(@"Effects\BayerMatrix2048");
            mask = Assets.GetTexture(@"Effects\BayerMatrix2048");
        }

        public void Update(GameTime gameTime)
        {
            _world.Update(gameTime);
           // entity.Get<Position2D>().Position += new Vector2(.5f, .5f);
            Console.WriteLine(entity.Get<Position2D>().Position);
            Console.WriteLine(_world.EntityCount);
        }

        public void DrawLights(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //gd.SetRenderTarget(Assets.GetRenderTarget("LightsTarget"));
            Dependencies.GetSB().Begin(Dependencies.GetCamera().Camera, SpriteSortMode.Immediate, BlendState.Additive);
            Dependencies.GetSB().FillRectangle(Dependencies.GetCamera().GetScreenRectf(), new Color(255,255,255) * .3f);
            Dependencies.GetSB().End();
            
        }

        public void DrawScene(GameTime gameTime, SpriteBatch spriteBatch)
        {
            gd.SetRenderTarget(Assets.GetRenderTarget("LightsTarget"));
            
            DrawLights(gameTime, spriteBatch);
            _world.Draw(gameTime);
            Dependencies.GetSB().Begin(Dependencies.GetCamera().Camera, SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);
            mRenderer.Draw(sb);
            Dependencies.GetSB().End();
            
            
        }
    }
}
