using ConsoleApplication.CustomConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication.BinaryFormating;

namespace ConsoleApplication
{
    class Program
    {
        private class SimplePoint
        {
            public int X { get; set; }
            public int Y { get; set; }

            public SimplePoint()
            {
                X = 0;
                Y = 0;
            }

            public override string ToString()
            {
                return string.Format("[{0}:{1}]", X, Y);
            }
        }

        static void Main(string[] args)
        {
            //CustomConsoleExample();

            var encoded = GetCodePoint('S');

            BinaryFormatterTest.Test();

            Console.ReadKey();

            ActivityHandler activityHandler = new ActivityHandler();
            activityHandler.WriteActivityStateInternal();

            activityHandler.ReadActivityState();

            Console.ReadKey();

            int intVal1 = 1;
            int intVal2 = 2;
            Console.WriteLine("Integers before regular swap method: {0} & {1}", intVal1, intVal2);
            SwapIntegers(intVal1, intVal2);
            Console.WriteLine("Integers after regular swap method: {0} & {1}", intVal1, intVal2);
            SwapIntegersByRef(ref intVal1, ref intVal2);
            Console.WriteLine("Integers after ref swap method: {0} & {1}", intVal1, intVal2);
            Console.ReadKey();

            PrintRedLine();

            SimplePoint sp1 = new SimplePoint();
            SimplePoint sp2 = new SimplePoint();
            Console.WriteLine("Points before swap method: {0} & {1}", sp1, sp2);
            SwapPoints(sp1, sp2);
            Console.WriteLine("Points after swap method: {0} & {1}", sp1, sp2);
            SwapPoints(ref sp1, ref sp2);
            Console.WriteLine("Points after ref swap method: {0} & {1}", sp1, sp2);

            PrintRedLine();

            Type intListType = typeof(List<int>);
            Type stringListType = typeof(List<string>);

            Console.WriteLine("intList type = " + intListType.ToString());
            Console.WriteLine("stringList type = " + stringListType.FullName);

            Console.WriteLine();
            Console.WriteLine("Press any key to quit...");
            Console.ReadKey();
        }

        private static void PrintRedLine()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine("----------------------------------------------------");
            Console.ResetColor();
        }

        private static void SwapIntegers(int intVal1, int intVal2)
        {
            int temp = intVal1;
            intVal1 = intVal2;
            intVal2 = temp;
        }

        private static void SwapIntegersByRef(ref int intVal1, ref int intVal2)
        {
            int temp = intVal1;
            intVal1 = intVal2;
            intVal2 = temp;
        }

        private static void SwapPoints(SimplePoint sp1, SimplePoint sp2)
        {
            sp1.X = 100;
            sp1.Y = 100;
            SimplePoint temp = sp1;
            sp1 = sp2;
            sp2 = temp;
        }

        private static void SwapPoints(ref SimplePoint sp1, ref SimplePoint sp2)
        {
            sp1.X = 100;
            sp1.Y = 100;
            SimplePoint temp = sp1;
            sp1 = sp2;
            sp2 = temp;
        }

        private static void CustomConsoleExample()
        {
            ICustomConsole customConsole = new CustomConsole.CustomConsole();

            try
            {
                Byte b = 100;
                customConsole.WriteGreen(b);
                b = checked((Byte)(b + 200));
                Console.WriteLine(b);
            }
            catch (OverflowException oe)
            {
                customConsole.WriteRed(oe.ToString());
            }
        }

        private static string GetCodePoint(char ch)
        {
            string retVal = "u+";
            byte[] bytes = Encoding.Unicode.GetBytes(ch.ToString());
            for (int ctr = bytes.Length - 1; ctr >= 0; ctr--)
                retVal += bytes[ctr].ToString("X2");

            return retVal;
        }
    }
}
