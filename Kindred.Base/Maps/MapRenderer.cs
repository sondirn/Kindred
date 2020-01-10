using Kindred.Base.Maps.Utils;
using Kindred.Base.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

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
        public void Draw(Rectangle bounds, SpriteBatch sb, Map Map)
        {
            int startX = Common.Clamp((bounds.X), 0, Map.Width * Map.TileWidth);
            int startY = Common.Clamp((bounds.Y), 0, Map.Height * Map.TileHeight);
            int endX = Common.Clamp(startX + (bounds.Width), 0, Map.Width * Map.TileWidth);
            int endY = Common.Clamp(startY + (bounds.Height), 0, Map.Height * Map.TileHeight);
            startX /= Map.TileWidth;
            startY /= Map.TileHeight;
            endY /= Map.TileHeight;
            endX /= Map.TileWidth;
            endY += 1;
            endX += 1;
            Console.WriteLine(startX);
            Console.WriteLine(endX);

            foreach (Layer layer in Map.Layers)
            {

                for (int y = startY; y < endY; y++)
                {
                    for (int x = startX; x < endX; x++)
                    {
                        if (x < layer.Data.GetLength(0) && y < layer.Data.GetLength(1))
                        {
                            if (layer.Data[y, x] != 0)
                            {
                                sb.Draw(Map.TileArray[layer.Data[y, x] - 1], new Vector2(x * 16, y * 16), Color.White);

                            }
                            
                        }
                        

                    }
                }
            }
        }

    }
}
