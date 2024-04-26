using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface IGenericRepository<T> where T : TheBase
    {
        T GetEntityid(int? id);
        IEnumerable<T> GetAll();
        void Add(T t);
        void Update(T t);
        void Delete(T t); 

    }
}
