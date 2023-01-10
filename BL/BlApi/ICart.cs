
using BO;

namespace BlApi;

public interface ICart
{
    /// <summary>
    /// add a product to the cart
    /// </summary>
    /// <param name="c"></param>
    /// <param name="id"></param>
    public Cart Add(Cart c, int id);
    /// <summary>
    /// update quantity of product in the cart
    /// </summary>
    /// <param name="c"></param>
    /// <param name="id"></param>
    /// <param name="quantity"></param>
    /// <returns></returns>
    public Cart Update(Cart c, int id, int quantity);
    /// <summary>
    /// confirmation the order
    /// </summary>
    /// <param name="c"></param>
    public void OrderConfirmation(Cart c);
}
