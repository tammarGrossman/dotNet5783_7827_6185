
using BlApi;
using BO;
using Dal;
using static BO.Exceptions;

namespace BlImplementation;

internal class Product:IProduct
{
     DalApi.IDal dal = new Dallist();  
    public IEnumerable<BO.ProductForList> GetAll()
    {
        List<BO.ProductForList> products = new List<BO.ProductForList>();
        foreach (var item in dal.Product.GetAll())
        {
            BO.ProductForList product = new BO.ProductForList();
            product.Name = item.Name;
            product.Price = item.Price;
            product.Category_ = (Category)item.Category_;  
            product.ID = item.ID;
            products.Add(product);
        }
        return products;
    }
    public BO.Product Get(int id)
    {
       try 
        { 
        BO.Product BlProduct = new BO.Product();  
        DO.Product DOProduct= dal.Product.Get(id);
        BlProduct.Name = DOProduct.Name;
        BlProduct.ID= DOProduct.ID;
        BlProduct.Price = DOProduct.Price;
        BlProduct.Category_ = (Category)DOProduct.Category_;
        BlProduct.InStock= DOProduct.InStock;
        return BlProduct;
       }
        catch (Exception ex)
        {
            throw new NotExist(ex.Message);
}
    }
    public ProductItem Get(int id,BO.Cart c)
    {
        try { 
        if (id > 0)
        {
            DO.Product DalProduct = dal.Product.Get(id);
            BO.ProductItem BlProductItem = new BO.ProductItem();
            BlProductItem.ID = DalProduct.ID;
            BlProductItem.Name= DalProduct.Name;
            BlProductItem.Price = DalProduct.Price;
            BlProductItem.Category_ = (Category)DalProduct.Category_;
            BlProductItem.InStock = DalProduct.InStock;
            foreach (var item in c.Items)
            {
                if(item.ID==id)
                    BlProductItem.Amount =item.Amount;
            }
            return BlProductItem;
        }
        else
            throw new NotLegal("this is not legal id of product");
        }
        catch (Exception ex)
        {
            throw new NotExist(ex.Message);
        }
    }
    public int Add(BO.Product p)
    {
        DO.Product doProduct = new DO.Product();
        
        if (p.ID > 0 && p.Name != "" && p.Price > 0 && p.InStock > 0)
        {
            doProduct.ID = p.ID;
            doProduct.Name = p.Name;
            doProduct.Price = p.Price;
            doProduct.Category_ = (DO.Category)p.Category_;
            doProduct.InStock = p.InStock;
            return dal.Product.Add(doProduct);
            //try
            //{
            //    Console.WriteLine("doproduct{0}",doProduct);
            //    return dal.Product.Add(doProduct);
            //}
            //catch(Exception ex)
            //{
            //    throw new Duplication(ex.Message);
            //}
        }
        else
            throw new NotLegal("this is not legal id and name of product");
    }
    public void Delete(int id)
    {
        try
        {
            bool exist = false;
            IEnumerable<DO.Order> orders = dal.Order.GetAll();
            List<DO.OrderItem> orderItems;
            foreach (DO.Order order in orders)
            {
                foreach (DO.OrderItem orderItem in dal.OrderItem.GetProductsInOrder(order.ID))
                {
                    if (orderItem.ProductID == id)
                    {
                        exist = true;
                        dal.Product.Delete(id);
                    }
                }
            }
            if (!exist)
                throw new NotExist("there is no such a product");
        }
        catch (Exception ex)
        {
            throw new NotExist(ex.Message);
        }
    }
    public void Update(BO.Product p)
    {
        try {
        DO.Product doProduct = new DO.Product();
        if (p.ID > 0 && p.Name != "" && p.Price > 0 && p.InStock > 0)
        {
            doProduct.ID = p.ID;
            doProduct.Name = p.Name;
            doProduct.Price = p.Price;
            doProduct.Category_ = (DO.Category)p.Category_;
            doProduct.InStock = p.InStock;
            dal.Product.Update(doProduct);
        }
        else
            throw new NotLegal("this is not legal details of id and name of product");
        }
        catch (Exception ex)
        {
            throw new NotExist(ex.Message);
        }
    }
}
