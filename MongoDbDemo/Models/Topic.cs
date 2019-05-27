using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web;

namespace MongoDbDemo.Models.Dtos
{
    public class Topic
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Author { get; set; }
        public DateTime WrittenOn { get; set; }
        public long Likes { get; set; }
        public IEnumerable<string> LikedByUsers { get; set; }
        [Required]
        public HttpPostedFileBase FileBase { get; set; }
        public string File { get; set; }
    }
}
