using RadosGWClientLibrary.src.Utils;
using System.Net.Http.Headers;

namespace RadosGWClientLibrary.src.Core.Rados
{
    public class RadosHttpClient
    {
        private readonly string _accessKey;
        private readonly string _secretKey;
        private readonly string _endpoint;
        private readonly HttpClient _client= new HttpClient();
        private readonly S3SignatureHelper _s3SignatureHelper;

        public RadosHttpClient(RadosCredentials creds, RadosConfig config)
        {
            _accessKey = creds.AccessKey;
            _secretKey = creds.SecretKey;
            _endpoint = config.endpoint;

            _client.BaseAddress = new Uri(_endpoint);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("AWS4-HMAC-SHA256", $"{_accessKey}:{_secretKey}");
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _s3SignatureHelper = new S3SignatureHelper(_secretKey, _accessKey);
        }

        public async Task<string> GetAsync(string uri)
        {
            var response = await _client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();  
        }

        public async Task<string> PostAsync(string uri, HttpContent content)
        {
            var response = await _client.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> PutAsync(string uri, HttpContent content)
        {
            var response = await _client.PutAsync(uri, content); 
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> DeleteAsync(string uri)
        {
            var response = await _client.DeleteAsync(uri);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }



       /* /// <summary>
        /// List all buckets from endpoint
        /// </summary>
        /// <returns></returns>
        public async Task<string> ListBucketsAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/");
            // TODO remove if not needed.
            // signature helper may not be needed 
            // _s3SignatureHelper.AddS3headers(request, HttpMethod.Get.ToString(), "/");

            var response = await _client.SendAsync(request);
            
            return await response.Content.ReadAsStringAsync();  
        }*/



    }
}
