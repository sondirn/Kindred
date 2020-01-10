using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kindred.Base.Utils
{
    public class Input
    {
        private static KeyboardStateExtended _currentKeyboardState;
        public static void Update()
        {
            _currentKeyboardState = KeyboardExtended.GetState();
        }

        public static KeyboardStateExtended GetKeyboardState()
        {
            return _currentKeyboardState;
        }

        public static bool CapsLock => _currentKeyboardState.CapsLock;
        public static bool NumLock => _currentKeyboardState.NumLock;
        public static bool IsShiftDown() => _currentKeyboardState.IsShiftDown();
        public static bool IsControlDown() => _currentKeyboardState.IsControlDown();
        public static bool IsAltDown() => _currentKeyboardState.IsAltDown();
        public static bool IsKeyDown(Keys key) => _currentKeyboardState.IsKeyDown(key);
        public static bool IsKeyUp(Keys key) => _currentKeyboardState.IsKeyUp(key);
        public static Keys[] GetPressedKeys() => _currentKeyboardState.GetPressedKeys();    
        public static bool WasKeyJustDown(Keys key) => _currentKeyboardState.WasKeyJustDown(key);
        public static bool WasKeyJustUp(Keys key) => _currentKeyboardState.WasKeyJustUp(key);
        public static bool WasAnyKeyJustDown() => _currentKeyboardState.WasAnyKeyJustDown();
        public static float GetAxis(string direction)
        {
            switch (direction)
            {
                case "Horizontal":
                    return IsKeyDown(Keys.A) ? -1 : IsKeyDown(Keys.D) ? 1 : IsKeyDown(Keys.Left) ? -1 : IsKeyDown(Keys.Right) ? 1 : 0;
                case "Vertical":
                    return IsKeyDown(Keys.W) ? -1 : IsKeyDown(Keys.S) ? 1 : IsKeyDown(Keys.Up) ? -1 : IsKeyDown(Keys.Down) ? 1 : 0;

                default:
                    return 0;
            }
        }


    }
}
