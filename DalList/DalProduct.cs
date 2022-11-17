using DO;
namespace Dal;
public class DalProduct
{
    public int AddProduct(Product p)
    {
        if (DataSource.Config.OrderItemIndex < DataSource.Products.Length)
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
    public Product[] GetProduct(int id)
    {
        foreach (Product item in DataSource.Products)
        {
            if (item.ID == id)//FIND
                return item;
        }
        throw new Exception("not exists");
    }
    public Product DeleteProduct(int id)
    {
        for (int i = 0; i < DataSource.Config.ProductIndex; i++)
            if (DataSource.Products[i].ID == id)
            { //FIND
                for (; i < DataSource.Config.ProductIndex; i++)
                {
                    DataSource.Products[i] = DataSource.Products[i + 1];
                }
                DataSource.Config.ProductIndex--;
            }
        throw new Exception("not exists");
    }
}
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
public Product[] GetProducts()
    {
        Product[] newProducts = new Product[DataSource.Config.ProductIndex];
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