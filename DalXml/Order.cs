﻿using DalApi;
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
    /// add order
    /// </summary>
    /// <param name="o"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public int Add(DO.Order o)
    {
        if (o.ID == 0)
        {
            XElement elem = XElement.Load(@"..\xml\config.xml");
            var id = elem.Element("lastOrderID");
            int idO = Convert.ToInt32(id!.Value);
            o.ID = idO + 1;
            elem.Element("lastOrderID")!.SetValue(o.ID);
            elem.Save(@"..\xml\config.xml");
        }
        List<DO.Order?> orders = XmlTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        if (orders.FirstOrDefault(order => order?.ID == o.ID) != null)
            throw new Duplication(o.ID, "order");
       
          
        orders.Add(o);       
        XmlTools.SaveListToXMLSerializer(orders, s_orders);

        return o.ID;


       
    }

    /// <summary>
    /// get order
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
    /// delete order
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
    /// update order
    /// </summary>
    /// <param name="o"></param>
    /// <exception cref="Exception"></exception>
    public void Update(DO.Order o)
    {
        Delete(o.ID);
        Add(o);
    }

    /// <summary>
    /// get all orders
    /// </summary>
    /// <returns></returns>
    public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? Condition = null)
    {

        List<DO.Order?> orders = XmlTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        if (Condition != null)
            return orders.Where(Condition).OrderBy(o => o?.ID);
        return orders.Select(o => o).OrderBy(o => o?.ID);
    }
    /// <summary>
    /// get order by condition
    /// </summary>
    /// <param name="Condition"></param>
    /// <returns></returns>
    /// <exception cref="NotExist"></exception>
    public DO.Order GetByCon(Func<DO.Order?, bool> Condition)
    {
        List<DO.Order?> orders = XmlTools.LoadListFromXMLSerializer<DO.Order>(s_orders);

        return orders.FirstOrDefault(Condition) ?? throw new NotExist(0, "order");
    }
}



