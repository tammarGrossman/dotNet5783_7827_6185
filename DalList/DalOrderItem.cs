using DO;
namespace Dal;
public class DalOrderItem
{
    /// <summary>
    /// add object
    /// </summary>
    /// <param name="oI"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public int AddOrderItem(OrderItem oI)
    {
        //check if there is place
        if (DataSource.Config.OrderItemIndex < DataSource.OrderItems.Length)
        {
            int i = DataSource.Config.LastOrderItem;
            oI.OrderItemID = i;
            DataSource.OrderItems[DataSource.Config.OrderItemIndex++] = oI;
            DalProduct dp = new DalProduct();
            Product p = dp.GetProduct(oI.ProductID);
            p.InStock -= oI.Amount;
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
    public OrderItem GetOrderItem(int id)
    {
        foreach (OrderItem item in DataSource.OrderItems)
        {
            if (item.OrderItemID == id)//FIND
                return item;
        }
        throw new Exception("not exists");
    }
    /// <summary>
    /// get object by ids
    /// </summary>
    /// <param name="pId"></param>
    /// <param name="oId"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public OrderItem GetOrderItemByIDS(int pId, int oId)
    {
        foreach (OrderItem item in DataSource.OrderItems)
        {
            if (item.ProductID == pId && item.OrderID == oId)//FIND
                return item;
        }
        throw new Exception("not exists");
    }
    /// <summary>
    /// get all objects
    /// </summary>
    /// <returns></returns>
    public OrderItem[] GetOrderItems()
    {
        OrderItem[] newOrderItems = new OrderItem[DataSource.Config.OrderItemIndex];
        OrderItem oI = new OrderItem();
        for (int i = 0; i < DataSource.Config.OrderItemIndex; i++)
        {
            oI.OrderItemID = DataSource.OrderItems[i].OrderItemID;
            oI.ProductID = DataSource.OrderItems[i].ProductID;
            oI.OrderID = DataSource.OrderItems[i].OrderID;
            oI.Price = DataSource.OrderItems[i].Price;
            oI.Amount = DataSource.OrderItems[i].Amount;
            newOrderItems[i] = oI;
        }
        if(DataSource.Config.OrderItemIndex==0)
            Console.WriteLine("there is no item in the order");
        return newOrderItems;
    }
    /// <summary>
    /// delete object
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>

    public void DeleteOrderItem(int id)
    {
        int exist = 0;
        for (int i = 0; i < DataSource.Config.OrderItemIndex; i++)
        {
            if (DataSource.OrderItems[i].OrderItemID == id)//FIND
            {
                exist = 1;
                for (; i < DataSource.Config.OrderItemIndex; i++)
                {
                    DataSource.OrderItems[i] = DataSource.OrderItems[i + 1];
                }
                DataSource.Config.OrderItemIndex--;
            }
        }
        if (exist == 0)
            throw new Exception("not exists");
    }
    /// <summary>
    /// update object
    /// </summary>
    /// <param name="oI"></param>
    /// <exception cref="Exception"></exception>
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

    /// <summary>
    /// get objects in order
    /// </summary>
    /// <param name="oIID"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public OrderItem[] GetProductsInOrder(int oID)
    {
        OrderItem[] newOrderItems = new OrderItem[DataSource.Config.OrderItemIndex];
        OrderItem oI = new OrderItem();
        int i = 0;
        for (; i < DataSource.Config.OrderItemIndex; i++)
        {
            if (DataSource.OrderItems[i].OrderID == oID)
            { //FIND
                oI.OrderItemID = DataSource.OrderItems[i].OrderItemID;
                oI.ProductID = DataSource.OrderItems[i].ProductID;
                oI.OrderID = DataSource.OrderItems[i].OrderID;
                oI.Price = DataSource.OrderItems[i].Price;
                oI.Amount = DataSource.OrderItems[i].Amount;
                newOrderItems[i] = oI;
            }
        }
            if(i!=0)
                return newOrderItems;
            else
               throw new Exception("no products in the order");
    }
}