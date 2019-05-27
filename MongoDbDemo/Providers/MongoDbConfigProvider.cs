using MongoDbDemo.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MongoDbDemo.Providers
{
    public class MongoDbConfigProvider : IDatabaseConfigProvider
    {
        public string MongoDatabaseName => ConfigurationManager.AppSettings["MongoDatabaseName"];

        public string ConnectionString => ConfigurationManager.ConnectionStrings["MongoDbDemo"].ConnectionString;
    }
}