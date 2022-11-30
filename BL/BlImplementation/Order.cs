
using BlApi;
using BO;
using static BO.Exceptions;
namespace BlImplementation;
internal class Order : IOrder
{
    DalApi.IDal dal = new Dal.Dallist();
    public IEnumerable<BO.OrderForList> GetAll()
    {
        int amountOfProInOrd = 0;
        double totalPriceInOrd = 0;
        List<BO.OrderForList> orders = new List<BO.OrderForList>();
        foreach (DO.Order item in dal.Order.GetAll())
        {
            BO.OrderForList order = new BO.OrderForList();
            IEnumerable<DO.OrderItem> proInOr = dal.OrderItem.GetProductsInOrder(order.ID);
            foreach (DO.OrderItem item2 in proInOr)
            {
                amountOfProInOrd += item2.Amount;
                totalPriceInOrd += item2.Price * item2.Amount;
            }
            order.ID = item.ID;
            order.CustomerName = item.CustomerName;
            order.Status = TrackOrder(item.ID).Status;
            order.TotalPrice = totalPriceInOrd;
            order.AmountOfItems = amountOfProInOrd;
            orders.Add(order);
        }
        return orders;
    }

    public BO.Order Get(int id)
    {
        try
        {
            if (id > 0)
            {
                double totalPrice = 0;
                BO.Order BlOrder = new BO.Order();
                DO.Order DalOrder = dal.Order.Get(id);
                BO.OrderItem BlorderItem = new BO.OrderItem();
                foreach (var item in dal.OrderItem.GetProductsInOrder(id))
                {
                    BlorderItem.ID = item.OrderItemID;
                    BlorderItem.Name = dal.Product.Get(item.ProductID).Name;
                    BlorderItem.ProductID = item.ProductID;
                    BlorderItem.Price = item.Price;
                    BlorderItem.Amount = item.Amount;
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
                blOrder.Status = OrderStatus.sent;
                return blOrder;
            }
             throw new NotLegal("this is not a legal value of order");
        }
        catch (Exception ex)
        {
            throw new BO.Exceptions.NotExist(ex.Message);
        }
    }
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
                blOrder.CustomerEmail = DOorder.CustomerEmail;
                blOrder.CustomerAdress = DOorder.CustomerAdress;
                blOrder.PaymentDate = DOorder.OrderDate;
                blOrder.ShipDate = DOorder.ShipDate;
                blOrder.DeliveryDate = DOorder.DeliveryDate;
                blOrder.TotalPrice = totalPrice;
                blOrder.Status = OrderStatus.received;
                return blOrder;
            }
            throw new NotLegal("this is not legal details of order");
        }
        catch (Exception ex)
        {
            throw new NotExist(ex.Message);
        }
    }
    public OrderTracking TrackOrder(int id)
    {
        OrderTracking orderTracking = new OrderTracking();
        bool exist = false;
        foreach (DO.Order item in dal.Order.GetAll())
        {
            if (item.ID == id)
            {
                exist = true;
                orderTracking.ID = item.ID;
                if (item.DeliveryDate > DateTime.MinValue)
                {
                    orderTracking.Status = OrderStatus.received;
                    orderTracking.Tracking.Add(new Tuple<DateTime, string>(item.DeliveryDate, "Order delivered"));
                }
                else if (item.ShipDate > DateTime.MinValue)
                {
                    orderTracking.Status = OrderStatus.sent;
                    orderTracking.Tracking.Add(new Tuple<DateTime, string>(item.ShipDate, "Order Sent"));
                }
                else
                {
                    orderTracking.Status = OrderStatus.ordered;
                    orderTracking.Tracking.Add(new Tuple<DateTime,string>(item.OrderDate.Date, "Order recieved"));
                }
            }
        }
        if (!exist)
            throw new NotExist("the order does not exist");
        return orderTracking;
    }
}

