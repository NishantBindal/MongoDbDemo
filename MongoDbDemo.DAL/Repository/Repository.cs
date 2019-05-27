using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDbDemo.DAL.Entity;
using MongoDbDemo.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbDemo.DAL.Repository
{
    public class Repository<T> : IRepository<T> where T : Base
    {
        private IMongoCollection<T> _mongoCollection { get; set; }
        public Repository(IMongoCollection<T> mongoCollection)
        {
            _mongoCollection = mongoCollection;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using (IAsyncCursor<T> cursor = await _mongoCollection.FindAsync<T>(FilterDefinition<T>.Empty))
            {
                List<T> documents = new List<T>();
                while (await cursor.MoveNextAsync())
                {
                    IEnumerable<T> batch = cursor.Current;
                    documents.AddRange(batch);
                }
                return documents;
            }
        }
        public async Task<T> GetByIdAsync(string Id)
        {
            //var filter = new FilterDefinitionBuilder<T>().Eq(doc => doc.Id, Id);
            var filter = new FilterDefinitionBuilder<T>().Eq("Id", new ObjectId(Id));
            return await _mongoCollection.FindAsync(filter).Result.FirstAsync();
        }

        public async Task<bool> CreateAsync(T data)
        {
            if (data == null)
                return false;
            await _mongoCollection.InsertOneAsync(data);
            return true;
        }

        public async Task<bool> UpdateAsync(string Id, T data)
        {
            if (string.IsNullOrEmpty(Id) || data == null)
                return false;
            //await DeleteAsync(Id);
            //await CreateAsync(data);
            var replaceOneResult = await _mongoCollection.ReplaceOneAsync(
                                            doc => doc.Id == new ObjectId(Id),
                                            data,
                                            new UpdateOptions { IsUpsert = true });
            if(replaceOneResult.ModifiedCount>0)
                return true;
            return false;
        }

        public async Task<bool> DeleteAsync(string Id)
        {
            if (string.IsNullOrEmpty(Id))
                return false;
            var filter = new FilterDefinitionBuilder<T>().Eq("Id", new ObjectId(Id));
            await _mongoCollection.DeleteOneAsync(filter);
            return true;
        }
    }
}
