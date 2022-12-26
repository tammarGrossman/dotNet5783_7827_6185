

using DO;
namespace DalTest
{
    public class Program
    {
        static DalApi.IDal? dal = DalApi.Factory.Get();
        /// <summary>
        /// the product case
        /// </summary>
        static void ProductCase()
        {
            int action;
            bool res;
            //enter action
            Console.WriteLine("enter 0 to add product");
            Console.WriteLine("enter 1 to show product by id");
            Console.WriteLine("enter 2 to view all products");
            Console.WriteLine("enter 3 to update product");
            Console.WriteLine("enter 4 to  delete product");
            res = int.TryParse(Console.ReadLine(), out action);

            switch (action)
            {
                case 0://add product
                    {
                        Product p = new Product();
                        Console.WriteLine("enter product ID:");                    
                        p.ID = int.Parse(Console.ReadLine() ?? throw new DO.MissingInputValue("id"));
                        Console.WriteLine("enter product name:");
                        p.Name = Console.ReadLine() ?? throw new DO.MissingInputValue("name");
                        Console.WriteLine("enter product price:");
                        p.Price = int.Parse(Console.ReadLine() ?? throw new DO.MissingInputValue("price"));
                        Console.WriteLine("enter product category:");
                        p.Category_ = (Category)Enum.Parse(typeof(Category), Console.ReadLine() ?? throw new DO.MissingInputValue("category"));
                        Console.WriteLine("enter product in stock:");
                        p.InStock = int.Parse(Console.ReadLine() ?? throw new DO.MissingInputValue("in stock"));
                        dal?.Product.Add(p);
                    }
                    break;
                case 1://get product
                    {
                        int id;
                        Console.WriteLine("enter product ID:");
                        res = int.TryParse(Console.ReadLine(), out id);
                        Console.WriteLine(dal?.Product.Get(id));
                    }
                    break;
                case 2://get all product
                    {
                        foreach (var item in dal?.Product.GetAll() ?? throw new DO.DBConnectionFailed("")  )
                        {
                            Console.WriteLine(item);
                        }
                    }
                    break;

                case 3://update product
                    {
                        int id;
                        Console.WriteLine("enter product ID:");
                        res = int.TryParse(Console.ReadLine(), out id);
                        Console.WriteLine(dal?.Product.Get(id));
                        Product p = new Product();
                        Console.WriteLine("enter product ID:");
                        p.ID = int.Parse(Console.ReadLine() ?? throw new DO.MissingInputValue("id"));
                        Console.WriteLine("enter product name:");
                        p.Name = Console.ReadLine() ?? throw new DO.MissingInputValue("name");
                        Console.WriteLine("enter product price:");
                        p.Price = int.Parse(Console.ReadLine() ?? throw new DO.MissingInputValue("price"));
                        Console.WriteLine("enter product category:");
                        p.Category_ = (Category)Enum.Parse(typeof(Category), Console.ReadLine() ?? throw new DO.MissingInputValue("category"));
                        Console.WriteLine("enter product in stock:");
                        p.InStock = int.Parse(Console.ReadLine() ?? throw new DO.MissingInputValue("in stock"));
                        dal?.Product.Update(p);
                        break;
                    }
                case 4://delete product
                    {
                        int id;
                        Console.WriteLine("enter product ID:");
                        res = int.TryParse(Console.ReadLine(),out id);
                        dal?.Product.Delete(id);
                    }
                    break;

                default://press again product
                    {
                        Console.WriteLine("enter again");
                        res = int.TryParse(Console.ReadLine(),out action);
                    }
                    break;

            }
        }
        /// <summary>
        /// the order case
        /// </summary>
        static void OrderCase()
        {
            DateTime deliveryD, orderD, shipD;
            int action, id;
            bool res;
            Console.WriteLine("enter 0 to add order");
            Console.WriteLine("enter 1 to show order by id");
            Console.WriteLine("enter 2 to view all orders");
            Console.WriteLine("enter 3 to update order");
            Console.WriteLine("enter 4 to  delete order");
            res = int.TryParse(Console.ReadLine(),out action);

            switch (action)
            {
                case 0://add order
                    {
                        Order o = new Order();
                        Console.WriteLine("enter customer name:");
                        o.CustomerName = Console.ReadLine();
                        Console.WriteLine("enter customer email:");
                        o.CustomerEmail = Console.ReadLine();
                        Console.WriteLine("enter customer adress:");
                        o.CustomerAdress = Console.ReadLine();
                        Console.WriteLine("enter order date:");
                        res = DateTime.TryParse(Console.ReadLine(),out orderD);
                        Console.WriteLine("enter ship date:");                      
                        res = DateTime.TryParse(Console.ReadLine(),out shipD);
                        Console.WriteLine("enter delivery date:");              
                        res = DateTime.TryParse(Console.ReadLine(),out deliveryD);
                        o.OrderDate = orderD;
                        o.ShipDate= shipD;
                        o.DeliveryDate= deliveryD;
                        dal?.Order.Add(o);
                    }
                    break;
                case 1://get order
                    {
                        Console.WriteLine("enter order ID:");
                        res = int.TryParse(Console.ReadLine(),out id);
                        Console.WriteLine(dal?.Order.Get(id));
                    }
                    break;
                  
                case 2:  //get all order
                    {
                        foreach (Order? item in dal?.Order.GetAll()?? throw new DO.DBConnectionFailed(""))
                        {
                            Console.WriteLine(item);
                        }
                    }
                    break;
                case 3://update order
                    {
                        Order o = new Order();
                        Console.WriteLine("enter order ID:");
                        res = int.TryParse(Console.ReadLine(), out id);
                        Console.WriteLine(dal?.Order.Get(id));
                        o.ID = id;
                        Console.WriteLine("enter customer name:");
                        o.CustomerName = Console.ReadLine();
                        Console.WriteLine("enter customer email:");
                        o.CustomerEmail = Console.ReadLine();
                        Console.WriteLine("enter customer adress:");
                        o.CustomerAdress = Console.ReadLine();
                        Console.WriteLine("enter order date:");
                        res = DateTime.TryParse(Console.ReadLine(), out orderD);
                        Console.WriteLine("enter ship date:");
                        res = DateTime.TryParse(Console.ReadLine(), out shipD);
                        Console.WriteLine("enter delivery date:");
                        res = DateTime.TryParse(Console.ReadLine(), out deliveryD);
                        o.OrderDate = orderD;
                        o.ShipDate = shipD;
                        o.DeliveryDate = deliveryD;
                        dal?.Order.Update(o);
                    }
                    break;

                case 4://delete order
                    {
                        Console.WriteLine("enter order ID:");
                        res = int.TryParse(Console.ReadLine(),out id);
                        dal?.Order.Delete(id);
                    }
                    break;
                default://press again order
                    {
                        Console.WriteLine("enter again");
                        res = int.TryParse(Console.ReadLine(), out action);
                    }
                    break;
            }
        }
        /// <summary>
        /// orderItem case
        /// </summary>
        static void OrderItemCase()
        {
            int action, id;
            bool res;
            Console.WriteLine("enter 0 to add order item");
            Console.WriteLine("enter 1 to show order item by id");
            Console.WriteLine("enter 2 to view all order items");
            Console.WriteLine("enter 3 to update order item");
            Console.WriteLine("enter 4 to  delete order item");
            Console.WriteLine("enter 5 to  view all products in order");
            Console.WriteLine("enter 6 to  get order item by product and order");
            res = int.TryParse(Console.ReadLine(),out action);

            switch (action)
            {
                case 0://add order item
                    {
                        OrderItem oI = new OrderItem();
                        Console.WriteLine("enter product ID:");
                        oI.ProductID = int.Parse(Console.ReadLine() ?? throw new DO.MissingInputValue(" product id"));
                        Console.WriteLine("enter order ID:");
                        oI.OrderID = int.Parse(Console.ReadLine() ?? throw new DO.MissingInputValue("order id"));
                        Console.WriteLine("enter product category:");
                        oI.Price = int.Parse(Console.ReadLine() ?? throw new DO.MissingInputValue("price"));
                        Console.WriteLine("enter product amount:");
                        oI.Amount = int.Parse(Console.ReadLine() ?? throw new DO.MissingInputValue("amount"));
                        dal?.OrderItem.Add(oI);
                    }
                    break;
                case 1://get order item
                    {
                        Console.WriteLine("enter order item ID:");
                        res = int.TryParse(Console.ReadLine(), out id);
                        Console.WriteLine(dal?.OrderItem.Get(id));
                    }
                    break;
                case 2://get all order item
                    {
                        foreach (var item in dal?.OrderItem.GetAll() ?? throw new DO.DBConnectionFailed(""))
                        {
                            Console.WriteLine(item);
                        }
                        
                    }
                    break;
                case 3://update order item
                    {
                        OrderItem oI = new OrderItem();
                        Console.WriteLine("enter order item ID:");
                        res = int.TryParse(Console.ReadLine(), out id);
                        Console.WriteLine(dal?.OrderItem.Get(id));
                        oI.OrderItemID = id;
                        Console.WriteLine("enter product ID:");
                        oI.ProductID = int.Parse(Console.ReadLine() ?? throw new DO.MissingInputValue("product id"));
                        Console.WriteLine("enter order ID:");
                        oI.OrderID = int.Parse(Console.ReadLine() ?? throw new DO.MissingInputValue("order id"));
                        Console.WriteLine("enter product category:");
                        oI.Price = int.Parse(Console.ReadLine() ?? throw new DO.MissingInputValue("price"));
                        Console.WriteLine("enter product amount:");
                        oI.Amount = int.Parse(Console.ReadLine() ?? throw new DO.MissingInputValue("amount"));
                        dal?.OrderItem.Update(oI);                      
                    }
                    break;
                case 4://delete order item
                    {
                        Console.WriteLine("enter order item ID:");
                        res = int.TryParse(Console.ReadLine(),out id);
                        dal?.OrderItem.Delete(id);                       
                    }
                    break;
                case 5://products in order 
                    {
                        Console.WriteLine("enter order id:");
                        res = int.TryParse(Console.ReadLine(),out id);
                        foreach (OrderItem? item in dal?.OrderItem.GetProductsInOrder(id) ?? throw new DO.DBConnectionFailed(""))
                        {
                            if (item?.OrderItemID != 0)
                                Console.WriteLine(item);
                        }
                       
                    }
                    break;
                case 6://order item by product and order ids
                    {
                        int idP;
                        Console.WriteLine("enter order id:");
                        res = int.TryParse(Console.ReadLine(),out id);
                        Console.WriteLine("enter product id:");
                        res = int.TryParse(Console.ReadLine(),out idP);
                        Console.WriteLine(dal?.OrderItem.GetOrderItemByIDS(idP, id));
                    }
                    break;
                default://press again order item
                    {
                        Console.WriteLine("enter again");
                        res = int.TryParse(Console.ReadLine(), out action);
                       
                    }
                    break;
            }
        }
        static void Main()
        {
            int entity;
            bool res;
            Console.WriteLine("enter 0 to exit");
            Console.WriteLine("enter 1 to products");
            Console.WriteLine("enter 2 to orders");
            Console.WriteLine("enter 3 to order items");
            res = int.TryParse(Console.ReadLine(),out entity);
            while (entity != 0)//enter entity
            {
                switch (entity)
                {
                    case 1://product case
                        {
                            try
                            {
                                ProductCase();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                        }
                        break;
                    case 2://order case
                        {
                            try
                            {
                                OrderCase();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                        }
                        break;
                    case 3://order item case
                        {
                            try
                            {
                                OrderItemCase();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                        }
                        break;

                    default:
                        {
                            Console.WriteLine("enter again");
                            res = int.TryParse(Console.ReadLine(),out  entity);
                        }
                        break;

                }
                //enter entity again
                Console.WriteLine("enter 0 to exit");
                Console.WriteLine("enter 1 to products");
                Console.WriteLine("enter 2 to orders");
                Console.WriteLine("enter 3 to order items");
                res = int.TryParse(Console.ReadLine(),out entity);
            }
        }
    }
}