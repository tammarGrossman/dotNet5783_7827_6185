using Dal;
using DO;
using System;
using System.Data.Common;
namespace DalTest
{
    public class DalTest
    { 
       private static DalOrder dalOrder=new DalOrder();
       private static DalOrderItem dalOrderItem=new DalOrderItem();
       private static DalProduct dalProduct = new DalProduct();
             static void ProductCase()
            {
            
             int action;
             Console.WriteLine("enter 0 to add product");
             Console.WriteLine("enter 1 to show product by id");
             Console.WriteLine("enter 2 to view all products");
             Console.WriteLine("enter 3 to update product");
             Console.WriteLine("enter 4 to  delete product");
             action=int.Parse( Console.ReadLine());
                        switch (action)
                        {
                            case 0:
                    {
                        Product p=new Product();
                        Console.WriteLine("enter product ID:");
                        p.ID = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter product name:");
                        p.Name = Console.ReadLine();
                        Console.WriteLine("enter product price:");
                        p.Price = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter product category:");
                        p.Category= (Category)((Enum)Console.ReadLine());//check
                        Console.WriteLine("enter product in stock:");
                        p.InStock = int.Parse(Console.ReadLine());                     
                        dalProduct.AddProduct(p);
                        break;
                    }
                    case 1:
                    {
                        int id;
                        Console.WriteLine("enter product ID:");
                        id = int.Parse(Console.ReadLine());
                        Console.WriteLine(dalProduct.GetProduct(id));
                        break;
                    }
                        
                    case 2:
                    {
                        foreach (Product item in dalProduct.GetProducts())
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    }
                    case 3:
                    {
                        int id;
                        Console.WriteLine("enter product ID:");
                        id = int.Parse(Console.ReadLine());
                        Console.WriteLine(dalProduct.GetProduct(id));
                        Product p = new Product();
                        Console.WriteLine("enter product ID:");
                        p.ID = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter product name:");
                        p.Name = Console.ReadLine();
                        Console.WriteLine("enter product price:");
                        p.Price = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter product category:");
                        p.Category= (Enums.Category)Enum.Parse(typeof(Enums.Category), Console.ReadLine());
                        Console.WriteLine("enter product in stock:");
                        p.InStock = int.Parse(Console.ReadLine());
                        dalProduct.UpdateProduct(p);
                        break;
                    }
                    case 4:
                    {
                        int id;
                        Console.WriteLine("enter product ID:");
                        id = int.Parse(Console.ReadLine());
                        dalProduct.DeleteProduct(id);
                        break;
                    }
                    default:
                        { 
                        Console.WriteLine("enter again");
                        action=int.Parse( Console.ReadLine());
                        break;
                        }
             } 
                         
            }

            static void OrderCase()
            {
             int action,id;
             Console.WriteLine("enter 0 to add order");
             Console.WriteLine("enter 1 to show order by id");
             Console.WriteLine("enter 2 to view all orders");
             Console.WriteLine("enter 3 to update order");
             Console.WriteLine("enter 4 to  delete order");
             action=int.Parse( Console.ReadLine());
                        switch (action)
                        {
                            case 0:
                    {
                        Order o = new Order();
                        Console.WriteLine("enter order ID:");
                        o.ID = DataSource.Config.LastOrder;
                        Console.WriteLine("enter customer name:");
                        o.CustomerName = Console.ReadLine();
                        Console.WriteLine("enter customer email:");
                        o.CustomerEmail =Console.ReadLine();
                        Console.WriteLine("enter customer adress:");
                        o.CustomerAdress = Console.ReadLine();//check
                        Console.WriteLine("enter order date:");
                        o.OrderDate = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("enter ship date:");
                        o.ShipDate = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("enter delivery date:");
                        o.DeliveryDate = DateTime.Parse(Console.ReadLine());
                        dalOrder.AddOrder(o);
                        break;
                    }
                     case 1:
                    {
                        Console.WriteLine("enter order ID:");
                        id = int.Parse(Console.ReadLine());
                        Console.WriteLine(dalOrder.GetOrder(id));
                        break;
                    }
                     case 2:
                    {
                        foreach (Order item in dalOrder.GetOrders())
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    }
                        
                     case 3:
                    {
                        Order o;
                        Console.WriteLine("enter product ID:");
                        id = int.Parse(Console.ReadLine());
                        Console.WriteLine(dalOrder.GetOrder(id));
                        o.ID = id;
                        Console.WriteLine("enter customer name:");
                        o.CustomerName = Console.ReadLine();
                        Console.WriteLine("enter customer email:");
                        o.CustomerEmail = Console.ReadLine();
                        Console.WriteLine("enter customer adress:");
                        o.CustomerAdress = Console.ReadLine();//check
                        Console.WriteLine("enter order date:");
                        o.OrderDate = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("enter ship date:");
                        o.ShipDate = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("enter delivery date:");
                        o.DeliveryDate = DateTime.Parse(Console.ReadLine());
                        dalOrder.UpdateOrder(o);
                        break;
                    }
                     case 4:
                    {
                        Console.WriteLine("enter order ID:");
                        id = int.Parse(Console.ReadLine());
                        dalOrder.DeleteOrder(id);
                        break;
                    }
                      default:
                     { 
                     Console.WriteLine("enter again");
                     action=int.Parse( Console.ReadLine());
                     break;
                     }  
                    }       
            }
           static void OrderItemCase()
           {
            int action,id;
            Console.WriteLine("enter 0 to add order item");
            Console.WriteLine("enter 1 to show order item by id");
            Console.WriteLine("enter 2 to view all order items");
            Console.WriteLine("enter 3 to update order item");
            Console.WriteLine("enter 4 to  delete order item");
            Console.WriteLine("enter 5 to  view all products in order");
            Console.WriteLine("enter 6 to  get order item by product and order");
            action = int.Parse( Console.ReadLine());
                        switch (action)
                        {
                            case 0:
                               {
                        OrderItem oI = new OrderItem();
                        oI.OrderItemID = DataSource.Config.LastOrderItem;
                        Console.WriteLine("enter product ID:");
                        oI.ProductID = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter order ID:");
                        oI.OrderID = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter product category:");
                        oI.Price = int.Parse(Console.ReadLine());//check
                        Console.WriteLine("enter product in stock:");
                        oI.Amount = int.Parse(Console.ReadLine());
                        dalOrderItem.AddOrderItem(oI);
                        break;
                    }   
                            case 1:
                    {
                        Console.WriteLine("enter order item ID:");
                        id = int.Parse(Console.ReadLine());
                        Console.WriteLine(dalOrderItem.GetOrderItem(id));
                        break;
                    }       
                                case 2:
                    {
                        foreach (OrderItem item in dalOrderItem.GetOrderItems())
                        {
                            Console.WriteLine(item);
                        }
                        
                        break;
                    }
                                case 3:
                    {
                        OrderItem oI = new OrderItem();
                        Console.WriteLine("enter product ID:");
                        oI.ProductID = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter order ID:");
                        oI.OrderID = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter product category:");
                        oI.Price = int.Parse(Console.ReadLine());//check
                        Console.WriteLine("enter product in stock:");
                        oI.Amount = int.Parse(Console.ReadLine());
                        dalOrderItem.UpdateOrderItem(oI);
                        break;
                    }
                                     
                                case 4:
                    {
                        Console.WriteLine("enter order item ID:");
                        id = int.Parse(Console.ReadLine());
                        dalOrderItem.DeleteOrderItem(id);
                        break;
                    }
                case 5:
                        Console.WriteLine("enter order item id:");
                        id = int.Parse(Console.ReadLine());
                        foreach (OrderItem item in dalOrderItem.GetProductsInOrder(id))
                        {
                        Console.WriteLine(item);
                        }
                    break;
                    }
            case 6:
                {
                int idP;
                Console.WriteLine("enter order id and product id:");
                id = int.Parse(Console.ReadLine());
                idP = int.Parse(Console.ReadLine());
                Console.WriteLine(dalOrderItem.GetOrderItemByIDS(idP,id));
            }
             default:
            { 
            Console.WriteLine("enter again");
            action=int.Parse( Console.ReadLine()); 
            break;
                }
                            
           }
            
                  

        static void Main()
        {
            
            do { 
            int entity;
            Console.WriteLine("enter 0 to exit");
            Console.WriteLine("enter 1 to products");
            Console.WriteLine("enter 2 to orders");
            Console.WriteLine("enter 3 to order items");
            entity=int.Parse( Console.ReadLine());
                    switch (entity)
                    {
                        case 1:
                            try
                            {
                                ProductCase();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                            break;
                        case 2:
                            try
                            {
                                OrderCase();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                            break;
                        case 3:
                            try
                            {
                                OrderItemCase();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                            break;

                        default:
                            {
                                Console.WriteLine("enter again");
                                entity = int.Parse(Console.ReadLine());
                                break;
                            }
                    }
                } while (entity != 0)
            }
       }
    }