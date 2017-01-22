using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace M101DotNet.WebApp.Models
{
    public class Comment
    {
        [BsonElement("author")]
        public string Author { get; set; }

        [BsonElement("body")]
        public string Content { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public int Likes { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }
    }
}