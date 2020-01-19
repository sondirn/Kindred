using System;
using System.Collections.Generic;
using System.Text;

namespace Kindred.Base.Utils
{
    public static class Logger
    { 
        public static void WriteLine(WarningLevel level, string message)
        {
            switch (level)
            {
                case WarningLevel.Low:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case WarningLevel.Medium:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                case WarningLevel.High:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case WarningLevel.Urgent:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void Write(WarningLevel level, string message)
        {
            switch (level)
            {
                case WarningLevel.Low:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case WarningLevel.Medium:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                case WarningLevel.High:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case WarningLevel.Urgent:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }
            Console.Write(message);
            Console.ResetColor();
        }
    }

    public enum WarningLevel 
    {
        Low,
        Medium,
        High,
        Urgent,
    }

}
