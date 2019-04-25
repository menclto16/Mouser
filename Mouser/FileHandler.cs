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
        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };

        private List<MouseProfile> mouseProfiles = new List<MouseProfile>();

        public FileHandler()
        {
            if (File.Exists("mouseProfiles.json"))
            {
                string jsonFromFile = File.ReadAllText("mouseProfiles.json");

                mouseProfiles = JsonConvert.DeserializeObject<List<MouseProfile>>(jsonFromFile, settings);
            }
        }

        private void saveFile()
        {
            string json = JsonConvert.SerializeObject(mouseProfiles, settings);

            File.WriteAllText("mouseProfiles.json", json);
        }
    }
}
