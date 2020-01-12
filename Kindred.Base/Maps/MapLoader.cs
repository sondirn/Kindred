using Kindred.Base.Maps.Utils;
using Kindred.Base.Utils;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Kindred.Base.Maps
{
    public class MapLoader
    {
        private static int dataTick = 0;
        //public const double frameTime = ‭0.033‬;
        public static void GenerateMap(Map map, string mapName)
        {
            if(map.Name == mapName)
            {
                Debug.WriteLine("Map " + mapName + " Has Already Been Loaded");
                return;
            }
            if (map.Layers != null)
            {
                foreach (Layer layer in map.Layers)
                {
                    layer.Dispose();
                    System.GC.Collect();
                }
            }

            map.Name = mapName;

            //Fill the map
            var data = JsonDeserialize.DeserializeJSON<TiledMapData>(mapName + ".Json");
            map.Width = data.Width;
            map.Height = data.Height;
            map.TileWidth = data.TileHeight;
            map.TileHeight = data.TileHeight;
            map.Layers = new Layer[data.Layers.Length];
            for (int p = 0; p < data.Layers.Length; p++)
            {
                var layer = map.Layers[p];
                var _data = data.Layers[p];
                layer.Height = _data.Height;
                layer.Width = _data.Width;
                layer.Name = _data.Name;
                layer.Type = _data.Type;
                layer.Visible = _data.Visible;
                layer.X = _data.X;
                layer.Y = _data.Y;

                int i = 0;
                int[,] result = new int[layer.Height, layer.Width];

                for (int x = 0; x < layer.Height; x++)
                {
                    for (int y = 0; y < layer.Width; y++)
                    {
                        result[x, y] = _data.Data[i];
                        i++;
                    }
                }
                layer.Data = result;
            }
            var tempdata = new List<Texture2D>();
            for (int i = 0; i < data.TileSets.Length; i++)
            {
                Assets.AddTexture(data.TileSets[i].Name);
                tempdata.AddRange(Common.Split(Assets.GetTexture(data.TileSets[i].Name), map.TileWidth, map.TileHeight));

            }
            map.TileArray = new Texture2D[tempdata.Count];
            map.TileArray = tempdata.ToArray();
            Debug.WriteLine("Map " + map.Name + " Has Been Loaded");
        }
    }
}