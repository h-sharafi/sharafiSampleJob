
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application;

namespace Application
{
    public interface IEntityService<T> : IService where T : class
    {
        void Create(T entity);
     
    }
}
