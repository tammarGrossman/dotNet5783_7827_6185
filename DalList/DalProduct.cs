using DalApi;
using DO;
namespace Dal;
internal class DalProduct : IProduct
{
    /// <summary>
    /// add object
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public int Add(Product p)
    {
        if (productExist(p.ID))
            throw new Duplication(p.ID, "product");

        DataSource.products.Add(p);
        return p.ID;
    }

    /// <summary>
    /// get object
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Product Get(int id)
    {

        if (productExist(id))//found
            return DataSource.products.Find(product => product?.ID == id) ?? throw new NotExist(id, "product");
        throw new NotExist(id, "product");
    }

    /// <summary>
    /// function that check if the order item exist 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private bool productExist(int id)
    {
        return DataSource.products.Any(product => product?.ID == id);
    }

    /// <summary>
    /// delete object
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public void Delete(int id)
    {
        int count = DataSource.products.RemoveAll(product => product?.ID == id);

        if (count == 0)
            throw new NotExist(id, "product");
    }

    /// <summary>
    /// update object
    /// </summary>
    /// <param name="p"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Product p)
    {
        int count = DataSource.products.RemoveAll(product => product?.ID == p.ID);
        if (count == 0)
            throw new NotExist(p.ID, "product");

        DataSource.products.Add(p);

    }

    /// <summary>
    /// get all objects
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? Condition = null)
    {
        if (Condition != null)
            return from product in DataSource.products
                   where Condition(product)
                   select product;

        return from product in DataSource.products
               select product; ;
    }
    public Product GetByCon(Func<Product?, bool>? Condition )
    {
        return DataSource.products.Find(x => Condition!(x)) ??
        throw  new NotExist(0, "product"); 

    }
}