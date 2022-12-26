
namespace DO;
/// <summary>
/// the not exist exeption
/// </summary>
public class NotExist:Exception
{
    int id;
    string name;
     
        public NotExist(int id_,string name_):base()
        {
        id = id_;
        name = name_;
        }
    public NotExist(int id_, string name_,string message) : base(message)
    {
        id = id_;
        name = name_;
    }
    public override string ToString()
    {
        return $"{name} id {id} does not exist";
    }

}
/// <summary>
/// the dupplicate exeption
/// </summary>
public class Duplication:Exception
{
    int id;
    string name;

    public Duplication(int id_, string name_) : base()
    {
        id = id_;
        name = name_;
    }
    public Duplication(int id_, string name_, string message) : base(message)
    {
        id = id_;
        name = name_;
    }
   
    public override string ToString()
    {
        return $"{name} id {id} already exist";
    }
}
public class DalConfigException : Exception
{
    public DalConfigException(string msg) : base(msg) { }
    public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
}
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
public class MissingInputValue : Exception
{
        string property;
        public MissingInputValue(string property_) : base()
        {
            this.property = property_;
        }
        public override string ToString()
        {
            return base.ToString() + $"the {property} input missing value";
        }  
}
 


