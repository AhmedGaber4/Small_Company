using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface IUnitOfWork
    {
        public IDepRepositories _depRepository { get; set; }
        public IEmpRepositories _empRepositories { get; set; }

      public  int Complete();
    }
}
