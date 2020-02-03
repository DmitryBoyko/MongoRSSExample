using MongoRSSExample.Models;
using MongoRSSExample.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Xml.Linq;

namespace MongoRSSExample.Bus.DTOs
{
    public sealed class RSSFeed : IRSSFeed
    {
        private readonly RSSService _rSSService;

        public RSSFeed(RSSService rssService)
        {
            _rSSService = rssService;
        }

        public IEnumerable<NasdaqRSS> Download(string url)
        {
            try
            {
                var items = new List<NasdaqRSS>();

                // It could be improved if we need it https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlreader?redirectedfrom=MSDN&view=netframework-4.8
                using var xmlReader = XmlReader.Create(url);
                var feed = SyndicationFeed.Load(xmlReader);

                foreach (var item in feed.Items)
                {
                    var dates = item.ElementExtensions.Where(x => x.OuterName == "date");

                    var date = DateTime.Now;
                    if (dates.Any())
                    {
                        var element = dates.First().GetObject<XElement>();
                        _ = DateTime.TryParse(element.Value, out DateTime parsedDate);
                        date = parsedDate;
                    }

                    string link = null;
                    if (item.Links.Any())
                    {
                        link = item.Links[0].Uri.ToString();
                    }

                    var n = new NasdaqRSS() { ItemID = item.Id, Title = item.Title.Text, Summary = item.Summary.Text, Date = date, Link = link };
                    items.Add(n);
                }

                return items;
            }
            catch (Exception ex)
            {
                //TODO 
            }

            return null;
        }

        public NasdaqRSS ReadById(string itemID)
        {
            try
            {
                //_rSSService.Get(itemID);

                var r = _rSSService.Get().FirstOrDefault(x => x.ItemID == itemID);
                if (r != null)
                {
                    return r;
                }
            }
            catch (Exception ex)
            {
                //TODO;
            }

            return null;
        }

        public IEnumerable<NasdaqRSS> Read()
        {
            List<NasdaqRSS> r = null;

            try
            {
                r = _rSSService.Get();
            }
            catch (Exception ex)
            {
                //TODO
            }

            return r;
        }

        public void Save(NasdaqRSS item)
        {
            try
            {
                var result = _rSSService.Create(item);
                //TODO Create Event: Collection Updated
            }
            catch (Exception ex)
            {
                //TODO
            }
        }
    }
}
