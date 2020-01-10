using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Kindred.Base.Utils
{
    public static class Common
    {
        public static string LOAD_DATA = Environment.CurrentDirectory + @"\Content\";
        public static Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
        public static DateTime buildDate = new DateTime(2020, 1, 8).AddDays(version.Build).AddSeconds(version.Revision * 2);
        public static string DisplayVersion = $"{version}";
        
        public static int Clamp(int value, int min, int max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }

        /// <summary>
        /// Splits a texture into an array of smaller textures of the specified size.
        /// </summary>
        /// <param name="original">The texture to be split into smaller textures</param>
        /// <param name="partWidth">The width of each of the smaller textures that will be contained in the returned array.</param>
        /// <param name="partHeight">The height of each of the smaller textures that will be contained in the returned array.</param>
        public static Texture2D[] Split(Texture2D texture,int CellWidth, int CellHeight)
        {
            var CellsWide = texture.Width / CellWidth;
            var CellsHigh = texture.Height / CellHeight;
            Texture2D[] r = new Texture2D[CellsWide * CellsHigh];
            int pixelsperTile = CellWidth * CellHeight;

            //get pixel data from texture
            Color[] originalData = new Color[texture.Width * texture.Height];
            texture.GetData<Color>(originalData);

            int index = 0;
            for (int y = 0; y < CellsHigh * CellHeight; y += CellHeight)
            {
                for (int x = 0; x < CellsWide * CellWidth; x += CellWidth)
                {
                    //create new texture
                    Texture2D cell = new Texture2D(texture.GraphicsDevice, CellWidth, CellHeight);
                    //data for tile
                    Color[] cellData = new Color[pixelsperTile];
                    //Fill tile data
                    for(int ty = 0; ty < CellHeight; ty++)
                    {
                        for (int tx = 0; tx < CellWidth; tx++)
                        {
                            int tileIndex = tx + (ty * CellWidth);
                            if (y + ty >= texture.Height || x + tx >= texture.Width)
                                cellData[tileIndex] = Color.Transparent;
                            else
                                cellData[tileIndex] = originalData[(x + tx) + ((y + ty) * texture.Width)];
                        }
                    }
                    cell.SetData<Color>(cellData);
                    r[index++] = cell;
                }
            }
            return r;
        }

        public static float GetDelta(GameTime gameTime)
        {
            return (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
