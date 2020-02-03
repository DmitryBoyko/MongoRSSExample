using MongoRSSExample.Bus.DTOs;
using MongoRSSExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MongoRSSExample.Bus.Commands
{
    public class RSSCommand : ICommand
    {
        public DateTime Now { get; set; }
        public Guid ID { get; set; }

        private readonly RSSFeed RSSFeed;

        public RSSCommand(RSSFeed rSSFeed)
        {
            RSSFeed = rSSFeed;
        }

        public void UpdateRSS(IEnumerable<NasdaqRSS> items)
        {
            try
            {
                foreach (var item in items)
                {

                    var nasdaqRSS = RSSFeed.ReadById(item.ItemID);
                    if (nasdaqRSS == null)
                    {
                        RSSFeed.Save(item);
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO
            }
        }

        public List<NasdaqRSS> ReadRSS(DateTime dt)
        {
            return RSSFeed.Read().Where(x => x.Date >= dt).ToList();
        }

        public IEnumerable<NasdaqRSS> DownloadRSS(string url)
        {
            return RSSFeed.Download(url);
        }
    }
}
