using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kindred.Base.Utils
{
    public class Assets
    {
        private static ContentManager content;
        private static Dictionary<string, Texture2D> textures;

        public Assets(ContentManager contentManager)
        {
            content = contentManager;
            textures = new Dictionary<string, Texture2D>();
        }

        public static Texture2D GetTexture(string name)
        {
            return textures[name];
        }

        public static void AddTexture(string name)
        {
            if (!textures.ContainsKey(name))
            {
                var tex = content.Load<Texture2D>(name);
                textures.Add(name, tex);
            }
            else
            {
                Console.WriteLine("Texture Asset " + name + " Has Already Been Added");
            }
        }
    }
}
