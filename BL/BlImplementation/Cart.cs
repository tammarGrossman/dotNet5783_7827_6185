
using BlApi;
using static BO.Exceptions;
namespace BlImplementation;
internal class Cart : ICart
{
    DalApi.IDal dal = new Dal.Dallist();
    public void Add(BO.Cart c, int id)
    {
        if (BO.Validation.NameAdress(c.CustomerName) && BO.Validation.NameAdress(c.CustomerAdress) && BO.Validation.Email(c.CustomerEmail) && BO.Validation.Price(c.TotalPrice))
        {
            try
            {
                double totalPrice = 0;
                bool exist = false, pExist = false;
                BO.OrderItem BOorderItem = new BO.OrderItem();
                DO.Product doProduct = dal.Product.Get(id);
                foreach (var item in c.Items)
                {

                    if (doProduct.ID == item.ProductID && doProduct.InStock > 0)
                    {
                        exist = true;
                        /*down*/
                    }
                }
                if (!exist)
                {
                    IEnumerable<DO.Product> products = dal.Product.GetAll();
                    foreach (var product in products)
                    {
                        if (product.ID == id && product.InStock > 0)
                        {
                            pExist = true;
                            BOorderItem.ID = product.ID;//check what is the id
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
    public BO.Cart Update(BO.Cart c, int id, int quantity)
    {
        try
        {
            int pInStock;
            BO.OrderItem BOorderItem = new BO.OrderItem();
            double totalPrice = 0;
            bool exist = false;
            int newQuantity = 0;
            foreach (var item in c.Items)
            {
                if (item.ID == id)
                {
                    pInStock = dal.Product.Get(item.ProductID).InStock;
                    exist = true;
                    newQuantity = item.Amount + quantity;
                    if (newQuantity == 0)
                        c.Items.Remove(item);
                    else if (newQuantity < 0)
                        throw new NotLegal("your cart does not contain this amount of this item");
                    else if (newQuantity > pInStock)
                        throw new NotLegal("there is not enough amount of this item in the store");
                    else
                    {
                        BOorderItem.ID = item.ID;
                        BOorderItem.Name = item.Name;
                        BOorderItem.ProductID = item.ProductID;
                        BOorderItem.Price = item.Price;
                        BOorderItem.Amount = newQuantity;
                        BOorderItem.TotalPrice = item.TotalPrice + item.Price;
                        c.Items.Remove(item);
                        c.Items.Add(BOorderItem);

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
    }
    public void OrderConfirmation(BO.Cart c)
    {
        int newOrderID;
        DO.OrderItem newOrderItem = new DO.OrderItem();
        DO.Order newOrder = new DO.Order();
        newOrder.OrderDate = DateTime.Now;
        newOrder.CustomerName = c.CustomerName;
        newOrder.CustomerAdress = c.CustomerAdress;
        newOrder.CustomerEmail = c.CustomerEmail;
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
                dal.OrderItem.Add(newOrderItem);
            }
            catch (Exception ex)
            {
                throw new Duplication(ex.Message);
            }
        }
    }
}




//BOorderItem.ID = item.ID;
//BOorderItem.Name = item.Name;
//BOorderItem.ProductID = item.ProductID;
//BOorderItem.Price = item.Price;
//BOorderItem.Amount = item.Amount + 1;
//BOorderItem.TotalPrice = item.TotalPrice + item.Price;
// Cart newC=  Update(c, id, item.Amount + 1);
//else
//  throw new NotFound("the product does not exist in the stock");
