using Data;
using Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : TheBase
    {
        private readonly MyDbcontexts _contexts;

        public GenericRepository(MyDbcontexts contexts)
        {
            _contexts = contexts;
        }
        public void Add(T t)
        {
            _contexts.Set<T>().Add(t);
          //  return _contexts.SaveChanges();
        }

        public void Delete(T t)
        {
            _contexts.Set<T>().Remove(t);
           // return _contexts.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        => _contexts.Set<T>().ToList();

        public T GetEntityid(int? id)
           => _contexts.Set<T>().Find(id);

        public void Update(T t)
        {
            _contexts.Set<T>().Update(t);
           // return _contexts.SaveChanges();
        }
    }
}
