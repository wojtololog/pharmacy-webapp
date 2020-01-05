using System.Collections;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Reflection;

namespace PharmacyWebApp.Models
{
    static class ContextExtensions
    {
        static public DbQuery<TEntity> IncludeAll<TEntity>(this DbSet<TEntity> dbSet)
            where TEntity : class
        {
            DbQuery<TEntity> query = null;

            foreach (PropertyInfo propertyInfo in typeof(TEntity).GetProperties())
            {
                if (typeof(TableRecord).IsAssignableFrom(propertyInfo.PropertyType) ||
                    typeof(IList).IsAssignableFrom(propertyInfo.PropertyType))
                {
                    if (query == null)
                    {
                        query = dbSet.Include(propertyInfo.Name);
                    }
                    else
                    {
                        query.Include(propertyInfo.Name);
                    }
                }
            }

            return query;
        }

        static public DbQuery<TEntity> Include<TEntity>(this DbSet<TEntity> dbSet, params string[] related)
            where TEntity : class
        {
            DbQuery<TEntity> query = null;

            foreach (var relatedName in related)
            {
                if (query == null)
                {
                    query = dbSet.Include(relatedName);
                }
                else
                {
                    query.Include(relatedName);
                }
            }

            return query;
        }

        static public void LoadRecord<TEntity>(this PharmacyDBContext context, TEntity record)
            where TEntity : class
        {
            context.Entry(record).State = EntityState.Unchanged;
            foreach (PropertyInfo propertyInfo in typeof(TEntity).GetProperties())
            {
                if (typeof(TableRecord).IsAssignableFrom(propertyInfo.PropertyType))
                {
                    context.Entry(record).Reference(propertyInfo.Name).Load();
                }
                else if (typeof(IList).IsAssignableFrom(propertyInfo.PropertyType))
                {
                    context.Entry(record).Collection(propertyInfo.Name).Load();
                }
            }
        }
    }
}
