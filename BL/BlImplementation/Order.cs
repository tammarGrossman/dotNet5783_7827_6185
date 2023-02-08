
using BlApi;

namespace BlImplementation;
internal class Order : IOrder
{
    DalApi.IDal? dal = DalApi.Factory.Get();
    /// <summary>
    ///  a function to get all orders
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.OrderForList?> GetAll()
    {
        IEnumerable<BO.OrderForList?> orders = new List<BO.OrderForList>();
        try
        {
            orders = (from DO.Order? order in dal!.Order.GetAll()
                      select new BO.OrderForList()
                      {
                          ID = order?.ID ?? 0,
                          CustomerName = order?.CustomerName ?? "",
                          Status = OrderStatus(order ?? throw new Exception("")),
                          AmountOfItems = dal.OrderItem.GetProductsInOrder(order?.ID ?? 0).Count(oI => oI?.Amount > 0),
                          TotalPrice = dal.OrderItem.GetProductsInOrder(order?.ID ?? 0).Sum(oI => (oI?.Amount * oI?.Price) ?? 0)
                      });
            return orders;
        }
        catch (DO.NotExist ex)
        {
            throw new BO.Exceptions.NotExist(ex.Message, ex);
        }
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
                DO.Order dalOrder = dal!.Order.Get(id);
                BO.Order blOrder = new BO.Order();
                blOrder.ID = dalOrder.ID;
                blOrder.CustomerName = dalOrder.CustomerName;
                blOrder.CustomerEmail = dalOrder.CustomerEmail;
                blOrder.CustomerAdress = dalOrder.CustomerAdress;
                blOrder.PaymentDate = dalOrder.OrderDate;
                blOrder.ShipDate = dalOrder.ShipDate;
                blOrder.DeliveryDate = dalOrder.DeliveryDate;
                blOrder.Status = OrderStatus(dalOrder);
                IEnumerable<DO.OrderItem?> orderItems = dal!.OrderItem.GetAll(orderItem => orderItem?.OrderID == id);
                blOrder.Items = (from orderItem in orderItems
                                 select new BO.OrderItem
                                 {
                                     ID = orderItem?.OrderID ?? 0,
                                     ProductID = orderItem?.ProductID ?? 0,
                                     Name = dal!.Product.Get(orderItem?.ProductID??0).Name,
                                     Amount = orderItem?.Amount ?? 0,
                                     Price = orderItem?.Price ?? 0,
                                     TotalPrice = orderItem?.Price * orderItem?.Amount ?? 0
                                 }).ToList();
                var items = dal.OrderItem.GetAll(x => x?.OrderID == id);
                blOrder.TotalPrice = items.Sum(x => x?.Price * x?.Amount) ?? 0;
                return blOrder;
            }
            else
                throw new BO.Exceptions.NotLegal("this is not a legal details of order");
        }
        catch (DO.NotExist ex)
        {
            throw new BO.Exceptions.NotExist(ex.Message, ex);
        }
    }

    /// <summary>
    /// the function returns the status of the order
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    private BO.OrderStatus OrderStatus(DO.Order order)
    {
        return order.DeliveryDate != null ? BO.OrderStatus.delivered : order.ShipDate != null ? BO.OrderStatus.shiped : BO.OrderStatus.payment;
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
            DO.Order dalOrder = dal!.Order.Get(id);
            BO.Order blOrder;
            IEnumerable<DO.OrderItem?> orderItems = dal!.OrderItem.GetProductsInOrder(id);
            if (dalOrder.ShipDate == null)
            {
                dalOrder.ShipDate = DateTime.Now;
                dal.Order.Update(dalOrder);

                blOrder = new BO.Order
                {
                    ID = dalOrder.ID,
                    CustomerName = dalOrder.CustomerName,
                    CustomerAdress = dalOrder.CustomerAdress,
                    CustomerEmail = dalOrder.CustomerEmail,
                    PaymentDate = dalOrder.OrderDate,
                    ShipDate = dalOrder.ShipDate,
                    DeliveryDate = dalOrder.DeliveryDate,
                    TotalPrice = 0,
                    Status = BO.OrderStatus.shiped,
                    Items = (from item in orderItems
                             select new BO.OrderItem
                             {
                                 ID = item?.OrderItemID ?? 0,
                                 Name = dal.Product.Get(item?.ProductID ?? 0).Name,
                                 ProductID = item?.ProductID ?? 0,
                                 Price = item?.Price ?? 0,
                                 Amount = item?.Amount ?? 0,
                                 TotalPrice = (item?.Price) ?? 0 * (item?.Amount) ?? 0
                             }).ToList(),

                };
                blOrder.TotalPrice = blOrder.Items.Sum(x => x?.TotalPrice ?? 0);
                return blOrder;

            }
            throw new BO.Exceptions.NotLegal("this is not a legal value of order");
        }

        catch (DO.NotExist ex)
        {
            throw new BO.Exceptions.NotExist(ex.Message, ex);
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
            DO.Order dalOrder = dal!.Order.Get(id);
            BO.Order blOrder;
            IEnumerable<DO.OrderItem?> orderItems = dal!.OrderItem.GetProductsInOrder(id);
            if (dalOrder.ShipDate == null)
                throw new BO.Exceptions.NotLegal("Delivery date can not be updated before shipping date");
            if (dalOrder.DeliveryDate == null)
            {

                dalOrder.DeliveryDate = DateTime.Now;
                dal.Order.Update(dalOrder);

                blOrder = new BO.Order()
                {
                    ID = dalOrder.ID,
                    CustomerName = dalOrder.CustomerName,
                    CustomerEmail = dalOrder.CustomerEmail,
                    CustomerAdress = dalOrder.CustomerAdress,
                    PaymentDate = dalOrder.OrderDate,
                    ShipDate = dalOrder.ShipDate,
                    DeliveryDate = dalOrder.DeliveryDate,
                    TotalPrice = 0,
                    Status = BO.OrderStatus.delivered,
                    Items = (from item in orderItems
                             select new BO.OrderItem()
                             {

                                 ID = (item?.OrderItemID) ?? 0,
                                 Name = dal.Product.Get((item?.ProductID) ?? 0).Name,
                                 ProductID = (item?.ProductID) ?? 0,
                                 Price = (item?.Price) ?? 0,
                                 Amount = (item?.Amount) ?? 0,
                                 TotalPrice = (item?.Price) ?? 0 * (item?.Amount) ?? 0
                             }).ToList()
                };
                blOrder.TotalPrice = blOrder.Items.Sum(x => x?.TotalPrice ?? 0);
                return blOrder;
            }
            throw new BO.Exceptions.NotLegal("this is not legal details of order");
        }

        catch (DO.NotExist ex)
        {
            throw new BO.Exceptions.NotExist(ex.Message, ex);
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
            order = dal!.Order.Get(id);

            if (order.ID != 0)
                exist = true;
            orderTracking.ID = (order.ID);

            if (order.OrderDate != null)
            {
                orderTracking.Status = BO.OrderStatus.payment;
                orderTracking.Tracking.Add(new Tuple<DateTime?, string?>(order.OrderDate, "Order Payment"));

                if (order.ShipDate != null)
                {
                    orderTracking.Status = BO.OrderStatus.shiped;
                    orderTracking.Tracking.Add(new Tuple<DateTime?, string?>(order.ShipDate, "Order Shiped"));

                    if (order.DeliveryDate != null)
                    {
                        orderTracking.Status = BO.OrderStatus.delivered;
                        orderTracking.Tracking.Add(new Tuple<DateTime?, string?>(order.OrderDate, "Order delivered"));
                    }
                }
            }
           
        }

        catch (DO.NotExist ex)
        {
            throw new BO.Exceptions.NotExist(ex.Message, ex);
        }

        if (!exist)
            throw new BO.Exceptions.NotExist("the order does not exist");

        return orderTracking;
    }
}

