using Kindred.Base.Maps.Utils;
using Kindred.Base.Utils;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Kindred.Base.Maps
{
    public class Map
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int TileWidth { get; set; }
        public int TileHeight { get; set; }
        public Layer[] Layers { get; set; }
        public Texture2D[] TileArray { get; set; }
        public string[] TileSetNames { get; set; }
        public string Name { get; set; }

        private void FillMap(TiledMapData data)
        {
            Width = data.Width;
            Height = data.Height;
            TileWidth = data.TileWidth;
            TileHeight = data.TileHeight;
            Layers = new Layer[data.Layers.Length];
            for (int i = 0; i < data.Layers.Length; i++)
            {
                Layers[i].FillData(data.Layers[i]);
            }
            var tempdata = new List<Texture2D>();
            TileSetNames = new string[data.TileSets.Length];
            for (int i = 0; i < data.TileSets.Length; i++)
            {
                Assets.AddTexture(data.TileSets[i].Name);
                TileSetNames[i] = data.TileSets[i].Name;
                tempdata.AddRange(Common.Split(Assets.GetTexture(data.TileSets[i].Name), TileWidth, TileHeight));
            }
            TileArray = new Texture2D[tempdata.Count];
            TileArray = tempdata.ToArray();
            Debug.WriteLine("Map " + Name + " Has Been Loaded");
            
        }

        public void GenerateMap(string mapName)
        {
            if(Name == mapName)
            {
                Console.WriteLine("Map: " + mapName + " Is Already Loaded");
                return;
            }
            
            if(Layers != null)
            {
                TileArray = null;
                foreach (Layer layer in Layers)
                {
                    layer.Dispose();
                    
                }
                foreach (string tileSet in TileSetNames)
                {
                    Assets.RemoveTexture(tileSet);
                }
                System.GC.Collect();
            }
            
            Name = mapName;
            FillMap(JsonDeserialize.DeserializeJSON<TiledMapData>(mapName + ".Json"));
            Console.WriteLine("Map " + mapName + "Has Loaded");
        }
    }
}