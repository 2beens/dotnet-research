using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Google_ProtoBuf_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            string personFileName = "person.bin";
            string personFilePath = "C:\\Temp\\" + personFileName;

            DeserializeExample(personFilePath);
        }

        private static void SerializeExample(string personFilePath)
        {
            List<Address> addresses = new List<Address>();
            addresses.Add(new Address { Line1 = "blabla1", Line2 = "gragra1" });
            addresses.Add(new Address { Line1 = "blabla2", Line2 = "gragra2" });
            addresses.Add(new Address { Line1 = "blabla3", Line2 = "gragra3" });

            PersonAdd2 p1 = new PersonAdd2
            {
                Id = 0,
                Address = new Address
                {
                    Line1 = "line11",
                    Line2 = "line12"
                },
                Addresses = addresses,
                Name = "Name1"
            };

            using (var personFile = File.Create(personFilePath))
            {
                Serializer.Serialize(personFile, p1);
            }
        }

        private static void DeserializeExample(string personFilePath)
        {
            PersonAdd2 deserializedPerson;
            using (var personFile = File.OpenRead(personFilePath))
            {
                deserializedPerson = Serializer.Deserialize<PersonAdd2>(personFile);
            }

            Console.WriteLine("Person read ? " + (deserializedPerson != null));
            Console.ReadKey();
        }

        private static void SerializeAndDeserializeExample(string personFilePath)
        {
            Person p1 = new Person
            {
                Id = 0,
                Address = new Address
                {
                    Line1 = "line11",
                    Line2 = "line12"
                },
                Name = "Name1"
            };

            using (var personFile = File.Create(personFilePath))
            {
                Serializer.Serialize(personFile, p1);
            }

            Person deserializedPerson;
            using (var personFile = File.OpenRead(personFilePath))
            {
                deserializedPerson = Serializer.Deserialize<Person>(personFile);
            }

            Console.WriteLine("Person read ? " + (deserializedPerson != null));
            Console.ReadKey();
        }
    }
}
