
using BlApi;
using Dal;
using DalApi;

namespace BlImplementation;

internal class Order:IOrder
{
    //  DalApi.Idal dal = new Dal.DalList();  
    public IEnumerable<BO.OrderForList> GetAll()
    {
        IDal idal = new Dallist();//change to singletown
        List<BO.OrderForList> orders = new List<BO.OrderForList>();
        List<BO.OrderItem> items = new List<BO.OrderItem>();
        foreach (var item in idal.Order.GetAll())
        {
            BO.OrderForList order = new BO.OrderForList();
            order.CustomerName = item.CustomerName;
            //pr.category = item.CategoryP;  
            order.CustomerAddres = item.CustomerAddres;
            order.CustomerEmail = item.CustomerEmail;
            order.ID = item.ID;
            order.PaymentDate = item.OrderDate;
            order.ShipDate = item.ShipDate;
            order.DeliveryDate = item.DeliveryDate;
            order.Status = item.Status;
            order.TotalPrice = item.TotalPrice;
            foreach(var orderItem in item.Items)
            {
                items.Add(orderItem);
            }

            order.i





            orders.Add(order);
        }
        return orders;

    }
}
