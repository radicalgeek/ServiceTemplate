using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Data.Relational
{
    public class DataContextResporitory : IDataContextResporitory
    {
        private readonly DataContext _context;
        
        public DataContextResporitory(DataContext context)
        {
            _context = context;
        }
        
        public List<SampleEntity> GetAll()
        {     
            return _context.Entites.ToList();
        }
 
        public SampleEntity Get(long id)
        {
            return _context.Entites.First(t => t.Id == id);
        }
 
        public void Post(SampleEntity dataEventRecord )
        {
            _context.Entites.Add(dataEventRecord);
            _context.SaveChanges();
        }
 
        public void Put(SampleEntity dataEventRecord)
        {
            _context.Entites.Update(dataEventRecord);
            _context.SaveChanges();
        }
 
        public void Delete(long id)
        {
            var entity = _context.Entites.First(t => t.Id == id);
            _context.Entites.Remove(entity);
            _context.SaveChanges();
        }
    }
}
