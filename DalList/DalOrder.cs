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
        if (DataSource.Config.OrderIndex < DataSource.Orders.Length)
        {
            //check if there is place
            int i = DataSource.Config.LastOrder;
            o.ID = i;
            DataSource.Orders[DataSource.Config.OrderIndex++] = o;
            return i;
        }
        else
            throw new Exception("there is no place");
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
        for (int i = 0; i < DataSource.Config.OrderIndex; i++)
        {
            if (DataSource.Orders[i].ID == id)//FIND
            {
                exist = 1;
                for (; i < DataSource.Config.OrderIndex; i++)
                {
                    DataSource.Orders[i] = DataSource.Orders[i + 1];
                }
                DataSource.Config.OrderIndex--;
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
        for (int i = 0; i < DataSource.Config.OrderIndex; i++)
            if (DataSource.Orders[i].ID == o.ID)//FIND
            {
                exist = true;
                DataSource.Orders[i] = o;
            }
        if(!exist)
         throw new Exception("not exists");
    }
    /// <summary>
    /// get all objects
    /// </summary>
    /// <returns></returns>
    public Order[] GetAll()
    {
        Order[] newOrders = new Order[DataSource.Config.OrderIndex];
        Order o = new Order();
        for (int i = 0; i < DataSource.Config.OrderIndex; i++)
        {
            o.ID = DataSource.Orders[i].ID;
            o.CustomerName = DataSource.Orders[i].CustomerName;
            o.CustomerEmail = DataSource.Orders[i].CustomerEmail;
            o.CustomerAdress = DataSource.Orders[i].CustomerAdress;
            o.OrderDate = DataSource.Orders[i].OrderDate;
            o.ShipDate = DataSource.Orders[i].ShipDate;
            o.DeliveryDate = DataSource.Orders[i].DeliveryDate;
            newOrders[i] = o;
        }
        if (DataSource.Config.OrderIndex == 0)
            Console.WriteLine("there is no orders");
        return newOrders;
    }
}
