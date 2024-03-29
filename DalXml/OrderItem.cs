﻿
using System.Xml.Linq;
namespace Dal;
using DalApi;
using DO;
using System.Linq;

internal class OrderItem : IOrderItem
{
    const string s_orderItems = "orderItems";
    /// <summary>
    /// add order item
    /// </summary>
    /// <param name="oI"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public int Add(DO.OrderItem oI)
    {

        XElement elem = XElement.Load(@"..\xml\config.xml");
        var id = elem.Element("lastOrderItemID");
        int idO = Convert.ToInt32(id!.Value);
        List<DO.OrderItem?> orderItems = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);
        if (orderItems.FirstOrDefault(orderItem => orderItem?.OrderItemID == oI.OrderItemID) != null)
            throw new Duplication(oI.OrderItemID, "orderItem");
        oI.OrderItemID = idO + 1;
        orderItems.Add(oI);
        elem.Element("lastOrderItemID")!.SetValue(idO + 1);
        elem.Save(@"..\xml\config.xml");
        XmlTools.SaveListToXMLSerializer(orderItems, s_orderItems);

        return oI.OrderItemID;
    }

    /// <summary>
    /// get order item
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public DO.OrderItem Get(int id)
    {
        List<DO.OrderItem?> orderItems = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);
        return orderItems.FirstOrDefault(oI => oI?.OrderItemID == id) ?? throw new NotExist(id, "order item");
        throw new NotExist(id, "order item");
    }

    /// <summary>
    /// get order item by ids
    /// </summary>
    /// <param name="pId"></param>
    /// <param name="oId"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public DO.OrderItem GetOrderItemByIDS(int pId, int oId)
    {
        List<DO.OrderItem?> orderItems = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);

        return orderItems.FirstOrDefault(orderItem => orderItem?.OrderID == oId && orderItem?.ProductID == pId) ?? throw new DO.NotExist(0, "order item");
    }
    /// <summary>
    /// get all order items
    /// </summary>
    /// <returns></returns>
    public IEnumerable<DO.OrderItem?> GetAll(Func<DO.OrderItem?, bool>? Condition = null)
    {

        List<DO.OrderItem?> orderItems = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);
        if (Condition != null)
            return orderItems.Where(Condition).OrderBy(oI => oI?.OrderItemID);
        return orderItems.Select(oI => oI).OrderBy(oI => oI?.OrderItemID);
    }

    /// <summary>
    /// delete order item
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>

    public void Delete(int id)
    {
        List<DO.OrderItem?> orderItems = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);
        if (orderItems.RemoveAll(oI => oI?.OrderItemID == id) == 0)
            throw new NotExist(id, "order item");

        XmlTools.SaveListToXMLSerializer(orderItems, s_orderItems);

    }

    /// <summary>
    /// update order item
    /// </summary>
    /// <param name="oI"></param>
    /// <exception cref="Exception"></exception>
    public void Update(DO.OrderItem oI)
    {
        Delete(oI.OrderItemID);
        Add(oI);
    }
    /// <summary>
    /// get order item by condition
    /// </summary>
    /// <param name="Condition"></param>
    /// <returns></returns>
    /// <exception cref="NotExist"></exception>
    public DO.OrderItem GetByCon(Func<DO.OrderItem?, bool> Condition)
    {
        List<DO.OrderItem?> orderItems = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);

        return orderItems.FirstOrDefault(Condition) ?? throw new NotExist(0, "order item");


    }

    /// <summary>
    /// get order items in order
    /// </summary>
    /// <param name="oIID"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public IEnumerable<DO.OrderItem?> GetProductsInOrder(int oID)
    {
        List<DO.OrderItem?> orderItems = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);
        List<DO.OrderItem?> OrderItemsList = orderItems.FindAll(order => order?.OrderID == oID);
        if (OrderItemsList.Count() > 0)
            return OrderItemsList;
        throw new NotExist(oID, "order item");
    }
}


