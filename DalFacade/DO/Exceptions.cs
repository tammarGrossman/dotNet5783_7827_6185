
namespace DO;

public class NotExist:Exception
{
   public NotExist(string message) : base(message) { }
}
public class Duplication:Exception
{
    Duplication(string message) : base(message) { }
}
