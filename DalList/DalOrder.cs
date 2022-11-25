//
using DO;
namespace Dal;
using DalApi;
internal class DalOrder : IOrder
{
    public DalOrder()
    {
    }

    /// <summary>
    /// add object
    /// </summary>
    /// <param name="o"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public  int Add(Order o)
    {  //check if there is place
        /*if (DataSource.Config.OrderIndex < DataSource.Orders.Length) */        
            int i = DataSource.Config.LastOrder;
            o.ID = i;
            DataSource.Orders.Add(o);
            return i;       
        //else
            //throw new Exception("there is no place");
    }
    /// <summary>
    /// get object
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Order Get(int id)
    {
        foreach (Order item in DataSource.Orders)
        {
            if (item.ID == id)//FIND
                return item;
        }
        throw new Exception("not exists");
    }
    /// <summary>
    /// delete object
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public void Delete(int id)
    {
        int exist = 0;
        foreach(Order item in DataSource.Orders)
        {
            if (item.ID == id)//FIND
            {
                exist = 1;
                DataSource.Orders.Remove(item);
                //for (; i < DataSource.Config.OrderIndex; i++)
                //{
                //    DataSource.Orders[i] = DataSource.Orders[i + 1];
                //}
                //DataSource.Config.OrderIndex--;
            }
        }
        if (exist == 0)
            throw new Exception("not exists");
    }
    /// <summary>
    /// update object
    /// </summary>
    /// <param name="o"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Order o)
    {
        bool exist=false;
    foreach (Order item in DataSource.Orders)
    {
        if (item.ID == o.ID)//FIND
        {
            exist = true;
            DataSource.Orders.Remove(item);
            DataSource.Orders.Add(o);
        }
    }
        if(!exist)
         throw new Exception("not exists");
    }
    /// <summary>
    /// get all objects
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Order> GetAll()
    {
        int i = 0;
        Order[] newOrders = new Order[DataSource.Orders.Count()];
        Order o = new Order();
        foreach (Order item in DataSource.Orders)
        {
            o.ID = item.ID;
            o.CustomerName = item.CustomerName;
            o.CustomerEmail = item.CustomerEmail;
            o.CustomerAdress = item.CustomerAdress;
            o.OrderDate = item.OrderDate;
            o.ShipDate = item.ShipDate;
            o.DeliveryDate = item.DeliveryDate;
            newOrders[i++] = o;
        }
        if (DataSource.Orders.Count() == 0)
            Console.WriteLine("there is no orders");
        return newOrders;
    }
}
