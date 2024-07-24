using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RadosGWClientLibrary.src.Utils
{
    public class S3SignatureHelper
    {
        private readonly string _secretKey;
        private readonly string _accessKey;

        public S3SignatureHelper(string secretKey, string accessKey)
        {
            _secretKey = secretKey ?? throw new ArgumentNullException(nameof(secretKey));
            _accessKey = accessKey ?? throw new ArgumentNullException(nameof(accessKey));
        }



        /// <summary>
        /// Add S3 Headers to request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="method"></param>
        /// <param name="canonicalizedResource"></param>
        /// <param name="secretKey"></param>
        /// <param name="accessKey"></param>
        public void AddS3headers(HttpRequestMessage request, string method, string canonicalizedResource)
        {
            var date = DateTime.UtcNow.ToString("ddd, dd MMM yyyy HH:mm:ss \\G\\M\\T");
            request.Headers.Add("Date", date);

            var stringToSign = $"{method}\n\n\n{date}\n{canonicalizedResource}";
            var signature = SignString(_secretKey, stringToSign);

            var authHeader = $"AWS {_accessKey}:{signature}";
            request.Headers.Add("Authorization", authHeader);
        }

        /// <summary>
        /// Sign String
        /// </summary>
        /// <param name="secretKey"></param>
        /// <param name="stringToSign"></param>
        /// <returns></returns>
        private string SignString(string secretKey, string stringToSign)
        {
            var encoding = new ASCIIEncoding();
            var keyBytes = encoding.GetBytes(secretKey);
            var dataBytes = encoding.GetBytes(stringToSign);
            using (var hmacsha1 = new HMACSHA256(keyBytes))
            {
                var hashBytes = hmacsha1.ComputeHash(dataBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

    }
}
