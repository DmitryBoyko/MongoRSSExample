using MongoRSSExample.Bus.DTOs;
using MongoRSSExample.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoRSSExample.Bus.Queries
{
    public class MyRSS
    {
        public static RSSFeed Instance(RSSService rssService)
        {
            return new RSSFeed(rssService);
        }

    }
}
