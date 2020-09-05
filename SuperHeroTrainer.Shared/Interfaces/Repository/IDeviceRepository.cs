using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroTrainer.Shared.Interfaces.Repository
{
    public interface IRepository<T>
    {
        T GetById(object id);
        Task<T> GetByIdAsync(object id);
        T Insert(T entity);
        void Insert(IEnumerable<T> entities);
        T Update(T entity);
        Task<T> UpdateAsync(T entity);
        Task UpdateAsync(IEnumerable<T> entities);
        void Update(IEnumerable<T> entities);
        T Delete(T entity);
        void Delete(IEnumerable<T> entities);

        IQueryable<T> GetAll();
        Task<T> InsertAsync(T entity);
        Task<T> DeleteAsync(T entity);
        Task<T> InsertAsync(IEnumerable<T> entities);
        Task<T> DeleteAsync(IEnumerable<T> entities);
    }
}
