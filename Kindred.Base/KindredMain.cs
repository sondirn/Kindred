
using Comora;
using Kindred.Base.Maps;
using Kindred.Base.Utils;
using Kindred.Base.Utils.Input;
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
        public Assets assets;
        public Dependencies dependencies;
        readonly MapRenderer mRenderer;
        public KindredMain()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.SynchronizeWithVerticalRetrace = true;
            IsFixedTimeStep = false;
            mRenderer = new MapRenderer();
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
            IsMouseVisible = true;
            base.Initialize();
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width / 2;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height / 2;
            Window.IsBorderless = false;
            Window.Title = "Kindred " + "Version: " + Common.DisplayVersion;
            graphics.ApplyChanges();
            //Inject Dependencies
            dependencies = new Dependencies();
            Dependencies.CreateCamera(GraphicsDevice);
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
            KeyboardInput.Update();
            if (KeyboardInput.WasKeyJustDown(Keys.I))
                Dependencies.GenerateMap("Dungeon1");
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            Vector2 move = Vector2.Zero;
            move.X = KeyboardInput.GetAxis("Horizontal");
            move.Y = KeyboardInput.GetAxis("Vertical");
            if (KeyboardInput.IsKeyDown(Keys.LeftAlt) && KeyboardInput.WasKeyJustDown(Keys.Enter))
            {
                Window.IsBorderless = !Window.IsBorderless;
                if (Window.IsBorderless)
                {
                    graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
                    graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
                    graphics.ApplyChanges();
                }
                else
                {
                    graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width /2;
                    graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height /2;
                    graphics.ApplyChanges();
                } 
            }
            if (move != Vector2.Zero)
                move = Vector2.Normalize(move);
            Dependencies.GetCamera().Position += move * 320f * Common.GetDelta(gameTime);
            Dependencies.GetCamera().Update(gameTime);
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
            spriteBatch.Begin(Dependencies.GetCamera().Camera, SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);

            mRenderer.Draw(spriteBatch);

            spriteBatch.End();
            spriteBatch.Draw(Dependencies.GetCamera().Camera.Debug);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}