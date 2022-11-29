
using BlApi;
using BO;
using static BO.Exceptions;
namespace BlImplementation;
internal class Order:IOrder
{
      DalApi.IDal dal = new Dal.Dallist();  
    public IEnumerable<BO.OrderForList> GetAll()
    {
        List<BO.OrderForList> orders = new List<BO.OrderForList>();
       // List<BO.OrderItem> items = new List<BO.OrderItem>();
        foreach (var item in dal.Order.GetAll())
        {
            BO.OrderForList order = new BO.OrderForList();
            order.ID = item.ID;
            order.CustomerName = item.CustomerName;
            //order.CustomerAddres = item.CustomerAddres;
            //order.CustomerEmail = item.CustomerEmail;
            //order.PaymentDate = item.OrderDate;
            //order.ShipDate = item.ShipDate;
            //order.DeliveryDate = item.DeliveryDate;
            //order.Status = ?;
            //order.TotalPrice = ?;
            //order.AmountOfItems=?
            //foreach(var orderItem in item.Items)
            //{
            //    items.Add(orderItem);
            //}
            orders.Add(order);
        }
        return orders;
    }

    public BO.Order Get(int id)
    {
        try
        {
            //check what to do with productName
            // DO.Product DalProduct=dal.Product.Get(id);
            if (id > 0)
            {
                double totalPrice=0;
                BO.Order BlOrder = new BO.Order();
                DO.Order DalOrder = dal.Order.Get(id);
                BO.OrderItem BlorderItem = new BO.OrderItem();
                foreach (var item in dal.OrderItem.GetProductsInOrder(id))
                {
                    BlorderItem.ID = item.OrderItemID;
                    BlorderItem.ProductID = item.ProductID;
                    BlorderItem.Price = item.Price;
                    BlorderItem.Amount = item.Amount;
                    BlOrder.Items.Add(BlorderItem);
                    totalPrice += BlorderItem.Price * BlorderItem.Amount;
                }
                BlOrder.ID = DalOrder.ID;
                BlOrder.CustomerName = DalOrder.CustomerName;
                //BlOrder.CustomerEmail = DalOrder.CustomerEmail;
                //BlOrder.CustomerAddres = DalOrder.CustomerAddres;
                BlOrder.PaymentDate = DalOrder.OrderDate;
                BlOrder.ShipDate = DalOrder.ShipDate;
                BlOrder.DeliveryDate = DalOrder.DeliveryDate;
                BlOrder.TotalPrice = totalPrice;
                // BlOrder.Status;//check what to do with status
                return BlOrder;
            }
            else
                throw new NotLegal("ggg");
        }
        catch(Exception ex)
        {
            throw new NotExist("ex");
        }
    }

    public BO.Order UpdateSend(int id)
    {
        try { 
        double totalPrice=0;
        DO.Order dalOrder=dal.Order.Get(id);
        BO.Order blOrder=new BO.Order();
        OrderItem BlorderItem = new BO.OrderItem();
        DO.Order DOorder=new DO.Order();
        if (dalOrder.ShipDate==DateTime.MinValue)
        {
            DOorder.ID = dalOrder.ID;
            DOorder.CustomerName = dalOrder.CustomerName;
            //DOorder.CustomerEmail = dalOrder.CustomerEmail;
            //DOorder.CustomerAddres = dalOrder.CustomerAddres;
            DOorder.OrderDate = dalOrder.OrderDate;
            DOorder.DeliveryDate = dalOrder.DeliveryDate;
            DOorder.ShipDate = DateTime.Now;
            dal.Order.Update(DOorder);
            foreach (DO.OrderItem item in dal.OrderItem.GetProductsInOrder(id))
            {
                BlorderItem.ID = item.OrderItemID;
                BlorderItem.ProductID = item.ProductID;
                BlorderItem.Price = item.Price;
                BlorderItem.Amount = item.Amount;
                blOrder.Items.Add(BlorderItem);
                totalPrice += BlorderItem.Price * BlorderItem.Amount;
            }
            blOrder.ID = DOorder.ID;
            blOrder.CustomerName = DOorder.CustomerName;
            //blOrder.CustomerEmail = DOorder.CustomerEmail;
            //blOrder.CustomerAddres = DOorder.CustomerAddres;
            blOrder.PaymentDate = DOorder.OrderDate;
            blOrder.ShipDate = DOorder.ShipDate;
            blOrder.DeliveryDate = DOorder.DeliveryDate;
            blOrder.TotalPrice = totalPrice;
            blOrder.Status = OrderStatus.sent;
            return blOrder;
        }
        throw new NotLegal("bbb");
        }
        catch (Exception ex)
        {
            //throw new NotExist(ex);
            throw new NotExist("ex");
        }
    }
    public BO.Order UpdateSupply(int id)
    {
        try { 
        double totalPrice=0;
        DO.Order dalOrder = dal.Order.Get(id);
        BO.Order blOrder = new BO.Order();
        BO.OrderItem BlorderItem = new BO.OrderItem();
        DO.Order DOorder = new DO.Order();
        if (dalOrder.DeliveryDate == DateTime.MinValue)
        {
            DOorder.ID = dalOrder.ID;
            DOorder.CustomerName = dalOrder.CustomerName;
            //DOorder.CustomerEmail = dalOrder.CustomerEmail;
            //DOorder.CustomerAddres = dalOrder.CustomerAddres;
            DOorder.OrderDate = dalOrder.OrderDate;
            DOorder.ShipDate = dalOrder.ShipDate;
            DOorder.DeliveryDate = DateTime.Now;
                dal.Order.Update(DOorder);
            foreach (var item in dal.OrderItem.GetProductsInOrder(id))
            {
                BlorderItem.ID = item.OrderItemID;
                BlorderItem.ProductID = item.ProductID;
                BlorderItem.Price = item.Price;
                BlorderItem.Amount = item.Amount;
                blOrder.Items.Add(BlorderItem);
                totalPrice += BlorderItem.Price * BlorderItem.Amount;
            }
            blOrder.ID = DOorder.ID;
            blOrder.CustomerName = DOorder.CustomerName;
            //blOrder.CustomerEmail = DOorder.CustomerEmail;
            //blOrder.CustomerAddres = DOorder.CustomerAddres;
            blOrder.PaymentDate = DOorder.OrderDate;
            blOrder.ShipDate = DOorder.ShipDate;
            blOrder.TotalPrice = totalPrice;
            blOrder.DeliveryDate = DOorder.DeliveryDate;
            blOrder.Status = OrderStatus.received;
            return blOrder;
        }
        throw new NotLegal("NNN");
        }
        catch (Exception ex)
        {
            // throw new NotExist(ex);
            throw new NotExist("hhh");
        }
    }
    public OrderTracking TrackOrder(int id)
    {
        Console.WriteLine("help");
        OrderTracking orderTracking = new OrderTracking();
        return orderTracking;
    }
}
