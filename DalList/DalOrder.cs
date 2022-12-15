
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
        foreach (var item in DataSource.Orders)
        {
            if (o.ID == item?.ID)
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
        foreach (Order? item in DataSource.Orders)
        {
            if (item?.ID == id)
            { //FIND
                return item ?? throw new NotExist("not exists");
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
        Order order = new Order();
        foreach (Order? item in DataSource.Orders)
        {
            if (item?.ID == id)//FIND
            {

                exist = 1;
                order.ID = (item?.ID)??0;
                order.OrderDate = item?.OrderDate;
                order.ShipDate = item?.ShipDate;
                order.CustomerEmail = item?.CustomerEmail;
                order.CustomerName = item?.CustomerName;
                order.CustomerAdress = item?.CustomerAdress;
                order.DeliveryDate = item?.DeliveryDate;

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
        bool exist = false;
        foreach (Order? item in DataSource.Orders)
        {
            if (item?.ID == o.ID)//FIND
            {
                exist = true;
                order.ID = (item?.ID)??0;
                order.OrderDate = item?.OrderDate;
                order.ShipDate = item?.ShipDate;
                order.CustomerEmail = item?.CustomerEmail;
                order.CustomerName = item?.CustomerName;
                order.CustomerAdress = item?.CustomerAdress;
                order.DeliveryDate = item?.DeliveryDate;

            }
        }
        if (!exist)
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
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? Condition = null)
    {
        Order o = new Order();

        List<Order?> newOrders = new List<Order?>();

        foreach (Order? item in DataSource.Orders)
        {
            if (Condition == null)
            {
                o.ID = (item?.ID) ?? 0;
                o.CustomerName = (item?.CustomerName);
                o.CustomerEmail = item?.CustomerEmail;
                o.CustomerAdress = item?.CustomerAdress;
                o.OrderDate = item?.OrderDate;
                o.ShipDate = item?.ShipDate;
                o.DeliveryDate = item?.DeliveryDate;
                newOrders.Add(o);
            }
            else
            {
                if (Condition(item))
                {
                    o.ID = (item?.ID) ?? 0;
                    o.CustomerName = (item?.CustomerName);
                    o.CustomerEmail = item?.CustomerEmail;
                    o.CustomerAdress = item?.CustomerAdress;
                    o.OrderDate = item?.OrderDate;
                    o.ShipDate = item?.ShipDate;
                    o.DeliveryDate = item?.DeliveryDate;
                    newOrders.Add(o);
                }
            }
        }
        if (DataSource.Orders.Count() == 0)
            Console.WriteLine("there is no orders");
        return newOrders;
    }
    public Order GetByCon(Func<Order?, bool>? Condition = null)
    {
        //return from order in DataSource.Orders
        //       where Condition(order)
        //       select order??throw new NotExist("not exists");
        foreach (Order? item in DataSource.Orders)
        {
            if (Condition(item))
                return item ?? throw new NotExist("not exists");
        }
        throw new NotExist("not exists");
    }
}