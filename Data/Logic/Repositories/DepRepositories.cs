using Data;
using Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Repositories
{
    public class DepRepositories :GenericRepository<Department> ,IDepRepositories
    {
        private readonly MyDbcontexts _contexts;

        public DepRepositories(MyDbcontexts contexts) :base(contexts) 
        {
            _contexts = contexts;
        }

        //public int Add(Department department)
        //{
        //    _contexts.Departments.Add(department);
        //    return _contexts.SaveChanges();
        //}

        //public int Delete(Department department)
        //{
        //    _contexts.Departments.Remove(department);
        //    return _contexts.SaveChanges();
        //}

        //public IEnumerable<Department> GetAll()
        //    => _contexts.Departments.ToList();


        //public Department GetEmloyeeid(int? id)
        //=> _contexts.Departments.FirstOrDefault(x => x.Id == id);

        //public int Update(Department department)
        //{
        //    _contexts.Update(department);
        //    return _contexts.SaveChanges();
        //}
    }
}
