
namespace BlApi;

public interface IBl
{
    public IOrder Order { get; }
    public IProduct Product { get; }
    public IOrderItem OrderItem { get; }
    public IProductItem ProductItem { get; }
    public ICart Cart { get; }
    public IProductForList ProductForList { get; }
    public IOrderForList OrderForList { get; }
    public IOrderTracking OrderTracking { get; }
  
}
