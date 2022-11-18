using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface ICrud<T>
    {
        public T Get(int id); 
        public T Add(T entity);
        public T Delete(int id);    
        public T Update(T entity);
        public IEnumerable<T> GetAll();
    }
}
