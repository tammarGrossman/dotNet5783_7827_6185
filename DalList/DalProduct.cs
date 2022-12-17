using DalApi;
using DO;
namespace Dal;
internal class DalProduct :IProduct
{
    /// <summary>
    /// add object
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public int Add(Product p)
    {
        foreach (Product? item in DataSource.Products)
          {
                if (item?.ID == p.ID)
                    throw new Duplication(p.ID,"product");
        }
        DataSource.Products.Add(p);
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
        foreach (Product? item in DataSource.Products)
        {
            if (item?.ID == id)//FIND
                return item ?? throw new NotExist(id, "product");
        }
        throw new NotExist(id, "product");
    }
    /// <summary>
    /// delete object
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public void Delete(int id)
    {
        int exist = 0;
        Product product = new Product();
        foreach (Product? item in DataSource.Products)
        {
            if (item?.ID == id)
            { //FIND
                exist = 1;
                product.Name = item?.Name;
                product.ID = (item?.ID) ?? 0;
                product.Price=(item?.Price) ?? 0;
                product.InStock = (item?.InStock) ?? 0;
                product.Category_ = item?.Category_;
            }
        }
        if(exist==0)
           throw new NotExist(id, "product");
        else
            DataSource.Products.Remove(product);

    }
    /// <summary>
    /// update object
    /// </summary>
    /// <param name="p"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Product p)
    {
        Product product = new Product();
        bool exist = false;
        foreach (Product? item in DataSource.Products)
        {
            if (item?.ID == p.ID && !exist)//FIND
            {
                exist = true;
                product.Name = item?.Name;
                product.ID = (item?.ID) ?? 0;
                product.Price = (item?.Price) ?? 0;
                product.InStock = (item?.InStock) ?? 0;
                product.Category_ = item?.Category_;
            }
        }
        if (!exist)
            throw new NotExist(p.ID, "product");
        else
        {
            DataSource.Products.Remove(product);
            DataSource.Products.Add(p);
        }
    }
    /// <summary>
    /// get all objects
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Product?> GetAll(Func<Product?,bool>? Condition=null)
    {
        List<Product?> newProducts = new List<Product?>();
        Product p = new Product();
        foreach (Product? item in DataSource.Products)
        {
            if (Condition == null||(Condition!=null && Condition(item)))
            {
                p.ID = (item?.ID)??0;
                p.Name = item?.Name;
                p.Category_ = item?.Category_;
                p.Price = (item?.Price) ?? 0;
                p.InStock = (item?.InStock)??0;
                newProducts.Add(p);
            }
        }
        if(DataSource.Products.Count()==0)
            Console.WriteLine("there is no products");
        return newProducts;
    }
    public Product GetByCon(Func<Product?, bool>? Condition = null)
     {
        
        foreach (Product? item in DataSource.Products)
        {
            if (Condition(item))
                return item ?? throw new NotExist((item?.ID)??0,"product");
        }
        throw new NotExist(0,"product");

    }
}