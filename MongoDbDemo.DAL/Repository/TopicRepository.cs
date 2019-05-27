using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDbDemo.DAL.Entity;
using MongoDB.Driver.Linq;
using System.Linq;
using Projection=MongoDbDemo.Models;
using System.Text.RegularExpressions;
using MongoDB.Bson;

namespace MongoDbDemo.DAL.Repository
{
    public class TopicRepository : Repository<Entity.Topic>
    {
        private IMongoCollection<Topic> _mongoCollection;
        public TopicRepository(IMongoCollection<Topic> mongoCollection) : base(mongoCollection)
        {
            _mongoCollection = mongoCollection;
        }
        public IEnumerable<Projection.AuthorStats> GetAuthorStats()
        {
            return _mongoCollection.AsQueryable<Topic>().GroupBy(topic=>topic.Author).Select(group=>new Projection.AuthorStats() { Author = group.Key, Likes = group.Sum(item=>item.Likes) });
        }
        public IQueryable<Entity.Topic> GetAllByAuthorName(string name)
        {
            return _mongoCollection.AsQueryable<Topic>().Where(topic=>topic.Author==name);
        }
        public async Task<bool> LikeAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return false;
            var builder = Builders<Topic>.Filter;
            var filter = builder.Eq(x => x.Id, new ObjectId(id));
            var existingDoc = _mongoCollection.AsQueryable().Where(topic => topic.Id == new ObjectId(id)).FirstOrDefault();
            if (existingDoc != null)
            {
                var update = Builders<Topic>.Update
                    .Set(x=>x.Likes,existingDoc.Likes+1);
                var response=await _mongoCollection.UpdateOneAsync(filter, update, new UpdateOptions() { IsUpsert = false });
                if(response.IsModifiedCountAvailable)
                    return true;
                return false;

            }
            return false;
        }
    }
}
