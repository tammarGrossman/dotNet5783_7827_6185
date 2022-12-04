
namespace DO;
/// <summary>
/// the not exist exeption
/// </summary>
public class NotExist:Exception
{
   public NotExist(string message) : base(message) { }
}
/// <summary>
/// the dupplicate exeption
/// </summary>
public class Duplication:Exception
{
   public  Duplication(string message) : base(message) { }
}
