using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SerializationUsingTextEncoding
{
    public static class Serializer
    {
        private static readonly string KEY_VALUE_DELIMITER = ":";
        private static readonly string PROPERTIES_DELIMITER = "##";

        public static byte[] Serialize(object objectToSerialize)
        {
            string objectStringData = Serialize2String(objectToSerialize);
            return Encoding.Unicode.GetBytes(objectStringData);
        }

        private static string Serialize2String(object objectToSerialize)
        {
            var serializedData = string.Empty;

            var memberInfos = new HashSet<MemberInfo>();
            memberInfos.UnionWith(objectToSerialize.GetType().GetProperties());
            memberInfos.UnionWith(objectToSerialize.GetType().GetFields
                (BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public));

            foreach (var memberInfo in memberInfos)
            {
                var serializationInfoAtt = memberInfo.GetCustomAttribute(typeof(SerializationInfoAttribute));
                if (serializationInfoAtt == null)
                    continue;

                var dataKey = ((SerializationInfoAttribute) serializationInfoAtt).DataKey;
                var dataValue = memberInfo is FieldInfo
                    ? (memberInfo as FieldInfo).GetValue(objectToSerialize)
                    : (memberInfo as PropertyInfo)?.GetValue(objectToSerialize);

                // check for complex type
                if (dataValue != null && dataValue.GetType().IsClass && dataValue.GetType() != typeof(string))
                    dataValue = "{" + Serialize2String(dataValue) + "}";

                serializedData += dataKey + KEY_VALUE_DELIMITER + dataValue + PROPERTIES_DELIMITER;
            }

            if (serializedData.Length > 0)
                serializedData = serializedData.Substring(0, serializedData.Length - PROPERTIES_DELIMITER.Length);

            return serializedData;
        }

        public static T Deserialize<T>(byte[] objectData) where T : new()
        {
            var deserializedObject = new T();

            var serializedDataString = Encoding.Unicode.GetString(objectData);

            if (string.IsNullOrEmpty(serializedDataString))
                return deserializedObject;

            var key2MemberInfoMap = new Dictionary<string, MemberInfo>();
            var memberInfos = new HashSet<MemberInfo>();
            memberInfos.UnionWith(typeof(T).GetProperties());
            memberInfos.UnionWith(typeof(T).GetFields
                (BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public));
            foreach (var memberInfo in memberInfos)
            {
                var serializationInfoAtt = memberInfo.GetCustomAttribute(typeof(SerializationInfoAttribute));
                if (serializationInfoAtt == null)
                    continue;

                key2MemberInfoMap.Add(((SerializationInfoAttribute) serializationInfoAtt).DataKey, memberInfo);
            }

            var propertiesString = serializedDataString.Split(PROPERTIES_DELIMITER.ToCharArray());
            foreach (var propString in propertiesString)
            {
                if (propString.Length == 0)
                    continue;

                var propDetails = propString.Split(KEY_VALUE_DELIMITER.ToCharArray());
                if (propDetails.Length != 2)
                    continue;

                var dataKey = propDetails[0];
                var dataValue = propDetails[1];

                MemberInfo foundMemeberInfo;
                if (!key2MemberInfoMap.TryGetValue(dataKey, out foundMemeberInfo))
                    continue;

                if (foundMemeberInfo is PropertyInfo)
                {
                    var prop = (PropertyInfo) foundMemeberInfo;
                    if (prop.PropertyType == typeof(int))
                        prop.SetValue(deserializedObject, int.Parse(dataValue));
                    else if (prop.PropertyType == typeof(bool))
                        prop.SetValue(deserializedObject, bool.Parse(dataValue));
                    else
                        prop.SetValue(deserializedObject, dataValue);

                    //TODO: other types
                }
                else
                {
                    (foundMemeberInfo as FieldInfo)?.SetValue(deserializedObject, dataValue);
                }
            }

            return deserializedObject;
        }
    }
}