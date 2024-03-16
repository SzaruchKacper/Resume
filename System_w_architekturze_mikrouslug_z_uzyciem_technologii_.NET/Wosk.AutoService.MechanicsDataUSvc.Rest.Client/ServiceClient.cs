using System.Diagnostics;
using System.Text.Json;

namespace Wosk.AutoService.MechanicsDataUSvc.Rest.Client
{
    public class ServiceClient
    {
        private static readonly HttpClient httpClient = new HttpClient();

        private readonly string serviceHost;
        private readonly ushort servicePort;

        public ServiceClient(string serviceHost, int servicePort)
        {
            Debug.Assert(condition: !String.IsNullOrEmpty(serviceHost) && servicePort > 0);

            this.serviceHost = serviceHost;
            this.servicePort = (ushort)servicePort;
        }

        public R CallWebService<R>(HttpMethod httpMethod, string webServiceUri)
        {
            Task<string> webServiceCall = this.CallWebService(httpMethod, webServiceUri);

            webServiceCall.Wait();

            string jsonResponseContent = webServiceCall.Result;

            R result = this.ConvertJson<R>(jsonResponseContent);

            return result;
        }


        public async Task<string> CallWebService(HttpMethod httpMethod, string callUri)
        {
            string httpUri = String.Format("http://{0}:{1}/{2}", this.serviceHost, this.servicePort, callUri);

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(httpMethod, httpUri);

            httpRequestMessage.Headers.Add("Accept", "application/json");

            HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            httpResponseMessage.EnsureSuccessStatusCode();

            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync();

            return httpResponseContent;
        }

        public async Task<R> CallWebServiceAsync<R>(HttpMethod httpMethod, string webServiceUri)
        {
            string jsonResponseContent = await this.CallWebService(httpMethod, webServiceUri);
            R result = this.ConvertJson<R>(jsonResponseContent);

            return result;
        }


        public async Task<string> CallWebPostService(string callUri, StringContent jsonContent)
        {
            string httpUri = String.Format("http://{0}:{1}/{2}", this.serviceHost, this.servicePort, callUri);

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, httpUri);

            httpRequestMessage.Headers.Add("Accept", "application/json"); 

            httpRequestMessage.Content = jsonContent;

            HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            httpResponseMessage.EnsureSuccessStatusCode();

            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync();

            return httpResponseContent;
        }

        public T ConvertJson<T>(string json)
        {
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();

            jsonSerializerOptions.PropertyNameCaseInsensitive = true;

            return JsonSerializer.Deserialize<T>(json, jsonSerializerOptions);
        }
    }
}