using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mouser
{
    class APIHandler
    {
        HttpClient client = new HttpClient();

        FileHandler fileHandler = new FileHandler();

        public async void GET()
        {
            var response = await client.GetAsync("https://student.sps-prosek.cz/~menclto16/php/RestAPI/api.php");
        }
        public async void POST(string json)
        {
            json = fileHandler.GetJsonString();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://student.sps-prosek.cz/~menclto16/php/RestAPI/api.php");
            var keyValues = new List<KeyValuePair<string, string>>();
            keyValues.Add(new KeyValuePair<string, string>("data", json));
            request.Content = new FormUrlEncodedContent(keyValues);
            await client.SendAsync(request);
        }
    }
}
