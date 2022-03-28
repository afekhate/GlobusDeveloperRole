using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http.Json;
using System.Threading;
using Microsoft.Extensions.Configuration;
using RestSharp;
using Newtonsoft.Json;



namespace Globus.Utilities.Common
{
    public class MiddleWare
    {

        public static async Task<string> GetAsync(string ApiCall, string token, bool addToken)
        {

            string buildurl = "Baseurl" + ApiCall;

            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri("Baseurl");
                client.DefaultRequestHeaders.Clear();

                if (addToken == true)
                {
                    client.DefaultRequestHeaders.Add("Authorization", token);
                }
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage resultRespone = await client.GetAsync(ApiCall);

                using (var response = await client
                                                            .GetAsync(buildurl)
                                                            .ConfigureAwait(false))
                {
                    var data = await response.Content.ReadAsStringAsync();

                    return data;
                }
            }
        }



        public static async Task<string> PostBasicAsync(object content, CancellationToken cancellationToken, string token, string apiurl, bool addToken = true)
        {
            string buildurl = "Baseurl" + apiurl;


            var client = new HttpClient();


            using (var request = new HttpRequestMessage(HttpMethod.Post, buildurl))
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(content);


                if (addToken == true)
                {

                    client.DefaultRequestHeaders.Add("token", token);
                    client.DefaultRequestHeaders.Add("Authorization", token);

                }

                using (var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    request.Content = stringContent;

                    using (var response = await client
                                       .PostAsync(buildurl, stringContent)
                                       .ConfigureAwait(false))
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        return data;
                    }

                }
            }
        }

        public static async Task<string> PostBasicAsync(object content, CancellationToken cancellationToken, string apiurl)
        {

            string buildurl = "Baseurl" + apiurl;

            var client = new HttpClient();

            using (var request = new HttpRequestMessage(HttpMethod.Post, buildurl))
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(content);



                using (var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    request.Content = stringContent;

                    using (var response = await client
                                                             .PostAsync(buildurl, stringContent)
                                                             .ConfigureAwait(false))
                    {

                        var data = await response.Content.ReadAsStringAsync();

                        return data;
                    }

                }
            }
        }







        public static async Task<IRestResponse> IRestPostAsync(object payload)
        {


            try
            {



                string url = "";
                string payloadtoJson = JsonConvert.SerializeObject(payload);

                var client = new RestClient(url);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);

                request.AddHeader("Content-Type", "application/json");

                request.AddHeader("hashKey", "");


                request.AddParameter("application/json", payloadtoJson, ParameterType.RequestBody);
                IRestResponse responsetask = client.Execute(request);
                return responsetask;


            }


            catch (Exception ex)
            {
                return null;
            }

        }


        public static async Task<IRestResponse> PostRapidAsync()
        {
            var client = new RestClient("https://gold-price-live.p.rapidapi.com/get_metal_prices");
            var request = new RestRequest(Method.GET);
            request.AddHeader("X-RapidAPI-Host", "gold-price-live.p.rapidapi.com");
            request.AddHeader("X-RapidAPI-Key", "a7484a871amsh90666940db0ec0fp116fb3jsn358692fc7db2");
            IRestResponse response = client.Execute(request);
            return response;
        }


        

    }
}