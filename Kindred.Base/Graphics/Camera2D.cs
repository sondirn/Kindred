using Comora;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kindred.Base.Graphics
{
    public class Camera2D
    {
        public Camera Camera;
        public Vector2 Position { get
            {
                return Camera.Position;
            }
            set
            {
                Camera.Position = value;
            }
        }
        public Camera2D(GraphicsDevice device, int cWidth, int cHeight, AspectMode cAspect = AspectMode.Expand)
        {
            Camera = new Camera(device)
            {
                Width = cWidth,
                Height = cHeight,
                ResizeMode = cAspect
            };
            Camera.Debug.IsVisible = true;
            Camera.Position = Vector2.Zero;
        }

        public void AddDebugLines(int[] size, Color[] color, int[] width)
        {
            for (int i = 0; i < size.Length; i++)
            {
                Camera.Debug.Grid.AddLines(size[i], color[i], width[i]);
            }
        }
        public void ReplaceDebugLines(int[] size, Color[] color, int[] width)
        {
            Camera.Debug.Grid.RemoveLines();
            for (int i = 0; i < size.Length; i++)
            {
                Camera.Debug.Grid.AddLines(size[i], color[i], width[i]);
            }
        }

        public void DeleteDebugLines()
        {
            Camera.Debug.Grid.RemoveLines();
        }
        public void LoadContent()
        {
            Camera.LoadContent();
        }

        public void Update(GameTime gameTime)
        {
            Camera.Update(gameTime);
        }

        public Vector2 ScreenToWorldTile(Vector2 screenPosition, Vector2 tileSize)
        {
            return Vector2.Transform(screenPosition, Camera.ViewportOffset.Absolute) / tileSize;
        }

        public Vector2 ToWorld(Vector2 screenPosition)
        {
            return Vector2.Transform(screenPosition, Camera.ViewportOffset.Absolute);
        }

        public Rectangle ScreenBounds()
        {
            if (Camera.Zoom > 1)
            {
                return new Rectangle((int)((Camera.Position.X) - (Camera.Width / 2f)), (int)((Camera.Position.Y) - (Camera.Height / 2f)), (int)(Camera.Width), (int)(Camera.Height));
            }
            else
            {
                var regWidth = Camera.Width;
                var regHeight = Camera.Height;
                var newHeight = regHeight * (1 / Camera.Zoom);
                var newWidth = regWidth * (1 / Camera.Zoom);
                return new Rectangle((int)((Camera.Position.X - ((newWidth - regWidth) / 2)) - (Camera.Width / 2f)), (int)((Camera.Position.Y - ((newHeight - regHeight) / 2)) - (Camera.Height / 2f)), (int)(newWidth), (int)(newHeight));
            }

        }

    }
}
