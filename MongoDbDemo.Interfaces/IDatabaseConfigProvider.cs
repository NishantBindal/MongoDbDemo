using System;

namespace MongoDbDemo.Interfaces
{
    public interface IDatabaseConfigProvider
    {
        string MongoDatabaseName { get; }
        string ConnectionString { get; }
    }
}
