using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MongoDbDemo.Models.Dtos
{
    public class TopicsData
    {
        public IEnumerable<Topic> Topics { get; set; }
        public IEnumerable<AuthorStats> AuthorStats { get; set; }
    }
}