using DO;
namespace Dal;
using DalApi;
internal class DalOrderItem : IOrderItem
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

        if (OrderItemExist(oI.OrderItemID))
            throw new Duplication(oI.OrderItemID, "order item");

        DataSource.orderItems.Add(oI);
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
        if (OrderItemExist(id))//found
            DataSource.orderItems.Find(orderI => orderI?.OrderItemID == id);
        throw new NotExist(id, "order item");
    }

    /// <summary>
    /// function that check if the order item exist 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private bool OrderItemExist(int id)
    {
        return DataSource.orderItems.Any(orderI => orderI?.OrderItemID == id);
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
        return DataSource.orderItems.FirstOrDefault(orderItem => orderItem?.OrderID == oId && orderItem?.ProductID == pId) ?? throw new NotExist(0,"order item"); ;
    }
    /// <summary>
    /// get all objects
    /// </summary>
    /// <returns></returns>
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? Condition = null)
    {

        if (Condition != null)
            return from orderItem in DataSource.orderItems
                   where Condition(orderItem)
                   select orderItem;

        return from orderItem in DataSource.orderItems
               select orderItem; ;
    }

    /// <summary>
    /// delete object
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>

    public void Delete(int id)
    {
        int count = DataSource.orderItems.RemoveAll(orderItem => orderItem?.OrderItemID == id);

        if (count == 0)
            throw new NotExist(id, "order item");
    }

    /// <summary>
    /// update object
    /// </summary>
    /// <param name="oI"></param>
    /// <exception cref="Exception"></exception>
    public void Update(OrderItem oI)
    {
        int count = DataSource.orderItems.RemoveAll(OrderItem => OrderItem?.OrderItemID == oI.OrderItemID);
        if (count == 0)
            throw new NotExist(oI.OrderItemID, "order item");

        DataSource.orderItems.Add(oI);

    }
/// <summary>
/// get object by condition
/// </summary>
/// <param name="Condition"></param>
/// <returns></returns>
/// <exception cref="NotExist"></exception>
    public OrderItem GetByCon(Func<OrderItem?, bool>? Condition )
    {
        return DataSource.orderItems.Find(x => Condition!(x)) ??
         throw new NotExist(0, "order item");
       
    }

    /// <summary>
    /// get objects in order
    /// </summary>
    /// <param name="oIID"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public IEnumerable<OrderItem?> GetProductsInOrder(int oID)
    {
        List<OrderItem?> OrderItemsList= DataSource.orderItems.FindAll(order => order?.OrderID == oID);
        if( OrderItemsList.Count()>0)
            return OrderItemsList;
        throw new NotExist(oID, "order item");
    }
}