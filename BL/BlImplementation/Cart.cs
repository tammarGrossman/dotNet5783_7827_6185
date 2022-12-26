
using BlApi;
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
                        if ((product?.ID) == id && (product?.InStock) > 0)
                        {
                            pExist = true;
                            BOorderItem.ID = idOrderItem++;
                            BOorderItem.ProductID = (product?.ID)??0;
                            BOorderItem.Name = product?.Name;
                            BOorderItem.Price = (product?.Price)??0;
                            BOorderItem.TotalPrice =BOorderItem.Price;
                            BOorderItem.Amount = 1;
                        }
                    }

                    if (!pExist)
                        throw new BO.Exceptions.NotExist($"the product id {id} does not exist");
                }
                c.Items.Add(BOorderItem);
                foreach (BO.OrderItem? item in c.Items)
                {
                    totalPrice += (item?.TotalPrice)??0;
                }
                c.TotalPrice = totalPrice;
            }
            catch (DO.NotExist ex)
            {
                throw new BO.Exceptions.NotExist(ex.Message,ex);
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
                        pInStock = (dal?.Product.Get(item.ProductID).InStock)??0;
                        exist = true;
                        newQuantity = item.Amount + quantity;
                        if (newQuantity < 0)
                            throw new BO.Exceptions.NotLegal("your cart does not contain this amount of this item");
                        else if (newQuantity > pInStock)
                            throw new BO.Exceptions.NotLegal("there is not enough amount of this item in the store");
                        else
                        {
                            item.Amount = newQuantity;
                            item.TotalPrice = item.Price * item.Amount;
                        }
                    }
                }
                if (!exist)
                    throw new BO.Exceptions.NotExist("the product item does not exist in the cart");
                foreach (var item in c.Items)
                {
                    totalPrice += item.TotalPrice;
                }
                c.TotalPrice = totalPrice;
                return c;
            }
            catch (DO.NotExist ex)
            {
                throw new BO.Exceptions.NotExist(ex.Message,ex);
            }
        else
            throw new BO.Exceptions.NotLegal("this is not legal details");
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
        newOrder.ShipDate = null;
        newOrder.DeliveryDate = null;
        newOrder.CustomerName = c.CustomerName;
        newOrder.CustomerAdress = c.CustomerAdress;
        newOrder.CustomerEmail = c.CustomerEmail;
        if (BO.Validation.NameAdress(newOrder.CustomerName) && BO.Validation.NameAdress(newOrder.CustomerAdress) && BO.Validation.Email(newOrder.CustomerEmail))
        {
            newOrder.ShipDate = DateTime.MinValue;
            newOrder.DeliveryDate = DateTime.MinValue;
            newOrderID = (dal?.Order.Add(newOrder))??0;
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
                        catch (DO.NotExist ex)
                        {
                            throw new BO.Exceptions.NotExist(ex.Message,ex);
                        }
                    }
                    else
                        throw new BO.Exceptions.NotExist("there is no quantity from this product");
                }
                catch (DO.NotExist ex)
                {
                    throw new BO.Exceptions.NotExist(ex.Message,ex);
                }
                try
                {
                    dal.OrderItem.Add(newOrderItem);
                }
                catch (DO.Duplication ex)
                {
                    throw new BO.Exceptions.Duplication(ex.Message,ex);
                }
            }
        }
        else
            throw new BO.Exceptions.NotLegal("this is not legal customer details");
    }
}