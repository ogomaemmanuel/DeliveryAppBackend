using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryAppBackend.Data
{
    public class Repository<TEntity, Tkey> : IRepository<TEntity, Tkey> where TEntity : class
    {
      protected readonly  DbContext _context;
        public Repository(DbContext context)
        {
            _context = context;
        }

        public void Add(TEntity entity)
        {
             _context.Set<TEntity>().Add(entity);
        }

        public async Task AddAsync(TEntity entity)
        {
          await  _context.Set<TEntity>().AddAsync(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }

        public IEnumerable<TEntity> All()
        {
           return _context.Set<TEntity>().ToList();
        }

        public TEntity Find(Tkey id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public void Remove(TEntity entity)
        {
             _context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }
    }
}
