using Newtonsoft.Json;

namespace Kindred.Base.Maps.Utils
{
    public struct TileSetsData
    {
        [JsonProperty("firstgid")]
        public int Firstgid { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}