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
        public int Add(T entity);
        public void Delete(int id);    
        public void Update(T entity);
        public IEnumerable<T> GetAll();
    }
}
