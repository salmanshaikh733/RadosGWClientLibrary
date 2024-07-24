using RadosGWClientLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RadosGWClientLibrary.src.Core.Rados
{
    public class RadosClient
    {
        
        private readonly RadosHttpClient _httpClient;
        private readonly string _user;

        public RadosClient(RadosCredentials creds, RadosConfig config, string user)
        {
            _user = user;
            _httpClient = new RadosHttpClient(creds, config);
        }

        public async Task<List<ListBucketResponse>> ListBucketsAsync()
        {
            var response = await _httpClient.GetAsync("/?format=json");
            return JsonSerializer.Deserialize<List<ListBucketResponse>>(response)!;
        }

        public async Task<string> DeleteBucketAsync(string bucketName)
        {
            var response = await _httpClient.DeleteAsync($"/{bucketName}");
            return response;
        }


    }
}
