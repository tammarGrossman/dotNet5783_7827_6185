
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
    /// <summary>
    /// consructor
    /// </summary>
    public OrderTracking()
    {
        Tracking = new List<Tuple<DateTime, string>>();
    }
    /// <summary>
    /// the function prints object values
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return Descriptions.Description(this);
    }
}
