using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PromocionesFravega.Core.Interfaces
{
    public interface IRepository<T> where T:class
    {
        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
                          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                          string includeProperties = "", int? take = null);
        Task<T> GetByID(int id);
        Task<T> GetByID(long id);
        Task<T> GetByID(string id);
        T GetFirst(Expression<Func<T, bool>> filter = null, string includeProperties = "");

        Task Insert(T entity);
        Task AddRange(IEnumerable<T> entity);
        void Update(T entity);
        
        Task Delete(T entity);
        Task Delete(int id);
        Task Delete(string id);
        void Desacoplar(T entity);


    }
}
