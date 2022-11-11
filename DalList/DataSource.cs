
using DO;
using System.Runtime.CompilerServices;
namespace Dal;
static internal class DataSource
{
    static readonly int rand;
    static internal Order[] Orders = new Order[100];
    static internal OrderItem[] OrderItems = new OrderItem[200];
    static internal Product[] Products = new Product[50];
    private static s_Initialize()
    {
        createOrder();
        createProduct();
        createOrderItem();
    }
    static DataSource()
    {
        rand = new Random();
        s_Initialize();
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
        static internal int OrderIndex= 0;
        static internal int ProductIndex= 0;
        static internal int OrderItemIndex= 0;
        static private int lastOrder=0;
        static private int lastOrderItem=0;
        public int LastOrder { get=>return lastOrder++;}
        public int LastOrderItem { get=>lastOrderItem++;}
    }
}
