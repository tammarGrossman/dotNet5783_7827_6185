using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;

internal class Order : IOrder
{
    const string s_orders = "orders";

    /// <summary>
    /// add object
    /// </summary>
    /// <param name="o"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public int Add(DO.Order o)
    {
        XElement elem = XElement.Load(@"..\xml\config.xml");
        var id =elem.Element("lastOrderID");
        int idO = Convert.ToInt32(id.Value);
        List<DO.Order?> orders = XmlTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        if (orders.FirstOrDefault(order => order?.ID == o.ID) != null)
            throw new Duplication(o.ID, "order");
        o.ID = idO+1 ;
          
        orders.Add(o);
        elem.Element("lastOrderID")!.SetValue(idO + 1);
        elem.Save(@"..\xml\config.xml");
        XmlTools.SaveListToXMLSerializer(orders, s_orders);

        return o.ID;
    }


    /// <summary>
    /// get object
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public DO.Order Get(int id)
    {
        List<DO.Order?> orders = XmlTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        return orders.FirstOrDefault(o => o?.ID == id) ?? throw new DO.NotExist(id, "order");
        throw new NotExist(id, "order");
    }


    /// <summary>
    /// delete object
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public void Delete(int id)
    {
        List<DO.Order?> orders = XmlTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        if (orders.RemoveAll(order => order?.ID == id) == 0)
            throw new NotExist(id, "order");

        XmlTools.SaveListToXMLSerializer(orders, s_orders);
    }

    /// <summary>
    /// update object
    /// </summary>
    /// <param name="o"></param>
    /// <exception cref="Exception"></exception>
    public void Update(DO.Order o)
    {
        Delete(o.ID);
        Add(o);
    }

    /// <summary>
    /// get all objects
    /// </summary>
    /// <returns></returns>
    public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? Condition = null)
    {

        List<DO.Order?> orders = XmlTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        if (Condition != null)
            return orders.Where(Condition).OrderBy(o => o?.ID);
        return orders.Select(o => o).OrderBy(o => o?.ID);
    }
    public DO.Order GetByCon(Func<DO.Order?, bool> Condition)
    {
        List<DO.Order?> orders = XmlTools.LoadListFromXMLSerializer<DO.Order>(s_orders);

        return orders.FirstOrDefault(Condition) ?? throw new NotExist(0, "order");
    }
}



