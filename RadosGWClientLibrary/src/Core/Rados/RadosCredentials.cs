using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadosGWClientLibrary.src.Core.Rados
{
    public class RadosCredentials
    {
        public string AccessKey { get; private set; }
        public string SecretKey { get; private set; }
        public string Token { get; private set; }
        public bool UseToken { get; private set; }

        public RadosCredentials(string accessKey, string secretKey, string token = "")
        {
            AccessKey = accessKey ?? throw new ArgumentNullException(nameof(accessKey));
            SecretKey = secretKey ?? throw new ArgumentNullException(nameof(secretKey));
            Token = token ?? throw new ArgumentNullException(nameof(token));

            UseToken = !string.IsNullOrEmpty(token);
        }

        public override bool Equals(object? obj)
        {
            return obj is RadosCredentials credentials &&
                   AccessKey == credentials.AccessKey &&
                   SecretKey == credentials.SecretKey &&
                   Token == credentials.Token;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(AccessKey, SecretKey, Token);
        }
    }
}
