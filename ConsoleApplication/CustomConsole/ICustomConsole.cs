using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication.CustomConsole
{
    public interface ICustomConsole
    {
        void WriteRed(object text);
        void WriteGreen(object text);
    }
}
