using BO;
namespace BlApi;

public interface IProduct
{
    /// <summary>
    /// get all products
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.ProductForList?> GetAll(Func<BO.ProductForList?,bool> cond=null);
    /// <summary>
    /// add a product
    /// </summary>
    /// <param name="p"></param>
    public int Add(Product p);
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
    /// <summary>
    /// get list of productitem for cart
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    public IEnumerable<ProductItem?> GetAllPI(Func<BO.ProductItem?, bool>? cond = null);

    /// <summary>
    /// grouping products and choose the 3 firsts organs
    /// </summary>
    /// <returns></returns>
    IEnumerable<ProductForList> GroupingProductsByCat();

}
