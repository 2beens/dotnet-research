using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Google_ProtoBuf_Example
{
    [ProtoContract]
    class Person
    {
        [ProtoMember(1)]
        public int Id { get; set; }

        [ProtoMember(2)]
        public string Name { get; set; }

        [ProtoMember(3)]
        public Address Address { get; set; }
    }

    [ProtoContract]
    class PersonDim
    {
        [ProtoMember(1)]
        public int Id { get; set; }

        [ProtoMember(3)]
        public Address Address { get; set; }
    }

    [ProtoContract]
    class PersonAdd
    {
        [ProtoMember(1)]
        public int Id { get; set; }

        [ProtoMember(2)]
        public string Name { get; set; }

        [ProtoMember(3)]
        public Address Address { get; set; }

        [ProtoMember(4)]
        public string City { get; set; }
    }

    [ProtoContract]
    class PersonAdd2
    {
        [ProtoMember(1)]
        public int Id { get; set; }

        [ProtoMember(2)]
        public string Name { get; set; }

        [ProtoMember(3)]
        public Address Address { get; set; }

        [ProtoMember(4)]
        public string City { get; set; }

        [ProtoMember(5)]
        public List<Address> Addresses { get; set; }
    }
}
