using DalApi;
using System.Diagnostics;

namespace DalXml
{
    sealed internal class DalXml : IDal
    {
        public static IDal Instance { get; } = new DalXml();
        private DalXml() { }

        public IProduct Product { get; } = new Dal.Product();
        public IOrder Order { get; } = new Dal.Order();
        public IOrderItem OrderItem { get; } = new Dal.OrderItem();

    }
}