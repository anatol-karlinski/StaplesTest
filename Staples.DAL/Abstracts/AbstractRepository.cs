using Staples.DAL.Helpers;
using Staples.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Staples.DAL.Abstracts
{
    public abstract class AbstractRepository<T>
        where T : class
    {
        private XMLHelper<T> _xmlDbHelper;
        private LogHelper _logHelper;

        public AbstractRepository()
        {
            _xmlDbHelper = new XMLHelper<T>();
            _logHelper = new LogHelper();
        }

        public async Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> whereQuery)
        {
            using (var context = new StaplesDbContext())
            {
                return await context.Set<T>()
                    .Where(whereQuery)
                    .ToListAsync();
            }
        }

        public async Task<int> AddAsync(T entity)
        {
            _logHelper.LogEntity(entity);
            var taskArray = new List<Task<int>> {
                Task.Run(async () => await AddToDatabaseAsync(entity)),
                Task.Run(async () => await _xmlDbHelper.AddAsync(entity))
            };

            await Task.WhenAll(taskArray);
            return taskArray[1].Result;
        }

        private async Task<int> AddToDatabaseAsync(T entity)
        {
            using (var context = new StaplesDbContext())
            {
                context.Set<T>().Add(entity);
                return await context.SaveChangesAsync();
            }
        }
    }
}