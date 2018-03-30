using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Staples.DAL.Interfaces
{
    public interface IRepository<T>
    {
        Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> whereQuery);
        Task<int> AddAsync(T entity);
    }
}