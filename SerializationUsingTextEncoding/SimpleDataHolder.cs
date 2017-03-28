using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SerializationUsingTextEncoding
{
    internal class SimpleDataHolder
    {
        [SerializationInfo("num1")]
        public int ANumber { get; set; }
        [SerializationInfo("str1")]
        public string AString { get; set; }
        [SerializationInfo("bool1")]
        public bool ABool { get; set; }

        [SerializationInfo("str2")]
        private readonly string _aString2;

        public SimpleDataHolder()
        {
            _aString2 = "str2";
        }

        public string GetAString2()
        {
            return _aString2;           
        }
    }
}
