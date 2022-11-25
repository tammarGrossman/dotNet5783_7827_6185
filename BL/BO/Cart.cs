using DO;

namespace BO;
public class Cart
{
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
/// the list of items in the cart
/// </summary>
public List<OrderItem> Items { get; set; }
/// <summary>
/// the total price of all items in the cart
/// </summary>
public double TotalPrice { get; set; }
//לבדוק איך מדפיסים אברי רשימה
/// <summary>
/// print all properties in object
/// </summary>
/// <returns></returns>
public override string ToString() =>
      $@"
        Customer Name - {CustomerName}
    	Customer Email: {CustomerEmail}
    	Customer Adress: {CustomerAdress}
    	Total Price: {TotalPrice}
       ";
}
