using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA_Project
{
    class ColorHelper
    {
        public static void SetColor(ConsoleColor foreground, ConsoleColor background = ConsoleColor.Black)
        {
            Console.ForegroundColor = foreground;
            Console.BackgroundColor = background;
        }

        public static void ResetColor()
        {
            Console.ResetColor();
        }
    }
}
