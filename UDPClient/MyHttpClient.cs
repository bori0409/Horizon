using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace UDPClient
{
    class MyHttpClient
    {
        public async Task<List<MotionsModel>> PostItemHttpTask()
        {
            string MotionWebApi = "http://motionvab.azurewebsites.net";
            MotionsModel mymotion = new MotionsModel();

            using (HttpClient client = new HttpClient())
            {
                string stringmomtion = JsonConvert.SerializeObject(mymotion);
                var content = new StringContent(stringmomtion, Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(MotionWebApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.PostAsync("/api/motion", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseEvent = client.GetAsync("/api/motion" + mymotion).Result;
                    if (responseEvent.IsSuccessStatusCode)
                    {
                        // var Event = responseEvent.Content.ReadAsStreamAsync<Event>().Result;
                        //   string saveEvent = await responseEvent.Content.ReadAsStringAsync<Event>().Result;

                    }
                }
            }

            return await GetItemsHttpTask();
        }

        public async Task<List<MotionsModel>> GetItemsHttpTask()
        {
            string WebApi = "http://motionvab.azurewebsites.net";

            using (HttpClient client = new HttpClient())
            {
                string eventsJsonString = await client.GetStringAsync(WebApi);
                if (eventsJsonString != null)
                    return (List<MotionsModel>)JsonConvert.DeserializeObject(eventsJsonString, typeof(List<MotionsModel>));
                return null;
            }
        }
    }
}
