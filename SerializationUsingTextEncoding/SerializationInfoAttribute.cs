using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerializationUsingTextEncoding
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    internal class SerializationInfoAttribute : Attribute
    {
        public string DataKey { get; set; }

        public SerializationInfoAttribute(string dataKey)
        {
            DataKey = dataKey;
        }
    }
}
