using MongoDB.Driver;
using MongoDbDemo.DAL.Entity;
using MongoDbDemo.Interfaces;
using System;

namespace MongoDbDemo.DAL
{
    public class MongoDbDemoContext
    {
        public readonly string DatabaseName;
        public const string TOPICS_COLLECTION_NAME = "Topic";

        private IMongoClient _client;
        private IMongoDatabase _database;
        private IDatabaseConfigProvider _databaseConfigProvider { get; set; }
        public MongoDbDemoContext(IDatabaseConfigProvider databaseConfigProvider)
        {
            _databaseConfigProvider = databaseConfigProvider;
            DatabaseName = databaseConfigProvider.MongoDatabaseName;
            var connectionString = databaseConfigProvider.ConnectionString;
        }
        public IMongoClient Client
        {
            get
            {
                if (_client == null)
                {
                    _client = new MongoClient(_databaseConfigProvider.ConnectionString);
                    //var credential = MongoCredential.CreateCredential(_databaseConfigProvider.MongoDatabaseName, "test", "testpwd");

                    //var settings = new MongoClientSettings
                    //{
                    //    Credential = credential 
                    //};
                    //_client = new MongoClient();
                }
                return _client;
            }
        }
        public IMongoDatabase Database
        {
            get
            {
                if (_database == null)
                {
                    _database = Client.GetDatabase(_databaseConfigProvider.MongoDatabaseName);
                }
                return _database;
            }
        }
        public IMongoCollection<Topic> TopicCollection { get { return Database.GetCollection<Topic>(TOPICS_COLLECTION_NAME); } }
    }
}