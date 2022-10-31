
namespace DO;

/// <summary>
/// structure for Product
/// </summary>
public struct Product
{
    /// <summary>
    /// Unique ID of Product
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// the name of Product
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// the price of Product
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// the category of Product
    /// </summary>
    public Category Category { get; set; }
    /// <summary>
    /// if the product is in stock
    /// </summary>
    public int InStock { get; set; }

    public override string ToString() =>
        $@"
        Product ID={ID}: {Name}, 
        category - {Category}
    	Price: {Price}
    	Amount in stock: {InStock}
       ";
}
