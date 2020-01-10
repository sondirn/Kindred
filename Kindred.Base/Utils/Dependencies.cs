using Kindred.Base.Graphics;
using Kindred.Base.Maps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kindred.Base.Utils
{
    public class Dependencies
    {
        private static Camera2D camera;
        private static Map map;

        public Dependencies()
        {
            
        }
        #region camera functions
        public static void CreateCamera(GraphicsDevice graphics)
        {
            camera = new Camera2D(graphics, 640, 360, Comora.AspectMode.FillStretch);
            camera.LoadContent();
            camera.Camera.Zoom = 1f;
            camera.AddDebugLines(
                new int[]
                {
                    16, 16 * 4
                },
                new Color[]
                {
                    new Color(50, 50, 50, 50), new Color(0, 0, 100, 100)
                },
                new int[]
                {
                    1, 1
                }
                );
        }

        public static Camera2D GetCamera()
        {
            return camera;
        }
        #endregion
        #region Map Functions
        public static void GenerateMap(string name)
        {
            if (map == null)
                map = new Map();
            map.GenerateMap(name);
        }

        public static Map GetMap()
        {
            return map;
        }
        #endregion
    }
}
