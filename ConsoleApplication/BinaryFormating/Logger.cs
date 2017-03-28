using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApplication.BinaryFormating
{
    public class Logger
    {
        public static void Debug(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
            Console.WriteLine("D: " + message);
        }

        public static void Error(string message)
        {
            System.Diagnostics.Debug.WriteLine("ERROR:");
            System.Diagnostics.Debug.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("E: " + message);
            Console.ResetColor();
        }

        public static void Verbose(string message)
        {
            System.Diagnostics.Debug.WriteLine("Verbose:");
            System.Diagnostics.Debug.WriteLine(message);
            Console.WriteLine("V: " + message);
        }
    }
}
