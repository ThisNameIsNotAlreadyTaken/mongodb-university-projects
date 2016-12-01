using MongoDB.Bson.Serialization.Attributes;

namespace HomeWork3_1
{
    public class Scores
    {
        [BsonElement("type")]
        public string Type { get; set; }
        [BsonElement("score")]
        public double Score { get; set; } 
    }
}
