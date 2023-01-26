using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace Model.SaveSystem
{
    public class SaveManager
    {
        public static void WriteObject<T>(string fileName, T obj)
        {
            var dcss = new DataContractSerializerSettings
            {
                PreserveObjectReferences = true
            };
            var writer = new FileStream(fileName, FileMode.Create);
            var ser = new DataContractSerializer(typeof(T), dcss);
            ser.WriteObject(writer, obj);
            writer.Close();
        }

        public static T ReadObject<T>(string fileName)
        {
            var fs = new FileStream(fileName, FileMode.Open);
            var reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
            var ser = new DataContractSerializer(typeof(T));

            var deserialized = (T) ser.ReadObject(reader, true);
            reader.Close();
            fs.Close();
            return deserialized;
        }
    }
}