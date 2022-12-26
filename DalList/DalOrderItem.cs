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
        foreach (var item in DataSource.orderItems)
        {
            if (oI.OrderItemID == item?.OrderItemID)
                throw new Duplication(oI.OrderItemID,"order item");
        }
            DataSource.orderItems.Add(oI);
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
        foreach (OrderItem? item in DataSource.orderItems)
        {
            if (((item?.OrderItemID)??0) == id)//FIND
                return item ?? throw new NotExist(id,"order item");
        }
        throw new NotExist(id, "order item");
    }
    /// <summary>
    /// get object by ids
    /// </summary>
    /// <param name="pId"></param>
    /// <param name="oId"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public OrderItem GetOrderItemByIDS(int pId, int oId, Func<OrderItem?, bool>? Condition = null)
    {
            foreach (OrderItem? item in DataSource.orderItems)
            {
                if (Condition == null)
                {
                    if (item?.ProductID == pId && item?.OrderID == oId)//FIND
                        return item ?? throw new NotExist((item?.OrderItemID)??0,"order item");
                }
                else
                {
                    if (Condition(item))//FIND
                        return item ?? throw new NotExist((item?.OrderItemID) ?? 0, "order item");
                }
            }
        throw new NotExist(0,"order item");
    }
    /// <summary>
    /// get all objects
    /// </summary>
    /// <returns></returns>
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?,bool>? Condition=null)
    {
        List<OrderItem?> newOrderItems = new List<OrderItem?>();
        OrderItem oI = new OrderItem();
        foreach (OrderItem? item in DataSource.orderItems)
        {
            oI.OrderItemID = (item?.OrderItemID) ?? 0;
            oI.ProductID = (item?.ProductID)??0;
            oI.OrderID = (item?.OrderID)??0;
            oI.Price = (item?.Price)??0;
            oI.Amount = (item?.Amount)??0;
            newOrderItems.Add(oI);
        }
        if(DataSource.orderItems.Count() == 0)
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
        foreach (OrderItem? item in DataSource.orderItems)
        {
            if (item?.OrderItemID == id)//FIND
            {
                exist = 1;
                orderItem.OrderItemID=(item?.OrderItemID)??0;
                orderItem.ProductID = (item?.ProductID)??0;
                orderItem.OrderID = (item?.OrderID) ?? 0;
                orderItem.Price = (item?.Price)??0;
                orderItem.Amount = (item?.Amount)??0;
            }
        }
        if (exist == 0)
            throw new NotExist(id,"order item");
        else
            DataSource.orderItems.Remove(orderItem);

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
        foreach (OrderItem? item in DataSource.orderItems)
        {
            if (item?.OrderItemID == oI.OrderItemID)
            { //FIND
                exist = true;
                DalProduct dp = new DalProduct();
                Product p = dp.Get(oI.ProductID);
                p.InStock -= oI.Amount;
                orderItem.OrderItemID = (item?.OrderItemID)??0;
                orderItem.ProductID =( item?.ProductID) ?? 0;
                orderItem.OrderID = (item?.OrderID) ?? 0;
                orderItem.Price = (item?.Price) ?? 0;
                orderItem.Amount = (item?.Amount) ?? 0;
            }
        }
        if(!exist)
        throw new NotExist(oI.OrderItemID,"order item");
        else
        {
            DataSource.orderItems.Remove(orderItem);
            DataSource.orderItems.Add(oI);

        }
    }
    /// <summary>
    /// get objects in order
    /// </summary>
    /// <param name="oIID"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public IEnumerable<OrderItem?> GetProductsInOrder(int oID, Func<OrderItem?, bool>? Condition = null)
    {
        List<OrderItem?> newOrderItems=new List<OrderItem?>();
        OrderItem oI = new OrderItem();
        int i = 0;
        foreach (OrderItem? item in DataSource.orderItems)
            {
            if (item?.OrderID == oID)
            { //FIND
                i++;
                oI.OrderItemID = (item?.OrderItemID) ?? 0;
                oI.ProductID = (item?.ProductID) ?? 0;
                oI.OrderID = (item?.OrderID) ?? 0;
                oI.Price = (item?.Price) ?? 0;
                oI.Amount = (item?.Amount) ?? 0;
                newOrderItems.Add(oI);
            }
        }
        if (i != 0)
            return newOrderItems;
        else
            throw new NotExist(oID,"order item");
    }
    public OrderItem GetByCon(Func<OrderItem?, bool>? Condition = null)
    {
        foreach (OrderItem? item in DataSource.orderItems)
        {
            if (Condition(item))
                return item ?? throw new NotExist((item?.OrderItemID) ?? 0, "order item");
        }
        throw new NotExist(0,"order item");
    }
}