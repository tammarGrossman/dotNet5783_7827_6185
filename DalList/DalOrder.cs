
using DO;
namespace Dal;
using DalApi;
using System.Security.Cryptography;
internal class DalOrder : IOrder
{
    /// <summary>
    /// add object
    /// </summary>
    /// <param name="o"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public int Add(Order o)
    {
        int i = DataSource.Config.LastOrder;
        o.ID = i;

        if (OrderExist(o.ID))
            throw new Duplication(o.ID, "order");

        DataSource.orders.Add(o);
        return i;
    }


    /// <summary>
    /// get object
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Order Get(int id)
    {
        if (OrderExist(id))//found
            return  DataSource.orders.FirstOrDefault(order => order?.ID == id)?? throw new NotExist(id, "order");
        throw new NotExist(id, "order");
    }
    /// <summary>
    /// function that check if the order exist 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private bool OrderExist(int id)
    {
        return DataSource.orders.Any(order => order?.ID == id);
    }
    /// <summary>
    /// delete object
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public void Delete(int id)
    {
        int count = DataSource.orders.RemoveAll(order => order?.ID == id);

        if (count == 0)
        throw new NotExist(id, "order");
    

    }

    /// <summary>
    /// update object
    /// </summary>
    /// <param name="o"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Order o)
    {
        int count = DataSource.orders.RemoveAll(order => order?.ID == o.ID);
        if (count == 0)
            throw new NotExist(o.ID, "order");

        DataSource.orders.Add(o);
    }

    /// <summary>
    /// get all objects
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? Condition = null)
    {
        if (Condition != null)
            return from order in DataSource.orders
                   where Condition(order)
                   select order;

        return from order in DataSource.orders
               select order; 

    }
    public Order GetByCon(Func<Order?, bool> Condition)
    {
        return DataSource.orders.Find(x => Condition(x)) ??
          throw new NotExist(0, "order");
    }
}