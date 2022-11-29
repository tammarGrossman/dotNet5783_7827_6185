
using BO;

namespace BlApi;

public interface IOrder
{
    /// <summary>
    /// get all orders
    /// </summary>
    /// <returns></returns>
    public IEnumerable<OrderForList> GetAll();
    /// <summary>
    /// get uniqe order
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Order Get(int id);
    /// <summary>
    /// update send order
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Order UpdateSend(int id);
    /// <summary>
    /// update supply order
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Order UpdateSupply(int id);
    /// <summary>
    /// tracking order
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public OrderTracking TrackOrder(int id);

}
