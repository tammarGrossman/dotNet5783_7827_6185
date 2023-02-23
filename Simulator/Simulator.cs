using BlApi;
using BO;

namespace Simulator
{
    public static class Simulator
    {
        private static BlApi.IBl? bl = BlApi.Factory.Get();
        private static EventHandler<OrderStatusUpdateEventArgs> orderStatusChanged;
        private static EventHandler<EventArgs> endSimulator;
        private static volatile bool active;
        public static void initilize()
        {
            Thread t = new Thread(RunSimulator);
            active = true;
            t.Start();
        }

        private static void RunSimulator(object? obj)
        {
            Random rn = new Random();
            while (active)
            {
                DO.Order order = bl!.Order.FindOrderToUpdate();
                if (order.ID != 0)
                {
                    int seconds = rn.Next(3, 11);
                    var nextStatus = OrderStatus.payment;
                    var estTime = DateTime.Now.AddSeconds(seconds);
                    var currentStatus = bl.Order.TrackOrder(order.ID).Status;
                    if (currentStatus == OrderStatus.payment)
                        nextStatus = OrderStatus.shiped;
                    else if (currentStatus == OrderStatus.shiped)
                        currentStatus = OrderStatus.delivered;

                    orderStatusChanged?.Invoke(null, new OrderStatusUpdateEventArgs(order.ID, currentStatus, nextStatus, DateTime.Now, estTime));
                    Thread.Sleep(seconds * 1000);
                    if (nextStatus == OrderStatus.shiped)
                        bl.Order.UpdateSend(order.ID);

                    else
                        bl.Order.UpdateSupply(order.ID);

                }
                Thread.Sleep(1000);
            }
            endSimulator?.Invoke(null, new());
        }

        public static void stop()
        {
            active = false;
        }

        public static void RegisterOrderStatusEvent(EventHandler<OrderStatusUpdateEventArgs> changeStatusOfOrder)
        {
            orderStatusChanged += changeStatusOfOrder;
        }

        public static void UnRegisterOrderStatusEvent(EventHandler<OrderStatusUpdateEventArgs> changeStatusOfOrder)
        {
            orderStatusChanged -= changeStatusOfOrder;
        }


        public static void RegisterEndEvent(EventHandler<EventArgs> endStatus)
        {
            endSimulator += endStatus;
        }

        public static void UnRegisterEndEvent(EventHandler<EventArgs> endStatus)
        {
            endSimulator -= endStatus;
        }
    }

    public class OrderStatusUpdateEventArgs : EventArgs
    {
        public int OrderId { get; set; }
        public OrderStatus? CurrentStatus { get; set; }
        public OrderStatus? NextStatus { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EstimatedTime { get; set; }

        public OrderStatusUpdateEventArgs(int orderId, OrderStatus? currentStatus, OrderStatus? nextStatus, DateTime startTime, DateTime estimatedTime)//add all props
        {
            OrderId = orderId;
            CurrentStatus = currentStatus;
            NextStatus = nextStatus;
            StartTime = startTime;
            EstimatedTime = estimatedTime;
        }
    }


}

