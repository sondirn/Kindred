using Kindred.Base.Maps.Utils;

namespace Kindred.Base.Maps
{
    public struct Layer
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int[,] Data { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool Visible { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public void FillData(TiledLayerData data)
        {
            //Set properties
            Height = data.Height;
            Width = data.Width;
            Name = data.Name;
            Type = data.Type;
            Visible = data.Visible;
            X = data.X;
            Y = data.Y;

            int i = 0;
            int[,] result = new int[Height, Width];

            // Console.WriteLine("-------------------------------");
            //Console.WriteLine("Layer: " + Name);

            for (int x = 0; x < Height; x++)
            {
                for (int y = 0; y < Width; y++)
                {
                    result[x, y] = data.Data[i];
                    i++;
                    //Console.Write(result[x, y] + " ");
                }
                //Console.WriteLine();
            }
            Data = result;
        }
    }
}