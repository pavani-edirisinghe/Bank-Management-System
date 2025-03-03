using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA_Project
{
    class SearchAccount:Project
    {
        public static void searchAcc()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.Clear();

            string text = "-------------Search Account-------------";
            int windowWidth = Console.WindowWidth;
            int textWidth = text.Length;
            int spaces = (windowWidth - textWidth) / 2;
            Console.SetCursorPosition(spaces, Console.CursorTop);
            Console.WriteLine(text);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            string[] items = new string[]
            {
                "1. Account List",
                "2. Search Account",
                "3. Back"
            };
            int consoleWidth = Console.WindowWidth;
            int maxItemLength = 0;

            foreach (var item in items)
            {
                if (item.Length > maxItemLength)
                {
                    maxItemLength = item.Length;
                }
            }
            int leadingSpaces = (consoleWidth - maxItemLength) / 2;

            foreach (var item in items)
            {
                Console.SetCursorPosition(leadingSpaces, Console.CursorTop);
                Console.WriteLine(item);
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
