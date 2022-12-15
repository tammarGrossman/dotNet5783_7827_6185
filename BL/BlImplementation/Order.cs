
using BlApi;
using static BO.Exceptions;
namespace BlImplementation;
internal class Order : IOrder
{
    DalApi.IDal dal = new Dal.Dallist();
    /// <summary>
    ///  a function to get all orders
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.OrderForList?> GetAll()
    {
        int countOrders = 0;
        int amountOfProInOrd = 0;
        double totalPriceInOrd = 0;
        IEnumerable<DO.OrderItem?> proInOr;
        List<BO.OrderForList?> orders = new List<BO.OrderForList?>();
        foreach (DO.Order? item in dal.Order.GetAll())
        {
            BO.OrderForList order = new BO.OrderForList();
            try
            {
                proInOr = dal.OrderItem.GetProductsInOrder((item?.ID) ?? 0);
                countOrders++;
                foreach (DO.OrderItem? item2 in proInOr)
                {
                    amountOfProInOrd += (item2?.Amount) ?? 0;
                    totalPriceInOrd += (item2?.Price) ?? 0 * (item2?.Amount) ?? 0;
                }
            }
            catch (Exception ex) { }
            order.ID = (item?.ID) ?? 0;
            order.CustomerName = item?.CustomerName;
            order.Status = TrackOrder((item?.ID) ?? 0).Status;
            order.TotalPrice = totalPriceInOrd;
            order.AmountOfItems = amountOfProInOrd;
            orders.Add(order);
        }
        if (countOrders == 0)
            throw new NotExist("there is no products in all orders");
        return orders;
    }
    /// <summary>
    /// a function to get order by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotLegal"></exception>
    /// <exception cref="BO.Exceptions.NotExist"></exception>
    public BO.Order Get(int id)
    {
        try
        {
            if (BO.Validation.ID(id))
            {
                double totalPrice = 0;
                BO.Order BlOrder = new BO.Order();
                DO.Order DalOrder = dal.Order.Get(id);
                BO.OrderItem BlorderItem = new BO.OrderItem();
                foreach (var item in dal.OrderItem.GetProductsInOrder(id))
                {
                    BlorderItem.ID = (item?.OrderItemID) ?? 0;
                    BlorderItem.Name = dal.Product.Get((item?.ProductID) ?? 0).Name;
                    BlorderItem.ProductID = (item?.ProductID) ?? 0;
                    BlorderItem.Price = (item?.Price) ?? 0;
                    BlorderItem.Amount = (item?.Amount) ?? 0;
                    BlOrder.Items.Add(BlorderItem);
                    totalPrice += BlorderItem.Price * BlorderItem.Amount;
                }
                BlOrder.ID = DalOrder.ID;
                BlOrder.CustomerName = DalOrder.CustomerName;
                BlOrder.CustomerEmail = DalOrder.CustomerEmail;
                BlOrder.CustomerAdress = DalOrder.CustomerAdress;
                BlOrder.PaymentDate = DalOrder.OrderDate;
                BlOrder.ShipDate = DalOrder.ShipDate;
                BlOrder.DeliveryDate = DalOrder.DeliveryDate;
                BlOrder.TotalPrice = totalPrice;
                BlOrder.Status = TrackOrder(BlOrder.ID).Status;
                return BlOrder;
            }
            else
                throw new NotLegal("this is not a legal details of order");
        }
        catch (Exception ex)
        {
            throw new BO.Exceptions.NotExist(ex.Message);
        }
    }
    /// <summary>
    ///  a function to update sent date of order
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotLegal"></exception>
    /// <exception cref="BO.Exceptions.NotExist"></exception>
    public BO.Order UpdateSend(int id)
    {
        try
        {
            double totalPrice = 0;
            DO.Order dalOrder = dal.Order.Get(id);
            BO.Order blOrder = new BO.Order();
            BO.OrderItem BlorderItem = new BO.OrderItem();
            DO.Order DOorder = new DO.Order();
            if (dalOrder.ShipDate == DateTime.MinValue)
            {
                DOorder.ID = dalOrder.ID;
                DOorder.CustomerName = dalOrder.CustomerName;
                DOorder.CustomerEmail = dalOrder.CustomerEmail;
                DOorder.CustomerAdress = dalOrder.CustomerAdress;
                DOorder.OrderDate = dalOrder.OrderDate;
                DOorder.DeliveryDate = dalOrder.DeliveryDate;
                DOorder.ShipDate = DateTime.Now;
                dal.Order.Update(DOorder);
                foreach (DO.OrderItem item in dal.OrderItem.GetProductsInOrder(id))
                {
                    BlorderItem.ID = item.OrderItemID;
                    BlorderItem.Name = dal.Product.Get(item.ProductID).Name;
                    BlorderItem.ProductID = item.ProductID;
                    BlorderItem.Price = item.Price;
                    BlorderItem.Amount = item.Amount;
                    BlorderItem.TotalPrice = BlorderItem.Price * BlorderItem.Amount;
                    blOrder.Items.Add(BlorderItem);
                    totalPrice += BlorderItem.Price * BlorderItem.Amount;
                }
                blOrder.ID = DOorder.ID;
                blOrder.CustomerName = DOorder.CustomerName;
                blOrder.CustomerAdress = DOorder.CustomerAdress;
                blOrder.CustomerEmail = DOorder.CustomerEmail;
                blOrder.PaymentDate = DOorder.OrderDate;
                blOrder.ShipDate = DOorder.ShipDate;
                blOrder.DeliveryDate = DOorder.DeliveryDate;
                blOrder.TotalPrice = totalPrice;
                blOrder.Status = BO.OrderStatus.sent;
                return blOrder;
            }
            throw new NotLegal("this is not a legal value of order");
        }
        catch (Exception ex)
        {
            throw new BO.Exceptions.NotExist(ex.Message);
        }
    }
    /// <summary>
    /// a function to update delivered date of order
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotLegal"></exception>
    /// <exception cref="NotExist"></exception>
    public BO.Order UpdateSupply(int id)
    {
        try
        {
            double totalPrice = 0;
            DO.Order dalOrder = dal.Order.Get(id);
            BO.Order blOrder = new BO.Order();
            BO.OrderItem BlorderItem = new BO.OrderItem();
            DO.Order DOorder = new DO.Order();
            if (dalOrder.DeliveryDate == DateTime.MinValue)
            {
                DOorder.ID = dalOrder.ID;
                DOorder.CustomerName = dalOrder.CustomerName;
                DOorder.CustomerEmail = dalOrder.CustomerEmail;
                DOorder.CustomerAdress = dalOrder.CustomerAdress;
                DOorder.OrderDate = dalOrder.OrderDate;
                DOorder.ShipDate = dalOrder.ShipDate;
                DOorder.DeliveryDate = DateTime.Now;
                dal.Order.Update(DOorder);
                foreach (var item in dal.OrderItem.GetProductsInOrder(id))
                {
                    BlorderItem.ID = (item?.OrderItemID) ?? 0;
                    BlorderItem.Name = dal.Product.Get((item?.ProductID) ?? 0).Name;
                    BlorderItem.ProductID = (item?.ProductID) ?? 0;
                    BlorderItem.Price = (item?.Price) ?? 0;
                    BlorderItem.Amount = (item?.Amount) ?? 0;
                    BlorderItem.TotalPrice = BlorderItem.Price * BlorderItem.Amount;
                    blOrder.Items.Add(BlorderItem);
                    totalPrice += BlorderItem.Price * BlorderItem.Amount;
                }
                blOrder.ID = DOorder.ID;
                blOrder.CustomerName = DOorder.CustomerName;
                blOrder.CustomerEmail = DOorder.CustomerEmail;
                blOrder.CustomerAdress = DOorder.CustomerAdress;
                blOrder.PaymentDate = DOorder.OrderDate;
                blOrder.ShipDate = DOorder.ShipDate;
                blOrder.DeliveryDate = DOorder.DeliveryDate;
                blOrder.TotalPrice = totalPrice;
                blOrder.Status = BO.OrderStatus.received;
                return blOrder;
            }
            throw new NotLegal("this is not legal details of order");
        }
        catch (Exception ex)
        {
            throw new NotExist(ex.Message);
        }
    }
    /// <summary>
    /// a function to track order
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotExist"></exception>
    public BO.OrderTracking TrackOrder(int id)
    {
        DO.Order order = new DO.Order();
        BO.OrderTracking orderTracking = new BO.OrderTracking();
        
        bool exist = false;
        try
        {
           order = dal.Order.Get(id);
            if (order.ID != 0)
                exist = true;
            orderTracking.ID = (order.ID);
            if (order.DeliveryDate != null)
            {
                orderTracking.Status = BO.OrderStatus.received;
                orderTracking.Tracking.Add(new Tuple<DateTime?, string?>(order.OrderDate, "Order delivered"));
            }
            else if (order.ShipDate > DateTime.MinValue)
            {
                orderTracking.Status = BO.OrderStatus.sent;
                orderTracking.Tracking.Add(new Tuple<DateTime?, string?>(order.ShipDate, "Order Sent"));
            }
            else
            {
                orderTracking.Status = BO.OrderStatus.ordered;
                orderTracking.Tracking.Add(new Tuple<DateTime?,string?>(order.OrderDate, "Order recieved"));
            }
        }
        catch (Exception ex)
        {
            throw new NotExist(ex.Message);
        }      
        if (!exist)
            throw new NotExist("the order does not exist");
        return orderTracking;
    }
}

