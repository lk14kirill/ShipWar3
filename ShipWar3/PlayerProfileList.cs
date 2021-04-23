using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Xml.Linq;

namespace ShipWar3
{
    public class PlayerProfileList
    {
        public  List<PlayerProfile> profiles = new List<PlayerProfile>();
        private string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)+ "\\Profiles.xml";
        int a = 34;

        public PlayerProfile Create(string name, string login, string password)
        {
            PlayerProfile profile = new PlayerProfile(name, PlayerType.human, null, 0, login, password);
            profiles.Add(profile);
            return profile;
        }
        public PlayerProfile? CheckProfileOnExistance(string login, string password)
        {
            foreach (PlayerProfile profile in profiles)
            {
                if (profile.login == login && profile.password == password)
                {
                    return profile;
                }
            }
            return null;
        }
        public void ChangeWinsOfProfile (string login, int wins)
        {
            int i = 0;
            foreach (PlayerProfile profile in profiles)
            {
                if (profile.login == login)
                {
                    PlayerProfile tempProfile = profile;
                    tempProfile.wins += wins;
                    profiles.RemoveAt(i);
                    profiles.Insert(i, tempProfile);
                    return;
                }
                i++;
            }
        }
        public bool IsLoginFree(string login)
        {
            foreach (PlayerProfile profile in profiles)
            {
                if (profile.login == login)
                {
                    return false;
                }
            }
            return true;
        }
        public void Save()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<PlayerProfile>));
            FileStream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
            serializer.Serialize(stream, profiles);
            stream.Close();
        }
        public void Load()
        {
            CheckFileForExistance();
            XmlSerializer serializer = new XmlSerializer(typeof(List<PlayerProfile>));
            FileStream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None);
            profiles = serializer.Deserialize(stream) as List<PlayerProfile>;
            stream.Close();
        }
        public void CheckFileForExistance()
        {
            if (!File.Exists(path))
            {
                PlayerProfile profile = new PlayerProfile();
                profiles.Add(profile);
                Save();
            }
        }
    }
}
