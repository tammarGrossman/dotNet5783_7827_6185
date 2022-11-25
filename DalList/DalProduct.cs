using DalApi;
using DO;
using System.Security.Principal;

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
        //if (DataSource.Config.ProductIndex < DataSource.Products.Length)
        //{
        foreach (Product item in DataSource.Products)
          {
                if (item.ID == p.ID)
                    throw new Exception("wrong id");
           }
            //check if there is place
            DataSource.Products.Add(p);
            return p.ID;
        //}
        //else
        // throw new Exception("there is no place");
    }
    /// <summary>
    /// get object
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Product Get(int id)
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
    public void Delete(int id)
    {
        int exist = 0;
        foreach (Product item in DataSource.Products)
        {

               if (item.ID == id)
            { //FIND
                exist = 1;
                //for (; i < DataSource.Config.ProductIndex; i++)
                //{
                //    DataSource.Products[i] = DataSource.Products[i + 1];
                //}
                DataSource.Products.Remove(item);   
                //DataSource.Config.ProductIndex--;
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
public void Update(Product p)
{
    bool exist = false;
        foreach (Product item in DataSource.Products)
        {
            if (item.ID == p.ID)//FIND
            {
                exist = true;
                DataSource.Products.Remove(item);
               DataSource.Products.Add(p);
            }
        }
    if (!exist)
        throw new Exception("not exists");
}
/// <summary>
/// get all objects
/// </summary>
/// <returns></returns>
public Product[] GetAll()
    {
        Product[] newProducts = new Product[DataSource.Products.Count()];
        Product p = new Product();
        foreach (Product item in DataSource.Products)
        {

               p.ID = item.ID;
               p.Name = item.Name;
               p.Category_ = item.Category_;
               p.Price = item.Price;
               p.InStock = item.InStock;
               newProducts[i] = p;
        }
        if(DataSource.Products.Count()==0)
            Console.WriteLine("there is no products");
        return newProducts;
    }
}