using PromocionesFravega.Core.Interfaces;
using PromocionesFravega.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PromocionesFravega.Core.Entities;

namespace PromocionesFravega.Infrastructure.Repositories
{
    public class Repository<T>: IRepository<T> where T: class
    {
        protected readonly MLControlStockContext _context;
        protected readonly DbSet<T> _entities;

        public Repository(MLControlStockContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
                          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                          string includeProperties = "", int? take = null)
        {
            IQueryable<T> query = _entities;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (take != null)
            {
                query = query.Take(take.Value);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public T GetFirst(Expression<Func<T, bool>> filter = null,string includeProperties = "")
        {
            return Get(filter,null, includeProperties).FirstOrDefault();
        }

        public async Task<T> GetByID(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<T> GetByID(long id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<T> GetByID(string id)
        {
            return await _entities.FindAsync(id); 
        }

        public async Task Insert(T entity)
        {
            await _entities.AddAsync(entity);            
        }

        public async Task AddRange(IEnumerable<T> entity)
        {
            await _entities.AddRangeAsync(entity);
        }
        public void Update(T entity)
        {
            _entities.Update(entity);
        }

        public void Desacoplar(T entity)
        {
            _context.Entry(entity).State = EntityState.Detached;            
        }
        //public void Update(T entity)
        //{
        //    _entities.Attach(entity);
        //    _context.Entry(entity).State = EntityState.Modified;
        //}

        public async Task Delete(T entity)
        {
            _entities.Remove(entity);
        }

        public async Task Delete(int id)
        {
             T entity = await GetByID(id);
            _entities.Remove(entity);           
        }

        public async Task Delete(string id)
        {
            T entity = await GetByID(id);
            _entities.Remove(entity);
        }

        
    }
}
