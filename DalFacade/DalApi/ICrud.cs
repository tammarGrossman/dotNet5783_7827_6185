using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface ICrud<T>where T:struct
    {
        /// <summary>
        /// the get object function
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get(int id);
        /// <summary>
        ///  the add object function
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Add(T entity);
        /// <summary>
        ///  the delete object function
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id);
        /// <summary>
        ///  the update object function
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity);
        /// <summary>
        ///  the get all objects function
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T?> GetAll(Func<T?, bool>? Condition=null);
        public T GetByCon(Func<T?,bool>? Condition=null);
    }
}
