using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Kindred.Base.Utils
{
    public class Assets
    {
        private static ContentManager content;
        private static GraphicsDevice gd;
        private static Dictionary<string, Texture2D> textures;
        private static Queue<string> textureQueue;
        private static Dictionary<string, RenderTarget2D> renderTargets;
        private static Dictionary<EffectType, Effect> effects;

        public Assets()
        {   
            textures = new Dictionary<string, Texture2D>();
            textureQueue = new Queue<string>();
            renderTargets = new Dictionary<string, RenderTarget2D>();
        }

        public static void Update(GraphicsDevice graphics)
        {
            gd = graphics;
        }

        public static void Initialize(GraphicsDevice graphics)
        {
            gd = graphics;
            var pp = gd.PresentationParameters;
            renderTargets.Add("MainTarget", new RenderTarget2D(gd, pp.BackBufferWidth, pp.BackBufferHeight));
            renderTargets.Add("LightsTarget", new RenderTarget2D(gd, pp.BackBufferWidth, pp.BackBufferHeight));
        }
        public static void LoadContent(ContentManager contentManager, GraphicsDevice device)
        {
            content = contentManager;
            effects = new Dictionary<EffectType, Effect>();
            effects.Add(EffectType.PointLight, content.Load<Effect>(@"Effects\radialGradient"));
            effects.Add(EffectType.LightMultiplication, content.Load<Effect>(@"Effects\lighteffect"));
        }

        public static Texture2D GetTexture(string name)
        {
            return textures[name];
        }

        public static void AddTexture(string name)
        {
            if (!textures.ContainsKey(name))
            {
                
                textures.Add(name, content.Load<Texture2D>(name));
                Console.WriteLine("Texture Asset Loaded " + name);
                
            }
            else
            {
                Console.WriteLine("Texture Asset " + name + " Has Already Been Added");
            }
        }

        public static void RemoveTexture(string name)
        {
            if (textures.ContainsKey(name))
            {
                //textures[name].Dispose();
                textures.Remove(name);
                Console.WriteLine("Texture Asset " + name + " Has Been Removed");
            }
            else
            {
                Console.WriteLine("Texture Asset " + name + "Can't be removed, It does not exist");
            }
        }

        public static void FlushTextures()
        {
            textures.Clear();
            Console.WriteLine("Textures have been flushed");
        }

        public static RenderTarget2D GetRenderTarget(string name)
        {
            return renderTargets[name];
        }

        public static void ResizeRenderTargets2D(string name)
        {
            var pp = gd.PresentationParameters;
            renderTargets[name] = new RenderTarget2D(gd, pp.BackBufferWidth, pp.BackBufferHeight);
            
        }

        public static GraphicsDevice GetGraphicsDevice()
        {
            return gd;
        }

        private static void loadEffects()
        {
            effects = new Dictionary<EffectType, Effect>();
            effects.Add(EffectType.PointLight, content.Load<Effect>("radialGradient"));
            effects.Add(EffectType.LightMultiplication, content.Load<Effect>("Lighteffect"));
        }

        public static Effect GetEffect(EffectType type)
        {
            return effects[type];
        }
    }
}

public enum EffectType
{
    PointLight,
    LightMultiplication
}