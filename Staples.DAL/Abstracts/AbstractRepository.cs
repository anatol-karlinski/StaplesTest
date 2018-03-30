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

        public AbstractRepository()
        {
            _xmlDbHelper = new XMLHelper<T>(typeof(T).Name + "Db");
        }

        protected async Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> whereQuery)
        {
            using (var context = new StaplesDbContext())
            {
                return await context.Set<T>()
                    .Where(whereQuery)
                    .ToListAsync();
            }
        }

        protected async Task<int> AddAsync(T entity)
        {
            Task<int> contextSaveChangesTask;
            Task<bool> xmlDatabaseAddTask = _xmlDbHelper.AddAsync(entity);

            using (var context = new StaplesDbContext())
            {
                context.Set<T>().Add(entity);
                contextSaveChangesTask = context.SaveChangesAsync();
            }

            await Task.WhenAll(contextSaveChangesTask, xmlDatabaseAddTask);

            return contextSaveChangesTask.Result;
        }
    }
}