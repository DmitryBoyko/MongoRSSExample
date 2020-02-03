using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoRSSExample.Models
{
    public interface IRSSDatabaseSettings
    {
        string RSSCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }

    public class RSSDatabaseSettings : IRSSDatabaseSettings
    {
        public string RSSCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

}
