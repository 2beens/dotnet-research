using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerializationUsingTextEncoding
{
    class Program
    {
        static void Main(string[] args)
        {
            SimpleDataHolder simpleDataHolder1 = new SimpleDataHolder {ANumber = 1, AString = "str1", ABool = true};
            byte[] simpleDataHolderBytes = Serializer.Serialize(simpleDataHolder1);

            //TODO: write object byte array to a file
            // File.WriteAllBytes(string path, byte[] simpleDataHolderBytes)

            // deserialize the object from the original bytes
            SimpleDataHolder deserializeDataHolder = Serializer.Deserialize<SimpleDataHolder>(simpleDataHolderBytes);
            Console.WriteLine("Simple Data Holder deserialized ? " + (deserializeDataHolder != null));
            Console.ReadKey();

            // deserialize the object from the bytes from the old version
            SimpleDataHolder2 deserializeDataHolder2 = Serializer.Deserialize<SimpleDataHolder2>(simpleDataHolderBytes);
            Console.WriteLine("Simple Data Holder 2 deserialized ? " + (deserializeDataHolder != null));
            Console.ReadKey();
        }
    }
}
