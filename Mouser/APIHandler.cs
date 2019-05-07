using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mouser
{
    class APIHandler
    {
        HttpClient client = new HttpClient();

        FileHandler fileHandler = new FileHandler();

        private string apiUrl = "https://menclto16.sps-prosek.cz/RestAPI/api.php";

        public async Task GetDataFromDB()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, apiUrl);
            var keyValues = new List<KeyValuePair<string, string>>();
            keyValues.Add(new KeyValuePair<string, string>("token", fileHandler.GetToken()));
            request.Content = new FormUrlEncodedContent(keyValues);
            var response = await client.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                fileHandler.SaveFile(fileHandler.DeserializeJsonString(responseContent));
            }
        }
        public async void SaveDataToDB()
        {
            string json = fileHandler.GetJsonString();

            var request = new HttpRequestMessage(HttpMethod.Post, apiUrl);
            var keyValues = new List<KeyValuePair<string, string>>();
            keyValues.Add(new KeyValuePair<string, string>("data", json));
            keyValues.Add(new KeyValuePair<string, string>("token", fileHandler.GetToken()));
            request.Content = new FormUrlEncodedContent(keyValues);
            var response = await client.SendAsync(request);
            string responseContent = await response.Content.ReadAsStringAsync();
        }
    }
}
