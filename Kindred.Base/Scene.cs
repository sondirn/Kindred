using Comora;
using Kindred.Base.ECS.Components;
using Kindred.Base.ECS.Systems.DrawSystems;
using Kindred.Base.ECS.Systems.UpdateSystems;
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
        private Entity player;
        private float brigthness = .01f;
        private bool brightnessUp = true;

        //ToBeRemoved


        public Scene(GraphicsDevice _gd)
        {
            gd = _gd;
            sb = new SpriteBatch(gd);
            mRenderer = new MapRenderer();
            _world = new WorldBuilder().AddSystem(new LightsSystem(gd))
                .AddSystem(new PlayerControllerSystem())
                .AddSystem(new PhysicsSystem()).Build();
            entity = _world.CreateEntity();
            entity2 = _world.CreateEntity();
            entity.Attach(new LightComponent
            {
                BayerMask = "BayerMatrix1024",
                Radius = 364,
                Intensity = 1f,
                Color = new Vector3(255, 0, 0),
                InnerIntensity = 2,
                InnerRadius = 1
            }) ;
            entity.Attach(new TransformComponent
            {
                Position = new Vector2(100, 100)
            });
            entity2.Attach(new LightComponent
            {
                BayerMask = "BayerMatrix1024",
                Radius = 364,
                Intensity = .7f,
                Color = new Vector3(255, 255, 255),
                InnerIntensity = 2,
                InnerRadius = 1
            });
            entity2.Attach(new TransformComponent
            {
                Position = new Vector2(132, 132)
            });
            player = _world.CreateEntity();
            player.Attach(new TransformComponent
            {
                Position = new Vector2(50, 50)
            });
            player.Attach(new PlayerControllerComponent
            {
                
            });
            player.Attach(new RigidBody
            {
                Velocity = new Vector2(0, 0)
            });

        }
        public void Initialize()
        {
            Dependencies.CreateCamera(gd);
            Dependencies.GenerateMap("Dungeon1");
            Assets.AddTexture(@"Effects\BayerMatrix2048");
            _world.Initialize();
            
        }

        public void Update(GameTime gameTime)
        {
            _world.Update(gameTime);
            if(brightnessUp)
                brigthness += .25f * (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
            if (!brightnessUp)
                brigthness -= .25f * (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
            if (brigthness >= 2f)
                brightnessUp = false;
            if (brigthness <= -1f)
                brightnessUp = true;
            
        }

        public void DrawLights(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //gd.SetRenderTarget(Assets.GetRenderTarget("LightsTarget"));
            Dependencies.GetSB().Begin(Dependencies.GetCamera().Camera, SpriteSortMode.Immediate, BlendState.Additive);
            Dependencies.GetSB().FillRectangle(Dependencies.GetCamera().GetScreenRectf(), new Color(255,255,255) * brigthness);
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
