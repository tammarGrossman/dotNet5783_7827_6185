﻿
using DO;
using System.Reflection;

namespace BO;

public class Order
{
    
    /// <summary>
    /// Unique ID of Order
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// the name of customer
    /// </summary>
    public string? CustomerName { get; set; }
    /// <summary>
    /// the email of customer
    /// </summary>
    public string? CustomerEmail { get; set; }
    /// <summary>
    /// the adress of customer
    /// </summary>
    public string? CustomerAdress { get; set; }
    /// <summary>
    /// the order date 
    /// </summary>


    public DateTime? PaymentDate { get; set; }
    /// <summary>
    /// the ship date 
    /// </summary>
    public DateTime? ShipDate { get; set; }
    /// <summary>
    /// the delivery date 
    /// </summary>
    public DateTime? DeliveryDate { get; set; }
    /// <summary>
    /// The status of the order
    /// </summary>
    public OrderStatus? Status { get; set; }
    /// <summary>
    /// sum of all the order
    /// </summary>
    public double TotalPrice { get; set; }
    /// <summary>
    /// the list of the items in the order
    /// </summary>
    public List<OrderItem?> Items { get; set; }
    /// <summary>
    /// constructor
    /// </summary>
    public Order()
    {
        Items = new List<OrderItem?>();
    }


/// <summary>
/// print object details
/// </summary>
/// <returns></returns>
public override string ToString()
    {
        return Descriptions.Description(this);
    }
}
