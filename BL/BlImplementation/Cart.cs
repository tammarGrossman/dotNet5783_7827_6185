
using BlApi;
using static BO.Exceptions;
namespace BlImplementation;
internal class Cart : ICart
{
    static int idOrderItem = 0;
    DalApi.IDal dal = new Dal.Dallist();
    /// <summary>
    /// a function to add product to cart
    /// </summary>
    /// <param name="c"></param>
    /// <param name="id"></param>
    /// <exception cref="NotFound"></exception>
    /// <exception cref="NotExist"></exception>
    /// <exception cref="NotLegal"></exception>
    public void Add(BO.Cart c, int id)
    {
        if (BO.Validation.NameAdress(c.CustomerName) && BO.Validation.NameAdress(c.CustomerAdress) && BO.Validation.Email(c.CustomerEmail) && BO.Validation.Price(c.TotalPrice) && BO.Validation.ID(id))
        {
            try
            {
                double totalPrice = 0;
                bool exist = false, pExist = false;
                BO.OrderItem BOorderItem = new BO.OrderItem();
                DO.Product doProduct = dal.Product.Get(id);
                foreach (var item in c.Items)
                {
                    if (doProduct.ID == item.ProductID && BO.Validation.InStock(doProduct.InStock))
                    {
                        exist = true;
                    }
                }
                if (!exist)
                {
                    IEnumerable<DO.Product?> products = dal.Product.GetAll();
                    foreach (var product in products)
                    {
                        if (product.ID == id && product.InStock > 0)
                        {
                            pExist = true;
                            BOorderItem.ID = idOrderItem++;//check what is the id
                            BOorderItem.ProductID = product.ID;
                            BOorderItem.Name = product.Name;
                            BOorderItem.Price = product.Price;
                            BOorderItem.TotalPrice = BOorderItem.Price;
                            BOorderItem.Amount = 1;
                        }
                    }
                    if (!pExist)
                        throw new NotFound("the product does not exist");
                }
                c.Items.Add(BOorderItem);
                foreach (BO.OrderItem item in c.Items)
                {
                    totalPrice += item.TotalPrice;
                }
                c.TotalPrice = totalPrice;
            }
            catch (Exception ex)
            {
                throw new NotExist(ex.Message);
            }
        }
        else
            throw new NotLegal("one or more of the details of the cart is not legal");
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
        if (BO.Validation.ID(id) && BO.Validation.NameAdress(c.CustomerName) && BO.Validation.NameAdress(c.CustomerAdress) && BO.Validation.Email(c.CustomerEmail))
            try
            {
                int pInStock = 0;
                double totalPrice = 0;
                bool exist = false;
                int newQuantity = 0;
                foreach (var item in c.Items)
                {
                    if (item.ProductID == id)
                    {
                        pInStock = dal.Product.Get(item.ProductID).InStock;
                        exist = true;
                        newQuantity = item.Amount + quantity;
                        if (newQuantity < 0)
                            throw new NotLegal("your cart does not contain this amount of this item");
                        else if (newQuantity > pInStock)
                            throw new NotLegal("there is not enough amount of this item in the store");
                        else
                        {
                            item.Amount = newQuantity;
                            item.TotalPrice = item.Price * item.Amount;
                        }
                    }
                }
                if (!exist)
                    throw new NotExist("the product item does not exist in the cart");
                foreach (var item in c.Items)
                {
                    totalPrice += item.TotalPrice;
                }
                c.TotalPrice = totalPrice;
                return c;
            }
            catch (Exception ex)
            {
                throw new NotExist(ex.Message);
            }
        else
            throw new NotLegal("this is not legal details");
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
        DO.OrderItem newOrderItem = new DO.OrderItem();
        DO.Order newOrder = new DO.Order();
        DO.Product pro = new DO.Product();
        newOrder.OrderDate = DateTime.Now;
        newOrder.CustomerName = c.CustomerName;
        newOrder.CustomerAdress = c.CustomerAdress;
        newOrder.CustomerEmail = c.CustomerEmail;
        if (BO.Validation.NameAdress(newOrder.CustomerName) && BO.Validation.NameAdress(newOrder.CustomerAdress) && BO.Validation.Email(newOrder.CustomerEmail))
        {
            newOrder.ShipDate = DateTime.MinValue;
            newOrder.DeliveryDate = DateTime.MinValue;
            newOrderID = dal.Order.Add(newOrder);
            foreach (var item in c.Items)
            {
                newOrderItem.OrderID = newOrderID;
                newOrderItem.ProductID = item.ProductID;
                newOrderItem.Price = item.Price;
                newOrderItem.Amount = item.Amount;
                try
                {
                    pro = dal.Product.Get(newOrderItem.ProductID);
                    if (BO.Validation.InStock(pro.InStock) && pro.InStock >= newOrderItem.Amount)
                    {
                        pro.InStock -= newOrderItem.Amount;
                        try
                        {
                            dal.Product.Update(pro);
                        }
                        catch (Exception ex)
                        {
                            throw new NotExist(ex.Message);
                        }
                    }
                    else
                        throw new NotExist("there is no quantity from this product");
                }
                catch (Exception ex)
                {
                    throw new NotExist("there is not such a product");
                }
                try
                {
                    dal.OrderItem.Add(newOrderItem);
                }
                catch (Exception ex)
                {
                    throw new Duplication(ex.Message);
                }
            }
        }
        else
            throw new NotLegal("this is not legal customer details");
    }
}