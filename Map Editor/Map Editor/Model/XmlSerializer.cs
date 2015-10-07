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
    /// <summary>
    /// Class serializer that can serialize any class as it has a template.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class XmlCustomSerializer<T>
    {
        string path;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlCustomSerializer{T}"/> class.
        /// </summary>
        /// <param name="_path">The _path.</param>
        public XmlCustomSerializer(string _path)
        {
            path = _path;
        }

        /// <summary>
        /// Saves the specified _object.
        /// </summary>
        /// <param name="_object">The _object.</param>
        public void Save(T _object)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            FileStream file = new FileStream(path, FileMode.Create);
            serializer.Serialize(file, _object);
            file.Close();
        }

        /// <summary>
        /// Loads this instance.
        /// </summary>
        /// <returns></returns>
        public T Load()
        {
            T result;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Scene));
                FileStream file = new FileStream(path, FileMode.Open);
                result = (T)serializer.Deserialize(file);
                file.Close();
            }
            catch (Exception)
            {
                result = default(T);
            }
            
            return result;
        }
    }
}
