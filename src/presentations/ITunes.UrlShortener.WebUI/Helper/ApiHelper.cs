using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ITunes.UrlShortener.WebUI.Helper
{
    public interface IApiHelper<T>
    {
        public Task<T?> Get(string uri, bool useToken = true, Dictionary<string, string> headers = null);

        public Task<T?> Post<TK>(string uri, TK request,
            bool useToken = true, Dictionary<string, string> headers = null);
    }

    public class ApiHelper<T> : IApiHelper<T> where T : class
    {
        private readonly HttpClient _httpClient;
        private readonly ISecurityTokenHandler _securityTokenHandler;

        public ApiHelper(HttpClient httpClient, ISecurityTokenHandler securityTokenHandler)
        {
            _httpClient = httpClient;
            _securityTokenHandler = securityTokenHandler;
        }

        public async Task<T?> Get(string uri, bool useToken = true, Dictionary<string, string> headers = null)
        {
            if (headers != null)
            {
                foreach (KeyValuePair<string, string> header in headers)
                {
                    _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            if (useToken)
            {
                var token = await _securityTokenHandler.GetToken();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.GetAsync(uri);
            await _securityTokenHandler.CheckUnauthorizeStatus(response.StatusCode);
            var responseObject = JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);

            return responseObject;
        }

        public async Task<T?> Post<TK>(string uri, TK request,
            bool useToken = true, Dictionary<string, string> headers = null)
        {
            if (headers != null)
            {
                foreach (KeyValuePair<string, string> header in headers)
                {
                    _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            if (useToken)
            {
                var token = await _securityTokenHandler.GetToken();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var json = JsonConvert.SerializeObject(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, data);
            await _securityTokenHandler.CheckUnauthorizeStatus(response.StatusCode);
            var responseObject = JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);

            return responseObject;
        }
    }
}