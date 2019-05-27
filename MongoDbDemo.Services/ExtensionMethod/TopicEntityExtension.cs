using Prjection = MongoDbDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity = MongoDbDemo.DAL.Entity;
using Projection = MongoDbDemo.Models;

namespace MongoDbDemo.Services.ExtensionMethod
{
    public static class TopicEntityExtension
    {
        public static Projection.Topic MapEntityToProjection(this Entity.Topic doc)
        {
            return new Projection.Topic()
                {
                    Id = doc.Id.ToString(),
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
