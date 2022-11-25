

namespace BO;

public class OrderItem
{
    /// <summary>
    /// the id of object
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// the name of object
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// the product id of object
    /// </summary>
    public int ProductID { get; set; }
    /// <summary>
    /// the price of object
    /// </summary>
    public double  Price { get; set; }
    /// <summary>
    /// the amount of object
    /// </summary>
    public int Amount { get; set; }
    /// <summary>
    /// the total price of object
    /// </summary>
    public double TotalPrice { get; set; }
}
