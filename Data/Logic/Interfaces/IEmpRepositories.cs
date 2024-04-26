using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface IEmpRepositories :IGenericRepository<Employee>
    {
        IEnumerable<Employee> Search(string name);
        //Employee GetEmloyeeid(int id);
        //IEnumerable<Employee> GetAll();
        //int Add(Employee emloyee);
        //int Update(Employee emloyee);
        //int Delete(Employee emloyee);
    }
}
