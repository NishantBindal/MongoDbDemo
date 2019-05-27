using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDbDemo.DAL.Entity
{
    public class Topic:Base
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Author { get; set; }
        public DateTime WrittenOn { get; set; }
        public long Likes { get; set; }
        public IEnumerable<string> LikedByUsers { get; set; }
        public string File { get; set; }
    }
}
