using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

namespace ShipWar3
{
    public class XML
    {
        public void Save(List<PlayerProfile> profiles)
        {

            XmlSerializer serializer = new XmlSerializer(typeof(List<PlayerProfile>));
            FileStream stream = new FileStream("D:\\Profiles.xml", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            serializer.Serialize(stream, profiles);
            stream.Close();

        }
        public void Load(ref List<PlayerProfile> profiles)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<PlayerProfile>));
            FileStream stream = new FileStream("D:\\Profiles.xml", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None);
            profiles = serializer.Deserialize(stream) as List<PlayerProfile>;
            stream.Close();

        }
    }
}