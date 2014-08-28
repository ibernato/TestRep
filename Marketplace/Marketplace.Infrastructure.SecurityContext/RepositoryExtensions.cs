using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Infrastructure
{
    public static class RepositoryExtensions
    {
        public static DbQuery<T> Includes<T>(this DbSet<T> dbSet, params String [] includes) where T : class
        {
            if (includes.Count() == 0)
                return dbSet;

            DbQuery<T> result = null;

            foreach (var include in includes)
            {
                if (result == null)
                {
                    result = dbSet.Include(include);
                }
                else
                {
                    result.Include(include);
                }
            }

            return result;
        }
    }
}
