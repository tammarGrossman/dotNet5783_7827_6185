

namespace Dal;

public class DalOrder
{

    public  int AddOrder(Order o)
    {
        if (Config.orderItemIndex < DataSource.Orders.length)
        {

            //check if there is place
            int i = Config.LastOrder;
            o.ID = i;
            DataSource.Orders[DataSource.Config.OrderIndex++] = o;
            return i;
        }
        else
            throw new Exception("there is no place")
    }

    public Order GetOrder(int id)
    {
        foreach (Order item in DataSource.orders)
        {
            if (item.ID == id)//FIND
                return item;
        }
        throw new Exception("not exists");
    }
    public Order DeleteOrder(int id)
    {
        foreach (Order item in DataSource.orders)
        {
            if (item.ID == id)//FIND
                item = null;
        }
        throw new Exception("not exists");

    }
    public void UpdateOrder(Order o)
    {
        foreach (Order item in DataSource.orders)
        {
            if (item.ID == o.ID)
            { //FIND
                item = o;
                return;
            }
        }
        throw new Exception("not exists");

    }

    public Order[] GetOrders()
    {
        Order[] newOrders = new Order[DataSource.Config.OrderIndex]
             Order o = new Order();
        int i = 0;
        foreach (Order item in DataSource.orders)
        {
            o.ID = item.ID;
            o.CustomerName = item.CustomerName;
            o.CustomerEmail = item.CustomerEmail;
            o.CustomerAdress = item.CustomerAdress;
            o.OrderDate = item.OrderDate;
            o.ShipDate = item.ShipDate;
            o.DeliveryDdate = item.DeliveryDdate;
            newOrders[i++] = o;
        }
        return newOrders;
    }
}


}
