using DO;
namespace DalApi
{
    public interface IOrderItem:ICrud<OrderItem>
    {
        /// <summary>
        /// get order items by product id and order id
        /// </summary>
        /// <param name="pId"></param>
        /// <param name="oId"></param>
        /// <returns></returns>
        public OrderItem GetOrderItemByIDS(int pId, int oId);
        /// <summary>
        /// get products in order
        /// </summary>
        /// <param name="oID"></param>
        /// <param name="Condition"></param>
        /// <returns></returns>
        public IEnumerable<OrderItem?> GetProductsInOrder(int oID, Func<OrderItem?, bool>? Condition = null);

    }
}
