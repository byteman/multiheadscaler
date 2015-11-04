using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Reflection;

namespace Monitor
{
    public class ConfigManage
    {
        public ConfigManage()
        {
            FilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName);
            FileDir = FilePath;
            FilePath += @"\config.xml";
        }

        //此函数在PC机运行
        public void Serialize()
        {
            Config cfg = new Config();

            XmlSerializer serializer = new XmlSerializer(typeof(Config));
            TextWriter writer = new StreamWriter(FilePath);
            serializer.Serialize(writer, cfg);
            writer.Close();
        }

        public bool Deserialize()
        {
            //if(!File.Exists("\\Temp\\config.xml")) return false;
            if (!File.Exists(FilePath)) return false;
            XmlSerializer serializer = new XmlSerializer(typeof(Config));
            FileStream stream = new FileStream(FilePath, FileMode.Open);
            _cfg = (Config)serializer.Deserialize(stream);
            stream.Close();
            return true;
        }

        public Config cfg
        {
            get { return _cfg;}
        }

        public string FileDir;
        private readonly string FilePath;
        private Config _cfg = null;
    }
}
