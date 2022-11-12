
namespace Dal;
public class DalOrderItem
{
    public int AddOrderItem(OrderItem oI)
    {
        //check if there is place
        if (Config.orderItemIndex < DataSource.OrderItems.length)
        {
            int i = Config.LastOrderItem;
            DataSource.OrderItems[Config.OrderItemIndex] = oI;
            return i;
        }
        else
            throw new Exception("there is no place")
    }

    public OrderItem GetOrderItem(int id)
    {
        foreach (OrderItem item in DataSource.orderItems)
        {
            if (item.OrderItemID == id)//FIND
                return item;
        }
        throw new Exception("not exists");
    }
    public OrderItem GetOrderItemByIDS(int pId, int oId)
    {
        foreach (OrderItem item in DataSource.orderItems)
        {
            if (item.ProductID == pId && item.OrderID == oId)//FIND
                return item;
        }
        throw new Exception("not exists");
    }
    public OrderItem[] GetOrderItems()
    {

        OrderItem[] newOrderItems = new OrderItem[Config.OrderItemIndex]
        OrderItem oI = new OrderItem();
        int i = 0;
        foreach (OrderItem item in DataSource.OrderItems)
        {
            oI.ProductID = item.ProductID;
            oI.OrderID = item.OrderID;
            oI.Price = item.Price;
            oI.Amount = item.Amount;
            newOrderItems[i++] = oI;
        }
        return newOrderItems;
    }


    public OrderItem DeleteOrderItem(int id)
    {
        foreach (OrderItem item in DataSource.orderItems)
        {
            if (item.ID == id)//FIND
                item = null;
        }
        throw new Exception("not exists");

    }
    public void UpdateOrderItem(OrderItem oI)
    {
        foreach (Order item in DataSource.orderItems)
        {
            if (item.ID == oI.ID)
            { //FIND
                item = oI;
                return;
            }
        }
        throw new Exception("not exists");

    }


    public OrderItem GetProductsInOrder(int oIID)
    {
        OrderItem[] newOrderItems = new OrderItem[Config.OrderItemIndex]
               OrderItem oI = new OrderItem();
        int i = 0;
        foreach (OrderItem item in DataSource.orderItems)
        {
            if (item.OrderID == oIID)
            { //FIND
                oI.ProductID = item.ProductID;
                oI.OrderID = item.OrderID;
                oI.Price = item.Price;
                oI.Amount = item.Amount;
                newOrderItems[i++] = oI;
            }
        }
            if(i!=0)
                return newOrderItems;
        throw new Exception("no products in the order");
    }
}

