using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;

using Model.GameEntity.EntityHealth;

namespace Model.SaveSystem
{
    public static class SaveManager
    {
        private static Type[] knownTypes;
        static SaveManager()
        {
            var types = new Type[]{typeof(ComponentSave), typeof(IHealthProperty)};
            var assemblies = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(assembly => assembly.ManifestModule.Name == "Assembly-CSharp.dll");
            knownTypes = assemblies
                .SelectMany(s => s.GetTypes())
                .Where(p => types.Any(type => type.IsAssignableFrom(p)) && !p.IsInterface)
                .ToArray();
        }
        public static void WriteObject<T>(string fileName, T obj)
        {
            var dcss = new DataContractSerializerSettings
            {
                PreserveObjectReferences = true,
                KnownTypes = knownTypes
            };
            var writer = new FileStream(fileName, FileMode.Create);
            var ser = new DataContractSerializer(typeof(T), dcss);
            ser.WriteObject(writer, obj);
            writer.Close();
        }

        public static T ReadObject<T>(string fileName)
        {
            var dcss = new DataContractSerializerSettings
            {
                PreserveObjectReferences = true,
                KnownTypes = knownTypes
            };
            var fs = new FileStream(fileName, FileMode.Open);
            var reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
            var ser = new DataContractSerializer(typeof(T), dcss);

            var deserialized = (T) ser.ReadObject(reader, true);
            reader.Close();
            fs.Close();
            return deserialized;
        }
    }
}