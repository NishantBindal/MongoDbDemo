using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbDemo.Interfaces
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(string Id);
        Task<bool> CreateAsync(T data);
        Task<bool> UpdateAsync(string Id, T data);
        Task<bool> DeleteAsync(string Id);
    }
}
