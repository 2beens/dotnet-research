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

            SimpleDataHolder deserializeDataHolder = Serializer.Deserialize<SimpleDataHolder>(simpleDataHolderBytes);
            Console.WriteLine("Simple Data Holder deserialized ? " + (simpleDataHolderBytes != null));
            Console.ReadKey();
        }
    }
}
