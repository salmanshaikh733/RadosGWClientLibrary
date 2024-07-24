using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadosGWClientLibrary.Models
{
    public class RadosStorageObject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public string Version { get; set; }
        public DateTime LastModified { get; set; }
        public KeyValuePair<string, string> MetaData { get; set; }
    }
}
