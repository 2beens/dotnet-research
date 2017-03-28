using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SerializationUsingTextEncoding
{
    public static class Serializer
    {
        private static readonly string KEY_VALUE_DELIMITER = ":";
        private static readonly string PROPERTIES_DELIMITER = "##";

        public static byte[] Serialize(object objectToSerialize)
        {
            string serializedData = String.Empty;

            HashSet<MemberInfo> memberInfos = new HashSet<MemberInfo>();
            memberInfos.UnionWith(objectToSerialize.GetType().GetProperties());
            memberInfos.UnionWith(objectToSerialize.GetType().GetFields
                (BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public));

            foreach (var memberInfo in memberInfos)
            {
                var serializationInfoAtt = memberInfo.GetCustomAttribute(typeof(SerializationInfoAttribute));
                if (serializationInfoAtt == null)
                    continue;

                var dataKey = ((SerializationInfoAttribute) serializationInfoAtt).DataKey;
                var dataValue = (memberInfo is FieldInfo)
                    ? (memberInfo as FieldInfo).GetValue(objectToSerialize)
                    : (memberInfo as PropertyInfo)?.GetValue(objectToSerialize);

                serializedData += dataKey + KEY_VALUE_DELIMITER + dataValue + PROPERTIES_DELIMITER;
            }
            
            if (serializedData.Length > 0)
                serializedData = serializedData.Substring(0, serializedData.Length - PROPERTIES_DELIMITER.Length);

            return Encoding.Unicode.GetBytes(serializedData);
        }

        public static T Deserialize<T>(byte[] objectData) where T : new()
        {
            T deserializedObject = new T();



            return deserializedObject;
        }
    }
}
