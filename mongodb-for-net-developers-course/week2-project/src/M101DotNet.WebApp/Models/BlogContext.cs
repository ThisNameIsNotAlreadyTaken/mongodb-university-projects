using System.Configuration;
using MongoDB.Driver;

namespace M101DotNet.WebApp.Models
{
    public class BlogContext
    {
        public const string ConnectionStringName = "Blog";
        public const string DatabaseName = "blog";
        public const string PostsCollectionName = "posts";
        public const string UsersCollectionName = "users";

        // This is ok... Normally, these or the entire BlogContext
        // would be put into an IoC container. We aren't using one,
        // so we'll just keep them statically here as they are 
        // thread-safe.
        private static readonly IMongoClient _client;
        private static readonly IMongoDatabase Database;

        static BlogContext()
        {
            var connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
            _client = new MongoClient(connectionString);
            Database = _client.GetDatabase(DatabaseName);
        }

        public IMongoClient Client => _client;

        public IMongoCollection<User> Users => Database.GetCollection<User>(UsersCollectionName);
    }
}