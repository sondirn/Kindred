using Kindred.Base.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Kindred.Base.Maps
{
    public class MapRenderer
    {
        

        public MapRenderer()
        {
            
        }


        public void Update(GameTime gameTime)
        {

        }
        public void Draw(SpriteBatch sb)
        {
            var map = Dependencies.GetMap();
            var bounds = Dependencies.GetCamera().ScreenBounds;
            

            foreach (Layer layer in map.Layers)
            {

                for (int y = bounds.StartY; y < bounds.EndY; y++)
                {
                    for (int x = bounds.StartX; x < bounds.EndX; x++)
                    {
                        if (x < layer.Data.GetLength(0) && y < layer.Data.GetLength(1))
                        {
                            if (layer.Data[y, x] != 0)
                            {
                                sb.Draw(map.TileArray[layer.Data[y, x] - 1], new Vector2(x * 16, y * 16), Color.White);

                            }
                            
                        }
                        

                    }
                }
            }
        }

    }
}
