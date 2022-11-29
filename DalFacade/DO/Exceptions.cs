
namespace DO;

public class NotExist:Exception
{
   public NotExist(string message) : base(message) { }
}
public class Duplication:Exception
{
   public  Duplication(string message) : base(message) { }
}
