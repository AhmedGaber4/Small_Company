using Data;
using Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyDbcontexts _contexts;

        public IDepRepositories _depRepository { get; set; }
        public IEmpRepositories _empRepositories { get; set; }
        public UnitOfWork(MyDbcontexts contexts)
        {
            _depRepository = new DepRepositories(contexts);
            _empRepositories = new EmpRepositories(contexts);
           _contexts = contexts;
        }

        public int Complete()
        {
            return _contexts.SaveChanges();
        }
    }
}
