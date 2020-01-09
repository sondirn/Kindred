using System;
using Kindred.Base;

namespace Kindred.GL
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new KindredMain())
                game.Run();
        }
    }
}
