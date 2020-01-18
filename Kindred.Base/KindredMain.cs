
using Comora;
using Kindred.Base.Maps;
using Kindred.Base.Utils;
using Kindred.Base.Utils.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

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
        public Scene scene;
        public static Texture2D bayerMask;
        public static Texture2D lowRezMask;
        private bool maskOn;
        public KindredMain()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.SynchronizeWithVerticalRetrace = true;
            IsFixedTimeStep = false;
            assets = new Assets();
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
           
            maskOn = false;
            // TODO: Add your initialization logic here
            IsMouseVisible = true;
            base.Initialize();
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width / 2;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height / 2;
            Window.IsBorderless = false;
            Window.Title = "Kindred " + "Version: " + Common.DisplayVersion;
            graphics.ApplyChanges();
            //Inject Dependencies

            Assets.Initialize(GraphicsDevice);
            dependencies = new Dependencies();
            scene = new Scene(GraphicsDevice);
            Dependencies.SetSpriteBatch(spriteBatch);
            scene.Initialize();
            //Assets.ResizeRenderTargets2D();
            

        }

        /// <summary>
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            Assets.LoadContent(Content, GraphicsDevice);
            Assets.AddTexture("lightmask");
            bayerMask = Content.Load<Texture2D>(@"Effects\BayerMatrix1024");
            lowRezMask = Content.Load<Texture2D>(@"Effects\BayerMatrix256");

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
            scene.Update(gameTime);
            Assets.Update(GraphicsDevice);  
            KeyboardInput.Update();
            if (KeyboardInput.WasKeyJustDown(Keys.I))
                Dependencies.GenerateMap("Dungeon1");
            if (KeyboardInput.WasKeyJustDown(Keys.P))
                Dependencies.GenerateMap("MapTest");
            if (KeyboardInput.WasKeyJustDown(Keys.J))
                maskOn = !maskOn;
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
                   
                    Assets.ResizeRenderTargets2D("LightsTarget");
                    Assets.ResizeRenderTargets2D("MainTarget");
                }
                else
                {
                    graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width /2;
                    graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height /2;
                    graphics.ApplyChanges();
                    
                    Assets.ResizeRenderTargets2D("LightsTarget");
                    Assets.ResizeRenderTargets2D("MainTarget");
                    //mainTarget = new RenderTarget2D(GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight);
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

            


            //spriteBatch.End();

           
            GraphicsDevice.Clear(Color.Transparent);
            
            scene.DrawScene(gameTime, spriteBatch);
           
            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.Black);


            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            //Assets.GetEffect(EffectType.LightMultiplication).Parameters["lightMask"].SetValue(Assets.GetRenderTarget("LightsTarget"));
            //Assets.GetEffect(EffectType.LightMultiplication).CurrentTechnique.Passes[0].Apply();
            spriteBatch.Draw(Assets.GetRenderTarget("LightsTarget"), Vector2.Zero, Color.White);
             
            spriteBatch.End();

            spriteBatch.Draw(Dependencies.GetCamera().Camera.Debug);
            //spriteBatch.Draw(Dependencies.GetCamera().Camera.Debug);
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}