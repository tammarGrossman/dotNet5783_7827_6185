///

using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Xml.Linq;
namespace DO;

/// <summary>
/// structure for Order
/// </summary>
public struct Order
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
    /// the email of customer
    /// </summary>
    public string CustomerEmail { get; set; }
    /// <summary>
    /// the adress of customer
    /// </summary>
    public string CustomerAdress { get; set; }
    /// <summary>
    /// the order date 
    /// </summary>
    public DateTime OrderDate { get; set; }
    /// <summary>
    /// the ship date 
    /// </summary>
    public DateTime ShipDate { get; set; }
    /// <summary>
    /// the delivery date 
    /// </summary>
    public DateTime DeliveryDate { get; set; }
    /// <summary>
    /// print object details
    /// </summary>
    /// <returns></returns>
    public override string ToString() =>
       $@"
        Order ID={ID}
        Customer Name - {CustomerName}
    	Customer Email: {CustomerEmail}
    	Customer Adress: {CustomerAdress}
        Order Date - {OrderDate}
    	ShipDate: {ShipDate}
    	DeliveryDate: {DeliveryDate}
       ";

}
