using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Report.Core.Entities
{
    public class ReportEntity
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public int PersonCount { get; set; }
        public int PhoneCount { get; set; }
        public string Status { get; set; }
    }
}
