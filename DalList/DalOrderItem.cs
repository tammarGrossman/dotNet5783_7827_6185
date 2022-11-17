using DO;
namespace Dal;
public class DalOrderItem
{
    public int AddOrderItem(OrderItem oI)
    {
        //check if there is place
        if (DataSource.Config.OrderItemIndex < DataSource.OrderItems.Length)
        {
            int i = DataSource.Config.LastOrderItem;
            oI.OrderItemID = i;
            DataSource.OrderItems[DataSource.Config.OrderItemIndex++] = oI;
            return i;
        }
        else
            throw new Exception("there is no place");
    }
    public OrderItem GetOrderItem(int id)
    {
        foreach (OrderItem item in DataSource.OrderItems)
        {
            if (item.OrderItemID == id)//FIND
                return item;
        }
        throw new Exception("not exists");
    }
    public OrderItem GetOrderItemByIDS(int pId, int oId)
    {
        foreach (OrderItem item in DataSource.OrderItems)
        {
            if (item.ProductID == pId && item.OrderID == oId)//FIND
                return item;
        }
        throw new Exception("not exists");
    }
    public OrderItem[] GetOrderItems()
    {

        OrderItem[] newOrderItems = new OrderItem[DataSource.Config.OrderItemIndex];
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
        for (int i = 0; i < DataSource.Config.OrderItemIndex; i++)
            if (DataSource.OrderItems[i].OrderItemID == id)//FIND
            {
                for (; i < DataSource.Config.OrderItemIndex; i++)
                {
                    DataSource.OrderItems[i] = DataSource.OrderItems[i + 1];
                }
                DataSource.Config.OrderItemIndex--;
            }
        throw new Exception("not exists");
    }
    public void UpdateOrderItem(OrderItem oI)
    {
        bool exist = false;
        for (int i = 0; i < DataSource.Config.OrderItemIndex; i++)
        {
            if (DataSource.OrderItems[i].OrderItemID == oI.OrderItemID)
            { //FIND
                exist = true;
                DataSource.OrderItems[i] = oI;
            }
        }
        if(!exist)
        throw new Exception("not exists");
    }


    public OrderItem[] GetProductsInOrder(int oIID)
    {
        OrderItem[] newOrderItems = new OrderItem[DataSource.Config.OrderItemIndex];
        OrderItem oI = new OrderItem();
        int i = 0;
        foreach (OrderItems item in DataSource.OrderItems)
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

