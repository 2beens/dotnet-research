using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Google_ProtoBuf_Example
{
    [ProtoContract]
    class Address
    {
        [ProtoMember(1)]
        public string Line1 { get; set; }

        [ProtoMember(2)]
        public string Line2 { get; set; }
    }
}
