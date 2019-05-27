//using Projection=MongoDbDemo.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using Dto=MongoDbDemo.Models.Dtos;

//namespace MongoDbDemo.ExtensionMethod
//{s
//    public static class IEnumerableExtension
//    {
//        public static IEnumerable<Dto.Topic> MapEntityToProjection(this IEnumerable<Topic> docs)
//        {
//            List<Projection.Topic> topics = new List<Projection.Topic>();
//            foreach (var doc in docs)
//            {
//                topics.Add(new Projection.Topic()
//                {
//                    Id=doc.i
//                    Author = data.Author,
//                    Description = data.Description,
//                    LikedByUsers = data.LikedByUsers,
//                    Likes = data.Likes,
//                    Title = data.Title,
//                    Link = data.Link,
//                    WrittenOn = data.WrittenOn
//                };)
//            }
//        }
//    }
//}