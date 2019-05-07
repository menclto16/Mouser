using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mouser
{
    class FileHandler
    {
        public void SaveFile(List<MouseProfile> mouseProfiles)
        {
            string json = JsonConvert.SerializeObject(mouseProfiles);

            File.WriteAllText("mouseProfiles.json", json);
        }

        public List<MouseProfile> ReadFile()
        {
            if (File.Exists("mouseProfiles.json"))
            {
                string jsonFromFile = File.ReadAllText("mouseProfiles.json");

                return JsonConvert.DeserializeObject<List<MouseProfile>>(jsonFromFile);
            }
            else return new List<MouseProfile>();
        }

        public List<MouseProfile> DeserializeJsonString(string json)
        {
            return JsonConvert.DeserializeObject<List<MouseProfile>>(json);
        }

        public string GetJsonString()
        {
            if(File.Exists("mouseProfiles.json"))
            {
                string jsonFromFile = File.ReadAllText("mouseProfiles.json");

                return jsonFromFile;
            }
            else return "";
        }

        public string GetToken()
        {
            if (File.Exists("applicationToken.cfg"))
            {
                string jsonFromFile = File.ReadAllText("applicationToken.cfg");

                return jsonFromFile;
            }
            else return "";
        }
    }
}
