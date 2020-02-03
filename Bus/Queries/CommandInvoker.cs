using MongoRSSExample.Bus.Commands;
using MongoRSSExample.Models;
using System;
using System.Collections.Generic;

namespace MongoRSSExample.Bus.Queries
{
    public class CommandInvoker
    {
        readonly RSSCommand RSSCommand;

        public CommandInvoker(RSSCommand command)
        {
           RSSCommand = command;
        }

        public IEnumerable<NasdaqRSS> Read(DateTime dt)
        {
            return RSSCommand.ReadRSS(dt);
        }

        public void Download(string url)
        {
            var feed = RSSCommand.DownloadRSS(url);
            if (feed != null)
            {
                RSSCommand.UpdateRSS(feed);
            } 
        } 
    }
}
