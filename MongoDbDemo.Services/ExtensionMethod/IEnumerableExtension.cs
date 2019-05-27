using Prjection = MongoDbDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity = MongoDbDemo.DAL.Entity;
using Projection = MongoDbDemo.Models;

namespace MongoDbDemo.Services.ExtensionMethod
{
    public static class IEnumerableExtension
    {
        public static IEnumerable<Projection.Topic> MapEntityToProjection(this IEnumerable<Entity.Topic> docs)
        {
            List<Projection.Topic> topics = new List<Projection.Topic>();
            foreach (var doc in docs)
            {
                topics.Add(doc.MapEntityToProjection());
            }
            return topics;
        }
    }
}