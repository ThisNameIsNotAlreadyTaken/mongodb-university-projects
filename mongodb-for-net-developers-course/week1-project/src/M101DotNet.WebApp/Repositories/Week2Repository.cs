using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;

namespace M101DotNet.WebApp.Repositories
{
    public class Week2Repository
    {
        private const string ConnectionString = "mongodb://localhost:27017";

        public class Grade
        {
            public ObjectId Id { get; set; }

            [BsonElement("student_id")]
            public int StudentId { get; set; }

            [BsonElement("type")]
            public string Type { get; set; }

            [BsonElement("score")]
            public double Score { get; set; }
        }

        public async Task<List<Grade>> Homework22Async()
        {
            var client = new MongoClient(ConnectionString);
            var db = client.GetDatabase("students");
            var col = db.GetCollection<Grade>("grades");

            var findAll = await col.Find(x => x.Type == "homework").SortBy(x => x.Score).ThenBy(x => x.StudentId).ToListAsync();

            var enumerable = findAll.GroupBy(x => x.StudentId).Select(g => g.OrderBy(x => x.Score).Take(1).Select(x => x.Id).First()).ToList();

            await col.DeleteManyAsync(x => enumerable.Contains(x.Id));

            return findAll;
        }
    }
}