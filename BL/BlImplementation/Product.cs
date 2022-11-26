
using BlApi;
using Dal;
using DalApi;
using static BO.Exceptions;

namespace BlImplementation;

internal class Product:IProduct
{
     DalApi.Idal dal = new Dallist();  
    public IEnumerable<BO.ProductForList> GetAll()
    {
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
        BO.Product BlProduct = new BO.Product();  
        DO.Product DalProduct= dal.Product.Get(id);
        BlProduct.Name = DalProduct.Name;
        BlProduct.ID= DalProduct.ID;
        BlProduct.Price = DalProduct.Price;
        BlProduct.Category_ = DalProduct.Category_;
        BlProduct.InStock=DalProduct.InStock;
        return BlProduct;
    }
    public Product Get(int id, Cart c)
    {
        if (id > 0)
        {
            DO.Product DalProduct = dal.Product.Get(id);
            BO.ProductItem BlProductItem = new BO.ProductItem();
            BlProductItem.ID = DalProduct.ID;
            BlProductItem.Name= DalProduct.Name;
            BlProductItem.Price = DalProduct.Name;
            BlProductItem.Category_ = DalProduct.Category_;
            BlProductItem.InStock = DalProduct.InStock;
            foreach (var item in c.Items)
            {
                if(item.ID==id)
                    BlProductItem.Amount =item.Amount;
            }
            return BlProductItem;
        }
        else
            throw new NotLegal();
    }
    public void Add(Product p)
    {
        DO.Product doProduct = new DO.Product();
        if (p.ID > 0 && p.Name != "" && p.Price > 0 && p.InStock > 0)
        {
            doProduct.ID = p.ID;
            doProduct.Name = p.Name;
            doProduct.Price = p.Price;
            doProduct.Category_ = p.Category_;
            doProduct.InStock = p.InStock;
            dal.Product.Add(doProduct); 
        }
        else
            throw new NotLegal();
    }
    public void Delete(int id)
    {
        bool exist = false;
        List<DO.Order> orders=dal.Order.GetAll();
        List<DO.OrderItem> orderItems;
        foreach (DO.Order order in orders)
        {
            foreach (OrderItem orderItem in dal.OrderItem.GetProductsInOrder(order.ID)
            {
                if(orderItem.ProductID==id)
                {
                    exist = true;
                }
            }
        }
        if(!exist)
            dal.Product.Delete(id);
        else
           throw new Exception();//
    }
    public void Update(Product p)
    {
        DO.Product doProduct = new DO.Product();
        if (p.ID > 0 && p.Name != "" && p.Price > 0 && p.InStock > 0)
        {
            doProduct.ID = p.ID;
            doProduct.Name = p.Name;
            doProduct.Price = p.Price;
            doProduct.Category_ = p.Category_;
            doProduct.InStock = p.InStock;
            dal.Product.Update(doProduct);
        }
        else
            throw new NotLegal();
    }
}
