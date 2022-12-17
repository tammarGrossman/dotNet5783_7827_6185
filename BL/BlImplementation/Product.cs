
using BlApi;

using static BO.Exceptions;
namespace BlImplementation;

internal class Product : IProduct
{

    DalApi.IDal? dal = DalApi.Factory.Get();
    /// <summary>
    /// a function to get all products
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.ProductForList?> GetAll(Func<BO.ProductForList?, bool> cond = null)
    {
        List<BO.ProductForList?> products = new List<BO.ProductForList?>();
        foreach (DO.Product? item in dal.Product.GetAll())
        {
            {
                BO.ProductForList product = new BO.ProductForList();
                product.Name = item?.Name;
                product.Price = (item?.Price) ?? 0;
                product.Category_ = (BO.Category)item?.Category_;
                product.ID = (item?.ID) ?? 0;
                if((cond != null&& cond(product))||cond==null)
                  products.Add(product);
            }
        }
        return products;
    }
    /// <summary>
    /// a function to get product
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotExist"></exception>
    public BO.Product Get(int id)
    {
        try
        {
            BO.Product BlProduct = new BO.Product();
            DO.Product DOProduct = dal.Product.Get(id);
            BlProduct.Name = DOProduct.Name;
            BlProduct.ID = DOProduct.ID;
            BlProduct.Price = DOProduct.Price;
            BlProduct.Category_ = (BO.Category)DOProduct.Category_;
            BlProduct.InStock = DOProduct.InStock;
            return BlProduct;
        }
        catch (DO.NotExist ex)
        {
            throw new BO.Exceptions.NotExist(ex.Message,ex);
        }
    }
    /// <summary>
    /// a function to get a product in cart
    /// </summary>
    /// <param name="id"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    /// <exception cref="NotLegal"></exception>
    /// <exception cref="NotExist"></exception>
    public BO.ProductItem Get(int id, BO.Cart c)
    {
        if (c != null)
        {
            try
            {
                if (BO.Validation.ID( id ))
                {
                    DO.Product DalProduct = dal.Product.Get(id);
                    BO.ProductItem BlProductItem = new BO.ProductItem();
                    BlProductItem.ID = DalProduct.ID;
                    BlProductItem.Name = DalProduct.Name;
                    BlProductItem.Price = DalProduct.Price;
                    BlProductItem.Category_ = (BO.Category)DalProduct.Category_;
                    BlProductItem.InStock = DalProduct.InStock;
                    foreach (var item in c.Items)
                    {
                        if (item.ProductID == id)
                            BlProductItem.Amount = item.Amount;
                    }
                    return BlProductItem;
                }
                else
                    throw new NotLegal("this is not legal id of product");
            }
            catch (DO.NotExist ex)
            {
                throw new BO.Exceptions.NotExist(ex.Message,ex);
            }
        }
        else
            throw new BO.Exceptions.NotExist("there is no product in cart");
    }
    /// <summary>
    /// a function to add a product
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    /// <exception cref="NotLegal"></exception>
    public int Add(BO.Product p)
    {
        DO.Product doProduct = new DO.Product();

        if (BO.Validation.ID(p.ID) && BO.Validation.NameAdress(p.Name) && BO.Validation.Price(p.Price) && BO.Validation.InStock(p.InStock))
        {
            doProduct.ID = p.ID;
            doProduct.Name = p.Name;
            doProduct.Price = p.Price;
            doProduct.Category_ = (DO.Category)p.Category_;
            doProduct.InStock = p.InStock;
            return dal.Product.Add(doProduct);
        }
        else
            throw new BO.Exceptions.NotLegal("there is one or more not legal details of product");
    }
    /// <summary>
    /// a function to delete a product
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="NotExist"></exception>
    public void Delete(int id)
    {
        try
        {
            bool existInOrder = false;
            IEnumerable<DO.Order?> orders = dal?.Order.GetAll();
            foreach (DO.Order? order in orders)
            {
                foreach (DO.OrderItem? orderItem in dal?.OrderItem.GetProductsInOrder((order?.ID)??0))
                {
                    if (orderItem?.ProductID == id)
                    {
                        existInOrder = true;
                    }
                }
            }
            if (!existInOrder)
                dal?.Product.Delete(id);
            else
                throw new BO.Exceptions.NotExist("there is such a product in other orders");
        }
        catch (DO.NotExist ex)
        {
            throw new BO.Exceptions.NotExist(ex.Message,ex);
        }
    }
    /// <summary>
    /// a function to update a product
    /// </summary>
    /// <param name="p"></param>
    /// <exception cref="NotLegal"></exception>
    /// <exception cref="NotExist"></exception>
    public void Update(BO.Product p)
    {
        try
        {
            DO.Product doProduct = new DO.Product();
            if (BO.Validation.ID(p.ID)  && BO.Validation.NameAdress(p.Name) && BO.Validation.Price(p.Price) && BO.Validation.InStock(p.InStock))
            {
                doProduct.ID = p.ID;
                doProduct.Name = p.Name;
                doProduct.Price = p.Price;
                doProduct.Category_ = (DO.Category)p.Category_;
                doProduct.InStock = p.InStock;
                dal?.Product.Update(doProduct);
            }
            else
                throw new BO.Exceptions.NotLegal("this is not legal details of id and name of product");
        }
        catch (DO.NotExist ex)
        {
            throw new BO.Exceptions.NotExist(ex.Message,ex);
        }
    }
}