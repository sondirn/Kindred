﻿using Kindred.Base.Maps.Utils;
using Kindred.Base.Utils;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

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
            for(int i = 0; i < data.TileSets.Length; i++)
            {
                Assets.AddTexture(data.TileSets[i].Name);
                tempdata.AddRange(Common.Split(Assets.GetTexture(data.TileSets[i].Name), TileWidth, TileHeight));
            }
            TileArray = new Texture2D[tempdata.Count];
            TileArray = tempdata.ToArray();
            Console.WriteLine("Map " + Name + " Has Been Loaded");

        }

        public void GenerateMap(string mapName)
        {
            Name = mapName;
            FillMap(JsonDeserialize.DeserializeJSON<TiledMapData>(mapName + ".Json"));
        }
    }
}
