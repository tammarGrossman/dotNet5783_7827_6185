
namespace BO;
public class Exceptions
{
    public class NotFound:Exception
    {
       public NotFound(string message) : base(message) { }
    }
    public class Duplication:Exception
    {
       public Duplication(string message) : base(message) { }
    }
    public class NotLegal : Exception
    {
        public NotLegal(string message) : base(message) { }
    }
}
