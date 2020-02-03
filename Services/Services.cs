using MongoDB.Driver;
using MongoRSSExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoRSSExample.Services
{
   /* public interface IRSSService
    {
        List<NasdaqRSS> Get();
        NasdaqRSS Get(string id);

        NasdaqRSS Create(NasdaqRSS item);

        void Update(string id, NasdaqRSS itemIn);

        void Remove(NasdaqRSS itemIn);

        void Remove(string id);
    }*/

    public class RSSService 
    {
        private readonly IMongoCollection<NasdaqRSS> _docs;

        public RSSService(IRSSDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _docs = database.GetCollection<NasdaqRSS>(settings.RSSCollectionName);
        }

        public List<NasdaqRSS> Get() => _docs.Find(item => true).ToList();

        public NasdaqRSS Get(string id) => _docs.Find<NasdaqRSS>(item => item.Id == id).FirstOrDefault();

        public NasdaqRSS Create(NasdaqRSS item) { _docs.InsertOne(item); return item; }

        public void Update(string id, NasdaqRSS itemIn) => _docs.ReplaceOne(item => item.Id == id, itemIn);

        public void Remove(NasdaqRSS itemIn) => _docs.DeleteOne(item => item.Id == itemIn.Id);

        public void Remove(string id) => _docs.DeleteOne(item => item.Id == id);
    }
}
