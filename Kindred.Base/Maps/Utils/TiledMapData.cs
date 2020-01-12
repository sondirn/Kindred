using Newtonsoft.Json;
using System;

namespace Kindred.Base.Maps.Utils
{
    public struct TiledMapData
    {
        [JsonProperty("height")]
        public int Height { get; set; }
        [JsonProperty("width")]
        public int Width { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("tilewidth")]
        public int TileWidth { get; set; }
        [JsonProperty("tileheight")]
        public int TileHeight { get; set; }
        [JsonProperty("orientation")]
        public string Orientation { get; set; }
        [JsonProperty("infinite")]
        public bool Infinite { get; set; }
        [JsonProperty("layers")]
        public TiledLayerData[] Layers { get; set; }
        [JsonProperty("tilesets")]
        public TileSetsData[] TileSets { get; set; }
        
    }
}