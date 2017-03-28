using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication.BinaryFormating
{
    public class ActivityState
    {
        internal int ANumber { get; set; }
        internal string AString1 { get; set; }
        internal string AString2 { get; set; }    

        public ActivityState()
        {
            ANumber = 0;
            AString1 = "n/a";
            AString2 = "n/a";
        }

        public ActivityState(int aNumber, string aString1, string aString2)
        {
            ANumber = aNumber;
            AString1 = aString1;
            AString2 = aString2;
        }

        internal static void SerializeToStream(Stream stream, ActivityState activity)
        {
            var writer = new BinaryWriter(stream);
            writer.Write(activity.ANumber);
            writer.Write(activity.AString1);
            writer.Write(activity.AString2);
        }

        // does not close stream received. Caller is responsible to close if it wants it
        internal static ActivityState DeserializeFromStream(Stream stream)
        {
            var reader = new BinaryReader(stream);            
            return new ActivityState
            {
                ANumber = reader.ReadInt32(),
                AString1 =  reader.ReadString(),
                AString2 =  reader.ReadString()
            };
        }
    }
}