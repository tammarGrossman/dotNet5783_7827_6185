
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
