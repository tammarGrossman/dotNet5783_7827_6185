
using BlApi;
using BO;
using DalApi;
using DO;
using static BO.Exceptions;

namespace BlImplementation;

internal class Order:IOrder
{
      DalApi.Idal dal = new Dal.DalList();  
    public IEnumerable<BO.OrderForList> GetAll()
    {
        List<BO.OrderForList> orders = new List<BO.OrderForList>();
        List<BO.OrderItem> items = new List<BO.OrderItem>();
        foreach (var item in idal.Order.GetAll())
        {
            BO.OrderForList order = new BO.OrderForList();
            order.ID = item.ID;
            order.CustomerName = item.CustomerName;
            order.CustomerAddres = item.CustomerAddres;
            order.CustomerEmail = item.CustomerEmail;
            order.PaymentDate = item.OrderDate;
            order.ShipDate = item.ShipDate;
            order.DeliveryDate = item.DeliveryDate;
            order.Status = item.Status;
            order.TotalPrice = item.TotalPrice;
            //foreach(var orderItem in item.Items)
            //{
            //    items.Add(orderItem);
            //}
            orders.Add(order);
        }
        return orders;
    }

    public Order Get(int id)
    {
        //check what to do with productName
        // DO.Product DalProduct=dal.Product.Get(id);
        if (id > 0)
        {
            double totalPrice;
            BO.Order BlOrder = new BO.Order();
            DO.Order DalOrder = dal.Order.Get(id);
            BO.OrderItem BlorderItem = new BO.OrderItem();
            foreach (OrderItem item in dal.OrderItem.GetProductByID(id))
            {
                BlorderItem.OrderItemID = item.OrderItemID;
                BlorderItem.ProductID = item.ProductID;
                BlorderItem.OrderID = item.OrderID;
                BlorderItem.Price = item.Price;
                BlorderItem.Amount = item.Amount;
                BlOrder.Items.Add(BlorderItem);
                totalPrice += BlorderItem.Price * BlorderItem.Amount;
            } 
            BlOrder.ID = DalOrder.ID;
            BlOrder.CustomerName = DalOrder.CustomerName;
            BlOrder.CustomerEmail = DalOrder.CustomerEmail;
            BlOrder.CustomerAddres = DalOrder.CustomerAddres;
            BlOrder.PaymentDate = DalOrder.OrderDate;
            BlOrder.ShipDate = DalOrder.ShipDate;
            BlOrder.DeliveryDate = DalOrder.DeliveryDate;
            BlOrder.TotalPrice = totalPrice;
           // BlOrder.Status;//check what to do with status
            return BlOrder;
        }
        else
            throw new NotLegal();
    }

    public Order UpdateSend(int id)
    {
        double totalPrice;
       DO.Order dalOrder=dal.Order.Get(id);
       BO.Order blOrder=new BO.Order();
        BO.OrderItem BlorderItem = new BO.OrderItem();
        DO.Order DOorder=new DO.Order();
        if (dalOrder.ShipDate==DateTime.MinValue)
        {
            DOorder.ID = dalOrder.ID;
            DOorder.CustomerName = dalOrder.CustomerName;
            DOorder.CustomerEmail = dalOrder.CustomerEmail;
            DOorder.CustomerAddres = dalOrder.CustomerAddres;
            DOorder.OrderDate = dalOrder.OrderDate;
            DOorder.DeliveryDate = dalOrder.DeliveryDate;
            DOorder.ShipDate = DateTime.Now();
            dal.Order.Update(DOorder)
            foreach (OrderItem item in dal.OrderItem.GetProductByID(id))
            {
                BlorderItem.OrderItemID = item.OrderItemID;
                BlorderItem.ProductID = item.ProductID;
                BlorderItem.OrderID = item.OrderID;
                BlorderItem.Price = item.Price;
                BlorderItem.Amount = item.Amount;
                BlOrder.Items.Add(BlorderItem);
                totalPrice += BlorderItem.Price * BlorderItem.Amount;
            }
            blOrder.ID = DOorder.ID;
            blOrder.CustomerName = DOorder.CustomerName;
            blOrder.CustomerEmail = DOorder.CustomerEmail;
            blOrder.CustomerAddres = DOorder.CustomerAddres;
            blOrder.PaymentDate = DOorder.OrderDate;
            blOrder.ShipDate = DOorder.ShipDate;
            blOrder.DeliveryDate = DOorder.DeliveryDate;
            blOrder.TotalPrice = totalPrice;
            blOrder.Status = OrderStatus.sent;
            return blOrder;
        }
        throw new NotLegal();
    }
    public Order UpdateSupply(int id)
    {
        double totalPrice;
        DO.Order dalOrder = dal.Order.Get(id);
        BO.Order blOrder = new BO.Order();
        BO.OrderItem BlorderItem = new BO.OrderItem();
        DO.Order DOorder = new DO.Order();
        if (dalOrder.DeliveryDate == DateTime.MinValue)
        {
            DOorder.ID = dalOrder.ID;
            DOorder.CustomerName = dalOrder.CustomerName;
            DOorder.CustomerEmail = dalOrder.CustomerEmail;
            DOorder.CustomerAddres = dalOrder.CustomerAddres;
            DOorder.OrderDate = dalOrder.OrderDate;
            DOorder.ShipDate = dalOrder.ShipDate;
            DOorder.DeliveryDate = DateTime.Now();
            dal.Order.Update(DOorder)
            foreach (OrderItem item in dal.OrderItem.GetProductByID(id))
            {
                BlorderItem.OrderItemID = item.OrderItemID;
                BlorderItem.ProductID = item.ProductID;
                BlorderItem.OrderID = item.OrderID;
                BlorderItem.Price = item.Price;
                BlorderItem.Amount = item.Amount;
                BlOrder.Items.Add(BlorderItem);
                totalPrice += BlorderItem.Price * BlorderItem.Amount;
            }
            blOrder.ID = DOorder.ID;
            blOrder.CustomerName = DOorder.CustomerName;
            blOrder.CustomerEmail = DOorder.CustomerEmail;
            blOrder.CustomerAddres = DOorder.CustomerAddres;
            blOrder.PaymentDate = DOorder.OrderDate;
            blOrder.ShipDate = DOorder.ShipDate;
            blOrder.TotalPrice = totalPrice;
            blOrder.DeliveryDate = DOorder.DeliveryDate;
            blOrder.Status = OrderStatus.received;
            return blOrder;
        }
        throw new NotLegal();
    }
    public OrderTracking TrackOrder(int id)
    {
}
}
