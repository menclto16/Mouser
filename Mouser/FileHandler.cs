﻿using Newtonsoft.Json;
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

        public void SaveFile(List<MouseProfile> mouseProfiles)
        {
            string json = JsonConvert.SerializeObject(mouseProfiles, settings);

            File.WriteAllText("mouseProfiles.json", json);
        }

        public List<MouseProfile> ReadFile()
        {
            if (File.Exists("mouseProfiles.json"))
            {
                string jsonFromFile = File.ReadAllText("mouseProfiles.json");

                return JsonConvert.DeserializeObject<List<MouseProfile>>(jsonFromFile, settings);
            }
            else return new List<MouseProfile>();
        }
    }
}
