using Map_Editor.GameData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Map_Editor
{
    public class XmlCustomSerializer<T>
    {
        string path;
        public XmlCustomSerializer(string _path)
        {
            path = _path;
        }

        public void Save(T _object)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            FileStream file = new FileStream(path, FileMode.Create);
            serializer.Serialize(file, _object);
            file.Close();
        }

        public T Load()
        {
            T result;
            XmlSerializer serializer = new XmlSerializer(typeof(Scene));
            FileStream file = new FileStream(path, FileMode.Open);
            result = (T)serializer.Deserialize(file);
            file.Close();
            return result;
        }
    }
}
