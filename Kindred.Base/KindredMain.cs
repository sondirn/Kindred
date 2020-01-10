﻿using Comora;
using Kindred.Base.Graphics;
using Kindred.Base.Maps;
using Kindred.Base.Maps.Utils;
using Kindred.Base.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Kindred.Base
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class KindredMain : Game
    {
        readonly GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Map realmap;
        public Camera2D camera;
        public Assets assets;
        readonly MapRenderer mRenderer;
        public KindredMain()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.SynchronizeWithVerticalRetrace = true;
            IsFixedTimeStep = false;
            mRenderer = new MapRenderer();
            realmap = new Map();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Console.WriteLine(Common.DisplayVersion);
            // TODO: Add your initialization logic here

            base.Initialize();
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();
            camera = new Camera2D(GraphicsDevice, 640, 360, Comora.AspectMode.FillStretch);
            camera.LoadContent();
            camera.Camera.Zoom = 1f;
            camera.AddDebugLines(
                new int[]
                {
                    16, 16 * 4
                },
                new Color[]
                {
                    new Color(50, 50, 50, 50), new Color(0, 0, 100, 100)
                },
                new int[]
                {
                    1, 1
                }
                );
            realmap.GenerateMap("MapTest");
        }

        /// <summary>
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            assets = new Assets(Content);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            camera.Position = Mouse.GetState().Position.ToVector2();
            camera.Update(gameTime);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.TransparentBlack);
            spriteBatch.Begin(camera.Camera, SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);

            mRenderer.Draw(camera.GetScreenBounds(), spriteBatch, realmap);

            spriteBatch.End();
            spriteBatch.Draw(camera.Camera.Debug);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}