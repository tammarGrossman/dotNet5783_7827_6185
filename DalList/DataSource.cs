
using DO;
using System;
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
    private static s_Initialize()
    {
        createOrder();
        createProduct();
        createOrderItem();
    }
   
    private static void createOrder()
    {
        string []customersNames=new string {"moshe choen", "ruth choen", "david levi", "ben bash", "eli lev", "dana shal", "ron cahna", "esti shalom", "tammar choen", "michal levi", "shir gold", "dan fisher", "shimon yzchaki", "yair choen", "ruth dagush", "chen frid", "mini lev", "zipi bash", "dini levi", "tali choen", }
        string[] customersEmails = new string {"j123@gmail.com", "r1s@gmail.com" "y23d@gmail.com" "bb@gmail.com" "dd123@gmail.com" "g3@gmail.com" "als123@gmail.com" "mc@gmail.com" "sh44@gmail.com" "df666@gmail.com" "sh9898@gmail.com" "ala123@gmail.com" "ney232@gmail.com" "ni2003@gmail.com" "zp67@gmail.com" "tt54@gmail.com" "ut6@gmail.com" "th9009@gmail.com" "kj09@gmail.com" "opopo@gmail.com" }
        string[] customersAdresses = new string {"pinkas 10", "zfat 12", "tveria 10", "chevron 12", "herzog 9", "hertzel 5", "radzemin 7", "perel 22", "awlochamim 7", "rambam 5", "sokolov 25", "walfson 12", "kaplan 8", "anilevith 10", "jerusalem 55", "gotliv 14", "rozovski 10", "daniel 2", "moaliver 5", "bagno 20", }

        for (int i = 0; i < 20 ; i++)
        {
            Orders[i] = new Order();
            Orders[i].OrderDate = DateTime.MinValue;
            ts = new TimeSpan(rand.Next(1, 10), rand.Next(0, 24), rand.Next(0, 60), rand.Next(0, 60));

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
    private static void createProduct()
    {
        string[] productsNames = new string{ "shirt", "skirt", "tights", "bottle", "shoes", "dumbbells", "mat", "rubberBand", "ball", "socks" };
        int[] categories = new int { 1, 2, 3 };
        
        for (int i = 1; i < 11 ; i++)
        {
            Products[i - 1] = new Product {
                ID = i * 1000000,
                Name = productsNames[i - 1],
                Price = rand.Next(20, 201),
                InStock = rand.Next(0, 50),
                Category = (Enums.Category)Enum.Parse(typeof(Enums.Category), categories[i-1]);
        };
    }
}
private static void createOrderItem()
{
        int iOrder = 1;
        int iProduct = 1000000;
        for (int i = 0; i < 40 ; i++)
        {
            if (iProduct == 10000000)
                iProduct = 1;
            if(iOrder==21)
                iOrder = 1;
            OrderItems[i] = new OrderItem() {
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
        internal static int OrderIndex= 0;
        internal static int ProductIndex = 0;
        internal static int OrderItemIndex = 0;
        private static int lastOrder=0;
        private static int lastOrderItem=0;
        public static int LastOrder { get=>return lastOrder++;}
        public static int LastOrderItem { get=>lastOrderItem++;}
    }
}