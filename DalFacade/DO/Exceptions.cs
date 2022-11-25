
namespace DO;

public class NotFound:Exception
{
   public  NotFound(string message) : base(message) { }
}
public class Duplication:Exception
{
    Duplication(string message) : base(message) { }
}
