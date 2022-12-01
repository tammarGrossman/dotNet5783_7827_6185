
namespace BlApi;

public interface IBl
{
    /// <summary>
    /// the interfaces of order product and cart
    /// </summary>
    public IOrder Order { get;  }
    public IProduct Product { get; }   
    public ICart Cart { get;  }

}
