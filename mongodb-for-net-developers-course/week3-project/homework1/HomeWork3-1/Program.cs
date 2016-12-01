using System;
using System.Linq;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace HomeWork3_1
{
    internal class Program
    {
        private const string ConnectionString = "mongodb://localhost:27017";

        private static void Main(string[] args)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase("school");
            var students = database.GetCollection<Student>("students");

            var convertionPack = new ConventionPack {new CamelCaseElementNameConvention()};
            ConventionRegistry.Register("camelCase", convertionPack, t => true);
            Work(students);

            Console.ReadKey();
        }

        private static async void Work(IMongoCollection<Student> collection)
        {
            var result = (await collection.FindAsync(Builders<Student>.Filter.Eq("scores.type", "homework"))).ToList();

            result.ForEach(st =>
            {
                var minScore = st.Scores.Where(x => x.Type == "homework").OrderBy(x => x.Score).FirstOrDefault();

                if (minScore != null)
                {
                    collection.FindOneAndUpdate(x => x.Id == st.Id,
                        Builders<Student>.Update.PullFilter(p => p.Scores,
                            f => f.Type == minScore.Type && f.Score == minScore.Score));
                }
            });

            Console.WriteLine(result);
        }
    }
}
