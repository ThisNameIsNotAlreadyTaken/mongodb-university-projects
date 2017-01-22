using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace M101DotNet.WebApp.Models
{
    public class Post
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("author")]
        public string Author { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("body")]
        public string Content { get; set; }

        [BsonElement("tags")]
        public string[] Tags { get; set; }

        [BsonElement("date")]
        public DateTime CreatedAtUtc { get; set; }

        [BsonElement("comments")]
        public List<Comment> Comments { get; set; }

        [BsonElement("permalink")]
        public string Permalink { get; set; }
    }
}