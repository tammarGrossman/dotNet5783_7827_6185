using DO;
namespace Dal;
public class DalProduct
{
    /// <summary>
    /// add object
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public int AddProduct(Product p)
    {
        if (DataSource.Config.ProductIndex < DataSource.Products.Length)
        {
            for (int i = 0; i < DataSource.Products.Length; i++)
            {
                if (DataSource.Products[i].ID == p.ID)
                    throw new Exception("wrong id");
            }
            //check if there is place
            DataSource.Products[DataSource.Config.ProductIndex++] = p;
            return p.ID;
        }
        else
            throw new Exception("there is no place");
    }
    /// <summary>
    /// get object
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Product GetProduct(int id)
    {
        foreach (Product item in DataSource.Products)
        {
            if (item.ID == id)//FIND
                return item;
        }
        throw new Exception("not exists");
    }
    /// <summary>
    /// delete object
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public void DeleteProduct(int id)
    {
        int exist = 0;
        for (int i = 0; i < DataSource.Config.ProductIndex; i++)
        {
            if (DataSource.Products[i].ID == id)
            { //FIND
                exist = 1;
                for (; i < DataSource.Config.ProductIndex; i++)
                {
                    DataSource.Products[i] = DataSource.Products[i + 1];
                }
                DataSource.Config.ProductIndex--;
            }
        }
        if(exist==0)
           throw new Exception("not exists");
    }

/// <summary>
/// update object
/// </summary>
/// <param name="p"></param>
/// <exception cref="Exception"></exception>
public void UpdateProduct(Product p)
{
    bool exist = false;
    for (int i = 0; i < DataSource.Config.ProductIndex; i++)
        if (DataSource.Products[i].ID == p.ID)//FIND
        {
            exist = true;
            DataSource.Products[i] = p;
        }
    if (!exist)
        throw new Exception("not exists");
}
/// <summary>
/// get all objects
/// </summary>
/// <returns></returns>
public Product[] GetProducts()
    {
        Product[] newProducts = new Product[DataSource.Config.ProductIndex];
        Product p = new Product();
        for (int i=0; i<DataSource.Config.ProductIndex;i++)
        {
               p.ID = DataSource.Products[i].ID;
               p.Name = DataSource.Products[i].Name;
               p.Category_ = DataSource.Products[i].Category_;
               p.Price =  DataSource.Products[i].Price;
               p.InStock =  DataSource.Products[i].InStock;
              newProducts[i] = p;
        }
        if(DataSource.Config.ProductIndex==0)
            Console.WriteLine("there is no products");
        return newProducts;
    }
}