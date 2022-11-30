//
using DO;
namespace Dal;
using DalApi;
internal class DalOrder : IOrder
{
    /// <summary>
    /// add object
    /// </summary>
    /// <param name="o"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public  int Add(Order o)
    {
        int i = DataSource.Config.LastOrder;
        o.ID = i;
        foreach (var item in DataSource.Orders)
        {
            if (o.ID == item.ID)
                throw new Duplication("this order is already exist");
        }
        DataSource.Orders.Add(o);
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
        foreach (Order item in DataSource.Orders)
        {
            if (item.ID == id)
            { //FIND
                return item;
            }
        }
        throw new NotExist("not exists");
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
        Order order=new Order();
        foreach(Order item in DataSource.Orders)
        {
            if (item.ID == id)//FIND
            {

                exist = 1;
                order.ID = item.ID;
                order.OrderDate=item.OrderDate;
                order.ShipDate=item.ShipDate;
                order.CustomerEmail = item.CustomerEmail;
                order.CustomerName = item.CustomerName;
                order.CustomerAdress = item.CustomerAdress; 
                order.DeliveryDate = item.DeliveryDate; 

            }
        }
        if (exist == 0)
            throw new NotExist("not exists");
        else
            DataSource.Orders.Remove(order);

    }
    /// <summary>
    /// update object
    /// </summary>
    /// <param name="o"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Order o)
    {
        Order order = new Order();
        bool exist=false;
    foreach (Order item in DataSource.Orders)
    {
        if (item.ID == o.ID)//FIND
        {
            exist = true;
            order.ID = item.ID;
            order.OrderDate = item.OrderDate;
            order.ShipDate = item.ShipDate;
            order.CustomerEmail = item.CustomerEmail;
            order.CustomerName = item.CustomerName;
            order.CustomerAdress = item.CustomerAdress;
            order.DeliveryDate = item.DeliveryDate;

            }
    }
        if(!exist)
         throw new NotExist("not exists");
        else
        {
            DataSource.Orders.Remove(order);
            DataSource.Orders.Add(o);
        }
    }
    /// <summary>
    /// get all objects
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Order> GetAll()
    {
        List<Order> newOrders = new List<Order>();
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
            newOrders.Add(o);
        }
        if (DataSource.Orders.Count() == 0)
            Console.WriteLine("there is no orders");
        return newOrders;
    }
}
