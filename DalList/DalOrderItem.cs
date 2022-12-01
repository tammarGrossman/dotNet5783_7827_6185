using DO;
namespace Dal;
using DalApi;
internal  class DalOrderItem : IOrderItem
{
    /// <summary>
    /// add object
    /// </summary>
    /// <param name="oI"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public int Add(OrderItem oI)
    {
       
        int i = DataSource.Config.LastOrderItem;
        oI.OrderItemID = i;
        foreach (var item in DataSource.OrderItems)
        {
            if (oI.OrderItemID == item.OrderItemID)
                throw new Duplication("this order item is already exist");

        }
            DataSource.OrderItems.Add(oI);
            DalProduct dp = new DalProduct();
            Product p = dp.Get(oI.ProductID);
            return i;
    }
    /// <summary>
    /// get object
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public OrderItem Get(int id)
    {
        foreach (OrderItem item in DataSource.OrderItems)
        {
            if (item.OrderItemID == id)//FIND
                return item;
        }
        throw new NotExist("not exists");
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
        throw new NotExist("not exists");
    }
    /// <summary>
    /// get all objects
    /// </summary>
    /// <returns></returns>
    public IEnumerable<OrderItem> GetAll()
    {
        int i = 0;
        List<OrderItem> newOrderItems = new List<OrderItem>();
        OrderItem oI = new OrderItem();
        foreach (OrderItem item in DataSource.OrderItems)
        {
            oI.OrderItemID = item.OrderItemID;
            oI.ProductID = item.ProductID;
            oI.OrderID = item.OrderID;
            oI.Price = item.Price;
            oI.Amount = item.Amount;
            newOrderItems.Add(oI);
        }        
        if(DataSource.OrderItems.Count() == 0)
            Console.WriteLine("there is no item in the order");
        return newOrderItems;
    }
    /// <summary>
    /// delete object
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>

    public void Delete(int id)
    {
        OrderItem orderItem = new OrderItem();
        int exist = 0;
        foreach (OrderItem item in DataSource.OrderItems)
        {
            if (item.OrderItemID == id)//FIND
            {
                exist = 1;
                DalProduct dp = new DalProduct();
                Product p = dp.Get(item.ProductID);
                orderItem.OrderItemID=item.OrderItemID;
                orderItem.ProductID = item.ProductID;
                orderItem.OrderID = item.OrderID;
                orderItem.Price = item.Price;
                orderItem.Amount = item.Amount;
            }
        }
        if (exist == 0)
            throw new NotExist("not exists");
        else
            DataSource.OrderItems.Remove(orderItem);

    }
    /// <summary>
    /// update object
    /// </summary>
    /// <param name="oI"></param>
    /// <exception cref="Exception"></exception>
    public void Update(OrderItem oI)
    {
        OrderItem orderItem = new OrderItem();
        bool exist = false;
        foreach (OrderItem item in DataSource.OrderItems)
        {
            if (item.OrderItemID == oI.OrderItemID)
            { //FIND
                exist = true;
                DalProduct dp = new DalProduct();
                Product p = dp.Get(oI.ProductID);
                p.InStock -= oI.Amount;
                orderItem.OrderItemID = item.OrderItemID;
                orderItem.ProductID = item.ProductID;
                orderItem.OrderID = item.OrderID;
                orderItem.Price = item.Price;
                orderItem.Amount = item.Amount;
            }
        }
        if(!exist)
        throw new NotExist("not exists");
        else
        {
            DataSource.OrderItems.Remove(orderItem);
            DataSource.OrderItems.Add(oI);

        }
    }
    /// <summary>
    /// get objects in order
    /// </summary>
    /// <param name="oIID"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public IEnumerable<OrderItem> GetProductsInOrder(int oID)
    {
        List<OrderItem> newOrderItems=new List<OrderItem>();
        OrderItem oI = new OrderItem();
        int i = 0;
        foreach (OrderItem item in DataSource.OrderItems)
            {
            if (item.OrderID == oID)
            { //FIND
                i++;
                oI.OrderItemID = item.OrderItemID;
                oI.ProductID = item.ProductID;
                oI.OrderID = item.OrderID;
                oI.Price = item.Price;
                oI.Amount = item.Amount;
                newOrderItems.Add(oI);
            }
        }
        if (i != 0)
            return newOrderItems;
        else
            throw new NotExist("no products in the order");
    }
}