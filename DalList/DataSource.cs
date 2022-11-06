
using DO;
using System.Runtime.CompilerServices;

namespace Dal;

static internal class DataSource
{
   static readonly int rand;
    static internal Order[] Orders=new Order[100];
    static internal OrderItem[] OrderItems=new OrderItem[200];
    static internal Product[] Products=new Product[50];
    static DataSource()
    {
       s_Initialize();
    }
    /// <summary>
    /// a function that push an object to its array if it is possible
    /// </summary>
    /// <param name="p"></param>
    static private void AddProduct(Product p)
    {

    }
    /// <summary>
    /// a function that push an object to its array if it is possible
    /// </summary>
    /// <param name="o"></param>
    static private void AddOrder(Order o)
    {

    }
    /// <summary>
    /// a function that push an object to its array if it is possible
    /// </summary>
    /// <param name="oI"></param>
    static private void AddOrderItem(Product oI)
    {

    }
    private static s_Initialize()
    {
        AddOrder(new Order());
        AddProduct(new Product());
        AddOrderItem(new OrderItem());
    }

}
