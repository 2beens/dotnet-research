using System;
using System.Runtime.InteropServices;

namespace ConsoleApplication
{
    [StructLayout(LayoutKind.Auto)]
    [Serializable]
    public struct SimplePoint2
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public SimplePoint2(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
