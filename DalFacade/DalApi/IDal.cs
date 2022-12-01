
namespace DalApi
{
    public interface IDal
    {
        /// <summary>
        /// this is the order interface
        /// </summary>
        public IOrder Order { get;}

        /// <summary>
        /// this is the product interface
        /// </summary>
        public IProduct Product { get;}

        /// <summary>
        /// this is the order item interface
        /// </summary>
        public IOrderItem OrderItem { get;}
    }
}
