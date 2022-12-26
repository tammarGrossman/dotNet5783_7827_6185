
namespace BO;
public class Exceptions
{
    /// <summary>
    /// exception when objects are not found
    /// </summary>
    public class NotFound:Exception
    {
        int id;
        string name;
       public NotFound(int id_,string name_):base()
        {
            id=id_;
            name=name_;
        }
        public NotFound(int id_, string name_, string message) : base(message)
        {
            id = id_;
            name = name_;
        }
        public NotFound(int id_, string name_, string message, Exception innerException) : base(message,innerException)
        {
            id = id_;
            name = name_;
        }
        public override string ToString()
        {
            return base.ToString()+ "name:" + name+" id:" +id+"Not Found";
        }
    }
    /// <summary>
    /// exception when objects are not exist
    /// </summary>
    public class NotExist : Exception
    {
        public NotExist(string message) : base(message) 
        { 
        }
        public NotExist(string message,Exception innerException) : base(message, innerException)
        {
        }
        public override string ToString()
        {
            return base.ToString()+ "Not Exist";
        }
    }
    /// <summary>
    /// exception when objects are  duplicate
    /// </summary>
    public class Duplication:Exception
    {
        public Duplication() : base() {}
        public Duplication( string message) : base(message) {}                  
        public Duplication( string message, Exception innerException) : base(message, innerException) {}
        
          
        
        public override string ToString()
        {
            return base.ToString() + "already exist";
        }
    }
    /// <summary>
    /// exception when objects are not legal
    /// </summary>
    public class NotLegal : Exception
    {
  
        public NotLegal(string message="details") : base(message)
        {
        }
        public override string ToString()
        {
            return base.ToString() + "Not Legal";
        }
    }
    /// <summary>
    /// exception when cannot connect to db
    /// </summary>
    public class DBConnectionFailed : Exception
    {

        public DBConnectionFailed(string message = "cannot connected to db") : base(message)
        {
        }
        public override string ToString()
        {
            return base.ToString() + "DBConnectionFailed";
        }
    }
    /// <summary>
    /// exception when there is a missing input value
    /// </summary>
    public class MissingInputValue : Exception
    {
        string property;
        public MissingInputValue(string property_ ) : base()
        {
            this.property = property_;
        }
        public override string ToString()
        {
            return base.ToString() + $"the {property} input missing value";
        }
    }
}
