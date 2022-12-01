
namespace BO;
public class Exceptions
{
    /// <summary>
    /// exception when objects are not found
    /// </summary>
    public class NotFound:Exception
    {
       public NotFound(string message) : base(message) { }
    }
    /// <summary>
    /// exception when objects are not exist
    /// </summary>
    public class NotExist : Exception
    {
        public NotExist(string message) : base(message) { }
    }
    /// <summary>
    /// exception when objects are  duplicate
    /// </summary>
    public class Duplication:Exception
    {
       public Duplication(string message) : base(message) { }
    }
    /// <summary>
    /// exception when objects are not legal
    /// </summary>
    public class NotLegal : Exception
    {
        public NotLegal(string message) : base(message) { }
    }
}
