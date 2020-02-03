using MongoRSSExample.Models;
using MongoRSSExample.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;

namespace MongoRSSExample.Bus.Commands
{
    interface ICommand
    {
        DateTime Now { get; set; }

        Guid ID { get; set; }

        void UpdateRSS(IEnumerable<NasdaqRSS> items);

        IEnumerable<NasdaqRSS> DownloadRSS(string str);

        List<NasdaqRSS> ReadRSS(DateTime dt);
    }
}
