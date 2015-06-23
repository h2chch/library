using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EtawaJaya.Common
{
    public static class Serializer
    {
        public static T JsonToObject<T>(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                throw new ArgumentException("missing json input");
            }
            return JsonConvert.DeserializeObject<T>(json, new StringEnumConverter());
        }

        public static string ObjectToJson<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, new StringEnumConverter());
        }

        public static void ObjectToJson<T>(T obj, Stream stream)
        {
            if (obj == null)
            {
                throw new ArgumentException("missing object input");
            }
            var serializer = new DataContractJsonSerializer(typeof(T));
            serializer.WriteObject(stream, obj);
            stream.Flush();
        }

        public static T XmlToObject<T>(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                throw new ArgumentException("missing xml input");
            }
            var serializer = new DataContractSerializer(typeof(T));
            return (T)serializer.ReadObject(XmlReader.Create(new StringReader(xml)));
        }

        public static byte[] ObjectToXmlByte<T>(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentException("missing object input");
            }

            var serializer = new DataContractSerializer(typeof(T));
            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, obj);
                stream.Flush();
                var output = stream.ToArray();
                return output;
            }
        }

        public static string ObjectToXml<T>(T obj)
        {
            var array = ObjectToXmlByte(obj);
            return Encoding.UTF8.GetString(array, 0, array.Length);
        }

        public static void ObjectToXml<T>(T obj, Stream stream)
        {
            if (obj == null)
            {
                throw new ArgumentException("missing object input");
            }

            var serializer = new DataContractSerializer(typeof(T));
            serializer.WriteObject(stream, obj);
            stream.Flush();
        }

    }
}
