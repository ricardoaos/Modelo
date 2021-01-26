using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FiscalPrime.Backend.Http.HttpUtils
{
    public static class HttpClientUtils<T>
    {
        public static async Task<T> GetTAsync(string url, string token)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var result = await httpClient.GetAsync(url);

                var resultContent = result.Content.ReadAsStringAsync().Result;

                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(resultContent);
            }
        }

        public static async Task<T> PostTAsync(string url, object data, string token)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                var result = await httpClient.PostAsync(url, stringContent);

                var resultContent = result.Content.ReadAsStringAsync().Result;

                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(resultContent);
            }
        }

        public static async Task<T> PutTAsync(string url, object data, string token)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                var result = await httpClient.PutAsync(url, stringContent);

                var resultContent = result.Content.ReadAsStringAsync().Result;

                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(resultContent);
            }
        }
    }
}
