using Comora;
using Kindred.Base.Utils;
using Kindred.Base.Utils.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace Kindred.Base.Graphics
{
    public class Camera2D
    {
        public Camera Camera;
        public ScreenBounds ScreenBounds;
        public Vector2 Position
        {
            get
            {
                return Camera.Position;
            }
            set
            {
                Camera.Position = value;
            }
        }

        public object Logger { get; private set; }

        public Camera2D(GraphicsDevice device, int cWidth, int cHeight, AspectMode cAspect = AspectMode.Expand)
        {
            Camera = new Camera(device)
            {
                Width = cWidth,
                Height = cHeight,
                ResizeMode = cAspect
            };
            Camera.Debug.IsVisible = false;
            Camera.Position = Vector2.Zero;
        }
        public void Update(GameTime gameTime)
        {
            if (KeyboardInput.WasKeyJustDown(Microsoft.Xna.Framework.Input.Keys.F1))
            {
                Camera.Debug.IsVisible = !Camera.Debug.IsVisible;
            }
            //Position = new Vector2(Common.Clamp((int)Position.X, 0 - GetScreenRect().Left, Dependencies.GetMap().Width * 16), Camera.Position.Y);
            UpdateScreenBounds();
            Camera.Update(gameTime);
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
        public Vector2 ScreenToWorldTile(Vector2 screenPosition, Vector2 tileSize)
        {
            return Vector2.Transform(screenPosition, Camera.ViewportOffset.Absolute) / tileSize;
        }

        public Vector2 ToWorld(Vector2 screenPosition)
        {
            return Vector2.Transform(screenPosition, Camera.ViewportOffset.Absolute);
        }

        public RectangleF GetScreenRectf()
        {
            if (Camera.Zoom > 1)
            {
                return new RectangleF(((Camera.Position.X) - (Camera.Width / 2f)), ((Camera.Position.Y) - (Camera.Height / 2f)), (Camera.Width), (Camera.Height));
            }
            else
            {
                var regWidth = Camera.Width;
                var regHeight = Camera.Height;
                var newHeight = regHeight * (1 / Camera.Zoom);
                var newWidth = regWidth * (1 / Camera.Zoom);
                return new RectangleF(((Camera.Position.X - ((newWidth - regWidth) / 2)) - (Camera.Width / 2f)), ((Camera.Position.Y - ((newHeight - regHeight) / 2)) - (Camera.Height / 2f)), (newWidth), (newHeight));
            }
        }

        public Rectangle GetScreenRect()
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
        public void UpdateScreenBounds()
        {
            
            var map = Dependencies.GetMap();
            if (map == null)
                return;
            var bound = GetScreenRect();
            ScreenBounds.StartX = Common.Clamp(bound.X, 0, map.Width * map.TileWidth);
            ScreenBounds.StartY = Common.Clamp(bound.Y, 0, map.Height * map.TileHeight);
            ScreenBounds.EndX = Common.Clamp(ScreenBounds.StartX + bound.Width, 0, map.Width * map.TileWidth);
            ScreenBounds.EndY = Common.Clamp(ScreenBounds.StartY + bound.Height, 0, map.Height * map.TileHeight);
            ScreenBounds.StartX /= map.TileWidth;
            ScreenBounds.StartY /= map.TileHeight;
            ScreenBounds.EndX /= map.TileWidth;
            ScreenBounds.EndY /= map.TileHeight;
            ScreenBounds.EndX += 1;
            ScreenBounds.EndY += 1;
        }

        public void Resize(GraphicsDeviceManager graphics)
        {
            var gd = Assets.GetGraphicsDevice().PresentationParameters;
            float aspectRatio = (float)gd.BackBufferWidth / (float)gd.BackBufferHeight;
            Camera.Width = Camera.Height * aspectRatio;
            Assets.ResizeRenderTargets2D("LightsTarget");
            Assets.ResizeRenderTargets2D("MainTarget");
            Utils.Logger.WriteLine(WarningLevel.High, aspectRatio.ToString());
            
        }
    }

    public struct ScreenBounds
    {
        public int StartX { get; set; }
        public int StartY { get; set; }
        public int EndX { get; set; }
        public int EndY { get; set; }
    }
}