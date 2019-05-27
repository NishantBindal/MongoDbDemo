using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDbDemo.Models.Dtos
{
    public class AuthorStats
    {
        public string Author { get; set; }
        public long Likes { get; set; }
    }
}
