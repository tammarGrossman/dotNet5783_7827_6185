﻿
using BlApi;

namespace BlImplementation;

internal class Product : IProduct
{

    DalApi.IDal? dal = DalApi.Factory.Get();
    /// <summary>
    /// a function to get all products
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.ProductForList?> GetAll(Func<BO.ProductForList?, bool>? cond = null)
    {
        IEnumerable<BO.ProductForList> list;
        list = from DO.Product doProduct in dal!.Product.GetAll()
               select new BO.ProductForList()
               {
                   ID = doProduct.ID,
                   Name = doProduct.Name,
                   Price = doProduct.Price,
                   Category_ = (BO.Category?)doProduct.Category_,
               };
        return cond is null ? list : list.Where(cond);
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
            DO.Product dalProduct = dal!.Product.Get(id);
            return new BO.Product()
            {
                Name = dalProduct.Name,
                ID = dalProduct.ID,
                Price = dalProduct.Price,
                Category_ = (BO.Category?)dalProduct.Category_,
                InStock = dalProduct.InStock
            };
        }

        catch (DO.NotExist ex)
        {
            throw new BO.Exceptions.NotExist(ex.Message, ex);
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
                if (BO.Validation.ID(id))
                {
                    DO.Product dalProduct = dal!.Product.Get(id);
                    BO.ProductItem blOrder = new BO.ProductItem()
                    {
                        ID = dalProduct.ID,
                        Name = dalProduct.Name,
                        Price = dalProduct.Price,
                        Category_ = (BO.Category?)dalProduct.Category_,
                        InStock = dalProduct.InStock > 0 ? true : false,
                        Amount = (c.Items.FirstOrDefault(x => x?.ProductID == id) ?? throw new BO.Exceptions.NotExist("there is no such product in the cart")).Amount,
                    };
                    return blOrder;
                }

                else
                    throw new BO.Exceptions.NotLegal("this is not legal id of product");
            }

            catch (DO.NotExist ex)
            {
                throw new BO.Exceptions.NotExist(ex.Message, ex);
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

        if (BO.Validation.ProductID(p.ID) && BO.Validation.NameAdress(p.Name ?? "") && BO.Validation.Price(p.Price) && BO.Validation.InStock(p.InStock))
        {
            doProduct.ID = p.ID;
            doProduct.Name = p.Name;
            doProduct.Price = p.Price;
            doProduct.Category_ = (DO.Category?)p.Category_;
            doProduct.InStock = p.InStock;
            return dal!.Product.Add(doProduct);
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
            IEnumerable<DO.Order?> orders;
            orders = from DO.Order? order in dal!.Order.GetAll()
                     where (dal!.OrderItem.GetProductsInOrder((order?.ID) ?? 0)).All(x => x?.ProductID != id)
                     select order;
            if (orders.Count() == dal!.Order.GetAll().Count())
                dal!.Product.Delete(id);
            else
                throw new BO.Exceptions.NotExist("there is such a product in other orders");
        }
        catch (DO.NotExist ex)
        {
            throw new BO.Exceptions.NotExist(ex.Message, ex);
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
            if (BO.Validation.ProductID(p.ID) && BO.Validation.NameAdress(p.Name ?? "") && BO.Validation.Price(p.Price) && BO.Validation.InStock(p.InStock))
            {
                doProduct.ID = p.ID;
                doProduct.Name = p.Name;
                doProduct.Price = p.Price;
                doProduct.Category_ = (DO.Category?)p.Category_;
                doProduct.InStock = p.InStock;
                dal!.Product.Update(doProduct);
            }

            else
                throw new BO.Exceptions.NotLegal("this is not legal details of id and name of product");
        }

        catch (DO.NotExist ex)
        {
            throw new BO.Exceptions.NotExist(ex.Message, ex);
        }
    }

    /// <summary>
    /// return all product items
    /// </summary>
    /// <param name="cond"></param>
    /// <returns></returns>
    public IEnumerable<BO.ProductItem?> GetAllPI(Func<BO.ProductItem?, bool>? cond = null)
    {

        IEnumerable<BO.ProductItem> items;
        items = from DO.Product item in dal!.Product.GetAll()
                 select new BO.ProductItem()
                 {
                     ID = item.ID,
                     Name = item.Name,
                     Price = item.Price,
                     Category_ = (BO.Category?)item.Category_,
                     InStock = item.InStock > 0 ? true : false,
                     Amount =item.InStock
                 };
        return cond is null ? items : items.Where(cond);
    }

    /// <summary>
    /// the function groups the products by category and choose the 3 first organs
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public IEnumerable<BO.ProductForList> GroupingProductsByCat()
    {
        var ans = (from x in dal!.Product.GetAll()
                   group x by x?.Category_ into g
                   select new { Key = g.Key, Values = g.Take(3) });
        
       return (from x in ans
                   from y in x.Values
                   select new BO.ProductForList()
                   {
                       ID= y?.ID??0, 
                       Name=y?.Name,
                       Price=y?.Price??0,
                       Category_= (BO.Category?)y?.Category_
                   }).ToList();
    }

}