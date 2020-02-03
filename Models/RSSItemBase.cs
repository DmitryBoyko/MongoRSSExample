using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoRSSExample.Models
{
    public abstract class RSSItemBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("ItemID")]
        /// <summary>
        /// RSS generated Id
        /// </summary>
        public string ItemID { get; set; }

        [BsonElement("Title")]
        public string Title { get; set; }

        [BsonElement("Summary")]
        public string  Summary { get; set; }

        [BsonElement("Date")]
        public DateTime Date { get; set; }

        [BsonElement("Link")]
        public string Link { get; set; }
    }
}
