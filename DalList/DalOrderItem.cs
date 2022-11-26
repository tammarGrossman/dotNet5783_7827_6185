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
        //check if there is place
        //if (DataSource.Config.OrderItemIndex < DataSource.OrderItems.Length)
        //{
            int i = DataSource.Config.LastOrderItem;
            oI.OrderItemID = i;
            DataSource.OrderItems.Add(oI);
            DalProduct dp = new DalProduct();
            Product p = dp.Get(oI.ProductID);
            p.InStock -= oI.Amount;
            return i;
        //}
        //else
            //throw new Exception("there is no place");
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
        int exist = 0;
        foreach (OrderItem item in DataSource.OrderItems)
        {
            if (item.OrderItemID == id)//FIND
            {
                exist = 1;
                //for (; i < DataSource.Config.OrderItemIndex; i++)
                //{
                //    DataSource.OrderItems[i] = DataSource.OrderItems[i + 1];
                //}
                //DataSource.Config.OrderItemIndex--;
                DalProduct dp = new DalProduct();
                Product p = dp.Get(item.ProductID);
                p.InStock += item.Amount;
                DataSource.OrderItems.Remove(item); 
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
    public void Update(OrderItem oI)
    {
        bool exist = false;
        foreach (OrderItem item in DataSource.OrderItems)
        {
            if (item.OrderItemID == oI.OrderItemID)
            { //FIND
                exist = true;
                DalProduct dp = new DalProduct();
                Product p = dp.Get(oI.ProductID);
                p.InStock -= oI.Amount;
                DataSource.OrderItems.Remove(item);  
                DataSource.OrderItems.Add(oI);
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
    public IEnumerable<OrderItem> GetProductsInOrder(int oID)
    {
        List<OrderItem> newOrderItems=new List<OrderItem>();
        OrderItem oI = new OrderItem();
        int i = 0;
        foreach (OrderItem item in DataSource.OrderItems)
            {
            if (item.OrderID == oID)
            { //FIND
                oI.OrderItemID = item.OrderItemID;
                oI.ProductID = item.ProductID;
                oI.OrderID = item.OrderID;
                oI.Price = item.Price;
                oI.Amount = item.Amount;
                newOrderItems.Add(oI);
            }
        }
            if(i!=0)
                return newOrderItems;
            else
               throw new Exception("no products in the order");
    }
}