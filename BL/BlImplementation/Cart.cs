
using BlApi;
using DalApi;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace BlImplementation;
internal class Cart : ICart
{
    static int idOrderItem = 0;

    static DalApi.IDal? dal = DalApi.Factory.Get();
    /// <summary>
    /// a function to add product to cart
    /// </summary>
    /// <param name="c"></param>
    /// <param name="id"></param>
    /// <exception cref="NotFound"></exception>
    /// <exception cref="NotExist"></exception>
    /// <exception cref="NotLegal"></exception>
    public BO.Cart Add(BO.Cart c, int id)
    {
        if ( BO.Validation.Price(c.TotalPrice) && BO.Validation.ProductID(id))
        {
            try
            {
                BO.Cart cart = new BO.Cart();
                BO.OrderItem boOrderItem ;
                DO.Product doProduct = dal!.Product.Get(id);
                int index = c.Items.FindIndex(x => x?.ProductID == id);
                if (index == -1)
                {
                    cart.CustomerAdress = c.CustomerAdress;
                    cart.CustomerEmail=c.CustomerEmail;
                    cart.CustomerName=c.CustomerName;
                    cart.Items = new(c.Items);   
                    try
                    {
                        DO.Product product = dal.Product.GetByCon(x => x?.ID == id && x?.InStock > 0);
                        boOrderItem = new BO.OrderItem()
                        {
                            ProductID = product.ID,
                            Name = product.Name,
                            Price = product.Price,
                            TotalPrice = product.Price,
                            Amount = 1
                        };                 

                        cart.Items.Add(boOrderItem);
                    }

                    catch (DO.NotExist ex)
                    {
                        throw new BO.Exceptions.NotExist($"the product id {id} does not exist");
                    }
                    cart.TotalPrice = cart.Items.Sum(x => x?.TotalPrice??0);
                }
                return cart;


            }

            catch (DO.NotExist ex)
            {
                throw new BO.Exceptions.NotExist(ex.Message, ex);
            }

        }

        else
            throw new BO.Exceptions.NotLegal("this is not legal details");
    }

    /// <summary>
    ///  a function to add/reduce amount of product in cart
    /// </summary>
    /// <param name="c"></param>
    /// <param name="id"></param>
    /// <param name="quantity"></param>
    /// <returns></returns>
    /// <exception cref="NotLegal"></exception>
    /// <exception cref="NotExist"></exception>
    public BO.Cart Update(BO.Cart c, int id, int quantity)
    {
        if (BO.Validation.ProductID(id))
            try
            {
                BO.Cart cart = new BO.Cart();
                int pInStock = 0;
                int newQuantity = 0;
                BO.OrderItem orderItem= c.Items.FirstOrDefault(x=>x?.ProductID==id) ?? throw new BO.Exceptions.NotExist("the product item does not exist in the cart");
                pInStock = dal!.Product.Get(orderItem.ProductID).InStock;
                newQuantity = orderItem.Amount + quantity;
                cart.CustomerAdress = c.CustomerAdress;
                cart.CustomerEmail = c.CustomerEmail;
                cart.CustomerName = c.CustomerName;
                cart.Items = new(c.Items);
                if (newQuantity < 0)
                    throw new BO.Exceptions.NotLegal("your cart does not contain this amount of this item");

                if (newQuantity == 0)
                    cart.Items.Remove(cart.Items.FirstOrDefault(x => x?.ProductID == id));

                else if (newQuantity > pInStock)
                    throw new BO.Exceptions.NotLegal("there is not enough amount of this item in the store");

                else
                {
                    orderItem.Amount = newQuantity;
                    orderItem.TotalPrice = orderItem.Price * orderItem.Amount;                   
                }
                cart.TotalPrice = cart.Items.Sum(x => x?.TotalPrice??0);
                return cart;
            }

            catch (DO.NotExist ex)
            {
                throw new BO.Exceptions.NotExist(ex.Message,ex);
            }

        else
            throw new BO.Exceptions.NotLegal("this is not legal details");
    }

    private bool UpdateProd(DO.Product p)
    {
        try
        {
            dal!.Product.Update(p);
            return true;
        }
        catch (DO.NotExist ex)
        {
            throw new BO.Exceptions.NotExist(ex.Message, ex);
        }
    }
    /// <summary>
    ///  a function to submit cart to order
    /// </summary>
    /// <param name="c"></param>
    /// <exception cref="NotExist"></exception>
    /// <exception cref="Duplication"></exception>
    /// <exception cref="NotLegal"></exception>
    public void OrderConfirmation(BO.Cart c)
    {
        int newOrderID;
        DO.Order newOrder = new DO.Order();
        newOrder.OrderDate = DateTime.Now;
        newOrder.ShipDate = null;
        newOrder.DeliveryDate = null;
        newOrder.CustomerName = c.CustomerName;
        newOrder.CustomerAdress = c.CustomerAdress;
        newOrder.CustomerEmail = c.CustomerEmail;

        if (BO.Validation.NameAdress(newOrder.CustomerName ?? "") && BO.Validation.NameAdress(newOrder.CustomerAdress ?? "") && BO.Validation.Email(newOrder.CustomerEmail ?? ""))
        {
            try
            {
                newOrderID = dal!.Order.Add(newOrder);
            
                c.Items.FindAll(x => dal.Product.Get(x?.ProductID??0).InStock - x?.Amount > 0 ? true : throw new BO.Exceptions.NotExist("there is no quantity from this product"));
                var res = from item in c.Items
                          let prod = dal.Product.Get(item.ProductID)

                          let newProd = new DO.Product() 
                          {
                              ID = prod.ID,
                              Category_ = prod.Category_,
                              Name = prod.Name,
                              InStock = prod.InStock - item.Amount 
                          }
                          let temp = UpdateProd(newProd)
                          select new DO.OrderItem()
                          {
                              OrderID = newOrderID,
                              Amount = item.Amount,
                              ProductID = item.ProductID,
                              Price = item.Price,
                          };
                res.All(x => dal.OrderItem.Add(x) > 0 ? true : throw new BO.Exceptions.Duplication("the order item is already exist"));
            }
            catch (DO.NotExist ex)
            {
                throw new BO.Exceptions.NotExist(ex.Message, ex);
            }

            catch (DO.Duplication ex)
            {
                throw new BO.Exceptions.Duplication(ex.Message, ex);
            }

        }
        else
            throw new BO.Exceptions.NotLegal("this is not legal customer details");
    }
}