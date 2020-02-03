using MongoRSSExample.Models;
using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;

namespace MongoRSSExample.Bus.DTOs
{
    public interface IRSSFeed
    {
        public void Save(NasdaqRSS item);

        public IEnumerable<NasdaqRSS> Download(string url);

        public IEnumerable<NasdaqRSS> Read();

        public NasdaqRSS ReadById(string itemID);
        
    }
}
