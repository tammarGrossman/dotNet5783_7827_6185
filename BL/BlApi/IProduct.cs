using BO;
namespace BlApi;

public interface IProduct
{
    /// <summary>
    /// get all products
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.ProductForList> GetAll();
    /// <summary>
    /// add a product
    /// </summary>
    /// <param name="p"></param>
    public void Add(Product p);
    /// <summary>
    /// get a product
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Product Get(int id);
    /// <summary>
    /// get a product from the cart
    /// </summary>
    /// <param name="id"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    public ProductItem Get(int id, Cart c);
    /// <summary>
    /// delete a product
    /// </summary>
    /// <param name="id"></param>
    public void Delete(int id);
    /// <summary>
    /// update a product
    /// </summary>
    /// <param name="p"></param>
    public void Update(Product p);

}
