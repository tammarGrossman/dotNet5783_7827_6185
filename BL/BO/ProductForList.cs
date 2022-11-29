

namespace BO;

public class ProductForList
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
    public Category Category_ { get; set; }
    public override string ToString()
    {
        return Descriptions.Description(this);
    }
}
