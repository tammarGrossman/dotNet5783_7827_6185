
namespace Dal;

public class DalProduct
{
    public int AddProduct(Product p)
    {
        if (Config.orderItemIndex < DataSource.Products.length) {
            for (int i = 0; i < DataSource.Products.length; i++)
            {
                if (DataSource.Products[i].ID == p.ID)
                    throw Exception("wrong id")
            }
            //check if there is place
            DataSource.Products[DataSource.Config.ProductIndex++] = p;
            return p.ID;
        }
        else
            throw  new Exception("there is no place")
    }
    public Product GetProduct(int id)
    {
        foreach (Product item in DataSource.products)
        {
            if (item.ID == id)//FIND
                return item;
        }
        throw new Exception("not exists")

    }

    public OrdProducts DeleteProduct(int id)
    {
        foreach (Product item in DataSource.Products)
        {
            if (item.ID == id)//FIND
                item = null;
        }
        throw new Exception("not exists");

    }
    public void UpdateProduct(Product p)
    {
        foreach (Order item in DataSource.Products)
        {
            if (item.ID == p.ID)
            { //FIND
                item = p;
                return;
            }
        }
        throw new Exception("not exists");
    }

    public Product[] GetProducts()
    {
        Product[] newProducts= new Product[DataSource.Config.ProductIndex]
        Product p = new Product();
        int i = 0;
        foreach (Product item in DataSource.Products)
        {
            p.ID = item.ID;
            p.Name = item.Name;
            p.Category = item.Category;
            p.Price = item.Price;
            p.InStock = item.InStock;
            newProducts[i++] = p;
        }
        return newProducts;
    }
}
