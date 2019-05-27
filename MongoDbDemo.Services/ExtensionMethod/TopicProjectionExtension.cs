using Prjection = MongoDbDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity = MongoDbDemo.DAL.Entity;
using Projection = MongoDbDemo.Models;
using MongoDB.Bson;

namespace MongoDbDemo.Services.ExtensionMethod
{
    public static class TopicProjectionExtension
    {
        public static Entity.Topic MapProjectionToEntity(this Projection.Topic doc)
        {
            return new Entity.Topic()
            {
                Id = new ObjectId(doc.Id),
                Author = doc.Author,
                Description = doc.Description,
                LikedByUsers = doc.LikedByUsers,
                Likes = doc.Likes,
                Title = doc.Title,
                Link = doc.Link,
                WrittenOn = doc.WrittenOn
            };
        }
    }
}
