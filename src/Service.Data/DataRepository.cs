using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace Service.Data
{
    public class DataRepository : IDatabaseRepository
    {
         readonly DbContext _context;
         public DataRepository(DbContext context)
        {
            _context = context;
        }

        public void CommitChanges()
        {
            _context.SaveChanges();
        }

        public void Delete<T>(Expression<Func<T, bool>> expression) where T : class, new()
        {
            var query = All<T>().Where(expression);
            foreach (var item in query)
            {
                Delete(item);
            }
        }

        public void Delete<T>(T item) where T : class, new()
        {
            _context.Set<T>().Remove(item);
        }

        public void DeleteAll<T>() where T : class, new()
        {
            var query = All<T>();
            foreach (var item in query)
            {
                Delete(item);
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public T Single<T>(Expression<Func<T, bool>> expression) where T : class, new()
        {
            return All<T>().FirstOrDefault(expression);
        }

        public IQueryable<T> All<T>() where T : class, new()
        {
            return _context.Set<T>().AsQueryable();
        }



        public void Add<T>(T item) where T : class, new()
        {
            _context.Set<T>().Add(item);
        }


        public void Add<T>(IEnumerable<T> items) where T : class, new()
        {
            foreach (var item in items)
            {
                Add(item);
            }
        }

        public void Update<T>(T item) where T : class, new()
        {
            _context.Entry<T>(item).State = EntityState.Modified;
        }
    }
}