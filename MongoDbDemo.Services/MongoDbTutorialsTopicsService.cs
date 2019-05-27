using ExpressMapper;
using MongoDbDemo.DAL;
using Entity = MongoDbDemo.DAL.Entity;
using MongoDbDemo.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Projection = MongoDbDemo.Models;
using MongoDbDemo.Services.ExtensionMethod;
using MongoDbDemo.DAL.Repository;

namespace MongoDbDemo.Services
{
    public class MongoDbTutorialsTopicsService
    {
        private MongoDbDemoContext _mongoDbDemoContext { get; set; }
        private IRepository<Entity.Topic> _repository { get; set; }
        public MongoDbTutorialsTopicsService(IDatabaseConfigProvider databaseConfigProvider)
        {
            _mongoDbDemoContext = new MongoDbDemoContext(databaseConfigProvider);
            _repository = new MongoDbDemo.DAL.Repository.TopicRepository(_mongoDbDemoContext.TopicCollection);
        }
        public async Task<IEnumerable<Projection.Topic>> GetAllAsync()
        {
            var documents = await _repository.GetAllAsync();
            return documents.MapEntityToProjection();
        }
        public IEnumerable<Projection.AuthorStats> GetTotalLikesPerAuthor()
        {
            MongoDbDemo.DAL.Repository.TopicRepository topicRepository = (MongoDbDemo.DAL.Repository.TopicRepository)_repository;
            var authorStats = topicRepository.GetAuthorStats();
            return authorStats;
        }
        public async Task<Projection.Topic> GetByIdAsync(string Id)
        {
            var document = await _repository.GetByIdAsync(Id);
            return document.MapEntityToProjection();
        }

        public async Task<bool> CreateAsync(Projection.Topic data)
        {
            var topic = new Entity.Topic()
            {
                Author = data.Author.ToLower(),
                Description = data.Description,
                LikedByUsers = data.LikedByUsers,
                Likes = data.Likes,
                Title = data.Title,
                Link = data.Link,
                WrittenOn = data.WrittenOn,
                File = data.File
            };
            return await _repository.CreateAsync(topic);
        }

        public async Task<bool> UpdateAsync(string Id, Projection.Topic data)
        {
            return await _repository.UpdateAsync(Id, data.MapProjectionToEntity());
        }

        public async Task<bool> DeleteAsync(string Id)
        {
            return await _repository.DeleteAsync(Id);
        }

        public IEnumerable<Projection.Topic> GetAllByAuthorName(string name)
        {
            MongoDbDemo.DAL.Repository.TopicRepository topicRepository = (MongoDbDemo.DAL.Repository.TopicRepository)_repository;
            var authorStats = topicRepository.GetAllByAuthorName(name);
            return authorStats.MapEntityToProjection();
        }
        public async Task<bool> LikeAsync(string Id)
        {
            var result = await ((TopicRepository)_repository).LikeAsync(Id);
            return result;
        }

    }
}