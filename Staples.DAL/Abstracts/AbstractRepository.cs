using Staples.DAL.Helpers;
using Staples.DAL.Interfaces;
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
        private IXmlDbHelper<T> _xmlDbHelper;
        private ILogHelper _logHelper;

        public AbstractRepository()
        {
            _xmlDbHelper = new XmlDbHelper<T>(); ;
            _logHelper = new LogHelper();
        }

        public async Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> whereQuery)
        {
            using (var context = new StaplesDBContext())
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
            using (var context = new StaplesDBContext())
            {
                context.Set<T>().Add(entity);
                return await context.SaveChangesAsync();
            }
        }
    }
}