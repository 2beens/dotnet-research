using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication.BinaryFormating
{
    [Serializable]
    public class SerializableActivityState : ISerializable
    {
        internal int ANumber { get; set; }
        internal string AString1 { get; set; }
        internal string AString2 { get; set; }
        internal bool ABool { get; set; }
        //internal List<SimplePoint> PointsList { get; set; }

        // field which is not serialized/deserialized
        internal int ACalculatedNum { get; set; }

        public SerializableActivityState()
        {
            ANumber = 0;
            AString1 = "n/a";
            AString2 = "n/a";
            ABool = true;
            //PointsList = new List<SimplePoint>();
            ACalculatedNum = ANumber * 2;
        }

        // The special constructor is used to deserialize values.
        public SerializableActivityState(SerializationInfo info, StreamingContext context)
        {
            ANumber = info.GetInt32("num");
            AString1 = info.GetString("str1");
            AString2 = info.GetString("str2");

            bool returnedBoolValue;
            if (TryGetValueFromSerializationInfo<bool>(info, "bool", out returnedBoolValue))
                ABool = returnedBoolValue;
            else
                ABool = true;

            ACalculatedNum = ANumber * 2;

            //List<SimplePoint> returnedPoints;
            //if (TryGetValueFromSerializationInfo<List<SimplePoint>>(info, "points", out returnedPoints))
            //    PointsList = returnedPoints;
            //else
            //    PointsList = new List<SimplePoint>();

            //if((PointsList = GetValueOrDefaultValueFromSerializationInfo<List<SimplePoint>>(info, "points")) == null)
            //    PointsList = new List<SimplePoint>();
        }

        private T GetValueOrDefaultValueFromSerializationInfo<T>(SerializationInfo info, string fieldName)
        {
            T fieldValue = default(T);
            try
            {
                fieldValue = (T)info.GetValue(fieldName, typeof(T));
            }
            catch (SerializationException)
            {
            }
            return fieldValue;
        }
        
        private bool TryGetValueFromSerializationInfo<T>(SerializationInfo info, string fieldName,
            out T fieldValue)
        {
            fieldValue = default(T);
            bool success = true;

            try
            {
                fieldValue = (T)info.GetValue(fieldName, typeof(T));
            }
            catch (SerializationException)
            {
                success = false;
            }

            return success;
        }

        public SerializableActivityState(int aNumber, string aString1, string aString2, bool aBool)
        {
            ANumber = aNumber;
            AString1 = aString1;
            AString2 = aString2;
            ABool = aBool;
            //PointsList = new List<SimplePoint>();
            ACalculatedNum = ANumber * 2;
        }

        // other code could call GetObjectData, which might then return potentially sensitive information, 
        // or other code could construct an object that passes in corrupt data
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("num", ANumber);
            info.AddValue("str1", AString1);
            info.AddValue("str2", AString2);
            info.AddValue("bool", ABool);
            //info.AddValue("points", PointsList);
        }
    }
}
