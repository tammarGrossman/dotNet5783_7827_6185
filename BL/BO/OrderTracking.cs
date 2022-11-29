
using System.Reflection;

namespace BO;

public class OrderTracking
{
    /// <summary>
    /// Unique ID of Order
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// status of Order
    /// </summary>
    public OrderStatus Status { get; set; }
    /// <summary>
    /// list of tuple date time and status of the state of order
    /// </summary>
    public List<Tuple<DateTime, string>> Tracking { get; set; }
    public override string ToString()
    {
        return Descriptions.Description(this);
    }
}
