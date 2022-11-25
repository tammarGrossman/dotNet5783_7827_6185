
using BlApi;
using Dal;
using DalApi;

namespace BlImplementation;

internal class Product:IProduct
{
     DalApi.Idal dal = new Dallist();  
    public IEnumerable<BO.ProductForList> GetAll()
    {
       // IDal idal = new Dallist();//change to singletown
        List<BO.ProductForList> products = new List<BO.ProductForList>();
        foreach (var item in dal.Product.GetAll())
        {
            BO.ProductForList product = new BO.ProductForList();
            product.Name = item.Name;
            product.Price = item.Price;
            //pr.category = item.CategoryP;  
            product.ID = item.ID;
            products.Add(product);
        }
        return products;

    }
    public Product Get(int id)
    {
        BO.Product Blproduct = new BO.Product();  
        DO.Product DalProduct= dal.Get(id);
        Blproduct.Name = DalProduct.Name;
        Blproduct.ID= DalProduct.ID;
        Blproduct.Price = DalProduct.Price;
        Blproduct.Category_ = DalProduct.Category_;
        Blproduct.InStock=DalProduct.InStock;
        return Blproduct;
    }

}
