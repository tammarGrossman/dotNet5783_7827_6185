
using DO;
namespace Dal;
static internal class DataSource
{
    static readonly Random rand;// random
    static bool start = false;//start project
    static internal Order[] Orders = new Order[100];
    static internal OrderItem[] OrderItems = new OrderItem[200];
    static internal Product[] Products = new Product[50];
    static DataSource()
    {
        rand = new Random();
        s_Initialize();
    }
    private static void s_Initialize()
    {
        createOrder();
        createProduct();
        createOrderItem();
    }
    /// <summary>
    /// initilize orders
    /// </summary>
    private static void createOrder()
    {
        string[] customersNames = new string [] { "moshe choen", "ruth choen", "david levi", "ben bash", "eli lev", "dana shal", "ron cahna", "esti shalom", "tammar choen", "michal levi", "shir gold", "dan fisher", "shimon yzchaki", "yair choen", "ruth dagush", "chen frid", "mini lev", "zipi bash", "dini levi", "tali choen", };
        string[] customersEmails = new string[] { "j123@gmail.com", "r1s@gmail.com" ,"y23d@gmail.com", "bb@gmail.com" ,"dd123@gmail.com", "g3@gmail.com" ,"als123@gmail.com" ,"mc@gmail.com" ,"sh44@gmail.com" ,"df666@gmail.com" ,"sh9898@gmail.com" ,"ala123@gmail.com" ,"ney232@gmail.com" ,"ni2003@gmail.com", "zp67@gmail.com" ,"tt54@gmail.com", "ut6@gmail.com" ,"th9009@gmail.com" ,"kj09@gmail.com" ,"opopo@gmail.com" };
        string[] customersAdresses = new string[] { "pinkas 10", "zfat 12", "tveria 10", "chevron 12", "herzog 9", "hertzel 5", "radzemin 7", "perel 22", "awlochamim 7", "rambam 5", "sokolov 25", "walfson 12", "kaplan 8", "anilevith 10", "jerusalem 55", "gotliv 14", "rozovski 10", "daniel 2", "moaliver 5", "bagno 20"};

        for (int i = 0; i < 20 ; i++)
        {
            Config.OrderIndex++;
            Orders[i] = new Order();
            Orders[i].OrderDate = DateTime.MinValue;
            var ts = new TimeSpan(rand.Next(1, 10), rand.Next(0, 24), rand.Next(0, 60), rand.Next(0, 60));

            if (i < 16)
            {
                Orders[i].ShipDate = DateTime.MinValue + ts;
            }

            if (i < 10)
            {
                ts.Add(new TimeSpan(rand.Next(1, 10), rand.Next(0, 24), rand.Next(0, 60), rand.Next(0, 60)));
                Orders[i].DeliveryDate = Orders[i].ShipDate + ts;
            }
            Orders[i].ID = Config.LastOrder;
            Orders[i].CustomerAdress = customersAdresses[i];
            Orders[i].CustomerEmail = customersEmails[i];
            Orders[i].CustomerName = customersNames[i];
        }
    }
    /// <summary>
    /// initilize products
    /// </summary>
    private static void createProduct()
    {
        string[] productsNames = new string[] { "shirt", "skirt", "tights", "bottle", "shoes", "dumbbells", "mat", "rubberBand", "ball", "socks" };
        int[] categories = new int[] { 2, 1, 1, 4, 3, 4, 4, 4, 4, 2 };

        for (int i = 1; i < 11; i++)
        {
            Config.ProductIndex++;
            Products[i - 1] = new Product
            {
                ID = i * 1000000,
                Name = productsNames[i - 1],
                Price = rand.Next(20, 201),
                InStock = rand.Next(0, 50),
                Category_ = (Category)Enum.ToObject(typeof(Category), categories[i - 1])
            };
        }
    }
    /// <summary>
    /// initilize order items
    /// </summary>
private static void createOrderItem()
{
        int iOrder = 0;
        int iProduct = 1;
        for (int i = 0; i < 40 ; i++)
        {
            Config.OrderItemIndex++;
            if (iProduct == 11)
                iProduct = 1;
            if(iOrder==20)
                iOrder = 0;
            OrderItems[i] = new OrderItem
            {
                OrderItemID =Config.LastOrderItem,
                ProductID=(iProduct++)*1000000,
                OrderID= iOrder++,
                Price=rand.Next(50,201),
                Amount=rand.Next(20,51)
            };
        }
}
    static internal class Config
    {
        //index and run number
        internal static int OrderIndex= 0;
        internal static int ProductIndex = 0;
        internal static int OrderItemIndex = 0;
        private static int lastOrder=0;
        private static int lastOrderItem=0;
        static Config()
        {
            start = true;
        }
        public static int LastOrder { get => lastOrder++; }
        public static int LastOrderItem { get=>lastOrderItem++;}


    }
}