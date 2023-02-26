using DalApi;
namespace Dal
{
       /// <summary>
      /// singelton dal xml 
      /// </summary>
        sealed internal class DalXml : IDal
        {
            public static IDal Instance { get; } = new DalXml();
            private DalXml() { }

        /// <summary>
        /// acsess to the implementation of the entities
        /// </summary>
        /// 
            public IProduct Product { get; } = new Product();
            public IOrder Order { get; } = new Order();
            public IOrderItem OrderItem { get; } = new OrderItem();
        }
    }
