using Microsoft.EntityFrameworkCore;
using SuperHeroTrainer.Core.EntityFramework;
using SuperHeroTrainer.Shared.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace SuperHeroTrainer.Core.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        private readonly IDbContext _context;
        private DbSet<T> _entities;

        public Repository(IDbContext context)
        {
            _context = context;
        }

        public virtual IQueryable<T> Table
        {
            get
            {
                return Entities;
            }
        }


        public virtual IQueryable<T> TableNoTracking
        {
            get
            {
                return Entities.AsNoTracking();
            }
        }

        protected virtual DbSet<T> Entities
        {
            get { return _entities ?? (_entities = _context.Set<T>()); }
        }

        public virtual T Delete(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                Entities.Remove(entity);

                var res = _context.SaveChanges();
                if (res > 0)
                {
                    return entity;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual void Delete(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException("entities");

                foreach (var entity in entities.ToList())
                    Entities.Remove(entity);

                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<T> DeleteAsync(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                Entities.Remove(entity);

                var res = await _context.SaveChangesAsync();
                if (res > 0)
                {
                    return entity;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public virtual async Task<T> DeleteAsync(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException("entities");

                foreach (var entity in entities.ToList())
                    Entities.Remove(entity);

                await _context.SaveChangesAsync();
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        //{
        //    return Entities.Where(predicate);
        //}

        public virtual IQueryable<T> GetAll()
        {
            return Entities;
        }

        public virtual T GetById(object id)
        {
            return Entities.Find(id);
        }

        public virtual async Task<T> GetByIdAsync(object id)
        {
            return await Entities.FindAsync(id);
        }

        public virtual T Insert(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                Entities.Add(entity);

                var result = _context.SaveChanges();
                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual void Insert(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException("entities");

                foreach (var entity in entities)
                    Entities.Add(entity);

                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<T> InsertAsync(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                this.Entities.Add(entity);

                var result = await _context.SaveChangesAsync();
                return result > 0 ? entity : null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<T> InsertAsync(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException("entities");

                foreach (var entity in entities)
                    Entities.Add(entity);

                await _context.SaveChangesAsync();
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual T Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                var res = _context.SaveChanges();
                if (res > 0)
                {
                    return entity;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return null;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                var res = await _context.SaveChangesAsync();
                if (res > 0)
                {
                    return entity;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return null;
        }

        public virtual async Task UpdateAsync(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException("entities");

                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual void Update(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException("entities");

                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
