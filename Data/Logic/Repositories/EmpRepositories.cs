using Data;
using Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Repositories
{
    public class EmpRepositories : GenericRepository<Employee> ,IEmpRepositories
    {
        private readonly MyDbcontexts _contexts;

        public EmpRepositories(MyDbcontexts contexts) :base(contexts) 
        {
            _contexts = contexts;
        }

        public IEnumerable<Employee> Search(string name)
        {
           var rt= _contexts.Emloyees.Where(
               h => h.Name.Trim().ToLower().Contains(name.Trim().ToLower()) ||
              h.Email.Trim().ToLower().Contains(name.Trim().ToLower())
               );
            return rt;
        }

        //public int Add(Employee employee)
        //{
        //    _contexts.Emloyees.Add(employee);
        //    return _contexts.SaveChanges();
        //}

        //public int Delete(Employee employee)
        //{
        //    _contexts.Emloyees.Remove(employee);
        //    return _contexts.SaveChanges();
        //}

        //public IEnumerable<Employee> GetAll()
        //    => _contexts.Emloyees.ToList();


        //public Employee GetEmloyeeid(int id)
        //=> _contexts.Emloyees.FirstOrDefault(x => x.Id == id);

        //public int Update(Employee employee)
        //{
        //    _contexts.Update(employee);
        //    return _contexts.SaveChanges();
        //}

    }
}
