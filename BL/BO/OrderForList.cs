
using System.Reflection;

namespace BO;

public class OrderForList
{
    /// <summary>
    /// Unique ID of Order
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// the name of customer
    /// </summary>
    public string CustomerName { get; set; }
    /// <summary>
    /// The status of the order
    /// </summary>
    public OrderStatus Status { get; set; }
    /// <summary>
    /// sum of all the order
    /// </summary>
    public double TotalPrice { get; set; }
    /// <summary>
    /// the amount of items in the orders list
    /// </summary>
    public int AmountOfItems { get; set; }
    public override string ToString()
    {
        return Descriptions.Description(this);
    }
}
