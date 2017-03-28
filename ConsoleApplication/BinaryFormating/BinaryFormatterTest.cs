using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication.BinaryFormating
{
    public class BinaryFormatterTest
    {
        public static void Test()
        {
            string fileName = "test2.bin";
            //string fileName = "test3.bin";
            string path =
                @"C:\Users\srdjan.tubin\AppData\Local\ConsoleApplication\ConsoleApplication\1.0.0.0\" + fileName;

            //SerializableActivityState sActivityState = new SerializableActivityState(1, "a", "b", true);
            //sActivityState.PointsList = new List<SimplePoint>
            //{
            //    new SimplePoint(0, 0),
            //    new SimplePoint(1,1)
            //};
            
            //IFormatter binaryFormatter1 = new BinaryFormatter();
            //FileStream fileStream = new FileStream(path, FileMode.Create);
            //binaryFormatter1.Serialize(fileStream, sActivityState);
            //fileStream.Close();

            IFormatter binaryFormatter2 = new BinaryFormatter();
            //binaryFormatter2.Binder = new CustomSerializationBinder();
            FileStream fileStreamOpen = new FileStream(path, FileMode.Open);
            SerializableActivityState sActivityStateDeserialized = (SerializableActivityState)binaryFormatter2.Deserialize(fileStreamOpen);
        }
    }

    /// <summary>
    /// In case the class name, assembly version etc. changes, use custom SerializationBinder to return the correct type
    /// </summary>
    internal sealed class CustomSerializationBinder : SerializationBinder
    {
        public override Type BindToType(String assemblyName, String typeName)
        {
            return typeof(SerializableActivityState);
        }
    }
}
