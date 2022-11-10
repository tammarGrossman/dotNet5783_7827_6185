
using DO;
using System.Runtime.CompilerServices;

namespace Dal;

static internal class DataSource
{
    static readonly int rand;
    static internal Order[] Orders = new Order[100];
    static internal OrderItem[] OrderItems = new OrderItem[200];
    static internal Product[] Products = new Product[50];
    static DataSource()
    {
        rand = new Random();
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
    private static void createOrder()
    {
        for (int i = 0; i < Orders.length; i++)
        {
            Orders[i] = new Order();
        }
    }
    private static void createProduct()
    {
        for (int i = 0; i < Products.length; i++)
        {
            Products[i] = new Product();
        }
    }
    private static void createOrderItem()
    {
        for (int i = 0; i < OrderItems.length; i++)
        {
            OrderItems[i] = new OrderItem();
        }
    }

    static internal class Config
    {
        static internal OrderIndex= 0;
        static internal ProductIndex= 0;
        static internal OrderItemIndex= 0;
        static private lastOrder=1;
        static private lastOrderItem=1;
        public int LastOrder { get=>return lastOrder++; set; }
        public int LastOrderItem { get=>lastOrderItem++; set; }
    }
}
