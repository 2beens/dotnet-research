using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication.CustomConsole
{
    public class CustomConsole : ICustomConsole
    {
        public void WriteGreen(object text)
        {
            WriteInColor(ConsoleColor.Green, text);
        }

        public void WriteRed(object text)
        {
            WriteInColor(ConsoleColor.Red, text);
        }

        private void WriteInColor(ConsoleColor color, object text)
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = originalColor;
        }
    }
}
