

using System.Xml;
using System.Xml.Linq;

namespace DO;

/// <summary>
/// structure for OrderItem
/// </summary>
public struct OrderItem
{
    /// <summary>
    /// Unique ID of Product
    /// </summary>
    public int ProductID { get; set; }
    /// <summary>
    /// Unique ID of Order
    /// </summary>
    public int OrderID { get; set; }
    /// <summary>
    /// the price of Product
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// the amount of Product
    /// </summary>
    public int Amount { get; set; }


    public override string ToString() =>
       $@"
        Product ID={ProductID}, 
        Order ID - {OrderID}
    	Price: {Price}
    	Amount: {Amount}
       ";

}
