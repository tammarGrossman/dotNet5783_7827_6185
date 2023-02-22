using BO;

namespace Simulator
{
    public static class Simulator
    {
        private static EventHandler<OrderStatusUpdateEventArgs> orderStatusChanged;
        private static volatile bool active;
        public static void initilize()
        {
            Thread t = new Thread(RunSimulator);
            active = true;
        }

        private static void RunSimulator(object? obj)
        {
            while (active)
            {
                Order order;//==FindOrderToUpdate
                if (order != null)
                {
                    int rn=0;//rand(3,10)
                    //calculate next status and estimated time
                    orderStatusChanged?.Invoke(null, new OrderStatusUpdateEventArgs(order.ID, order.Status));
                    Thread.Sleep(rn * 1000);
                    //bl.update status
                }
                Thread.Sleep(1000);
            }
            //raise event of end simultor
        }

        public static void stop()
        {
            active = false;
        }

        public static void RegisterOrderStatusEvent(EventHandler<OrderStatusUpdateEventArgs> changeStatusOfOrder)
        {
            orderStatusChanged += changeStatusOfOrder;
        }

        public static void UnregisterOrderStatusEvent(EventHandler<OrderStatusUpdateEventArgs> changeStatusOfOrder)
        {
            orderStatusChanged -= changeStatusOfOrder;
        }
    }

    public class OrderStatusUpdateEventArgs : EventArgs
    {
        int OrderId { get; set; }
        OrderStatus? CurrentStatus { get; set; }
        OrderStatus? NextStatus { get; set; }
        DateTime StartTime { get; set; }
        DateTime EstimatedTime { get; set; }

        public OrderStatusUpdateEventArgs(int orderId, OrderStatus? currentStatus)//add all props
        {
            OrderId = orderId;
            CurrentStatus = currentStatus;
        }
    }
}