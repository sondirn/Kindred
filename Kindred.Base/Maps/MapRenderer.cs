using Kindred.Base.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
                                sb.Draw(map.TileArray[layer.Data[y, x] - 1], new Vector2(x * map.TileWidth, y * map.TileHeight), Color.White);
                                //TODO: Is there a better way to do this? Perhaps Have vector start at 1, let index 0 be a blank texture??
                            }
                        }
                    }
                }
            }
        }
    }
}