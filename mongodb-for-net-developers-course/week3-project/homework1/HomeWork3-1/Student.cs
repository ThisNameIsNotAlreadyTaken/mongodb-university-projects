using MongoDB.Bson.Serialization.Attributes;

namespace HomeWork3_1
{
    public class Student
    {
        public int Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("scores")]
        public Scores[] Scores { get; set; }
    }
}
