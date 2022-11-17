
using DO;
namespace Dal;

public class DalOrder
{

    public  int AddOrder(Order o)
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

    public Order GetOrder(int id)
    {
        foreach (Order item in DataSource.Orders)
        {
            if (item.ID == id)//FIND
                return item;
        }
        throw new Exception("not exists");
    }
    public Order DeleteOrder(int id)
    {
        for (int i = 0; i < DataSource.Config.OrderIndex; i++)
            if (DataSource.Orders[i].ID == id)//FIND
            {
                for (; i < DataSource.Config.OrderIndex; i++)
                {
                    DataSource.Orders[i] = DataSource.Orders[i + 1];
                }
                DataSource.Config.OrderIndex--;
            }
        throw new Exception("not exists");
    }
    public void UpdateOrder(Order o)
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

    public Order[] GetOrders()
    {
        Order[] newOrders = new Order[DataSource.Config.OrderIndex];
             Order o = new Order();
        int i = 0;
        foreach (Order item in DataSource.Orders)
        {
            o.ID = item.ID;
            o.CustomerName = item.CustomerName;
            o.CustomerEmail = item.CustomerEmail;
            o.CustomerAdress = item.CustomerAdress;
            o.OrderDate = item.OrderDate;
            o.ShipDate = item.ShipDate;
            o.DeliveryDdate = item.DeliveryDate;
            newOrders[i++] = o;
        }
        return newOrders;
    }
}
