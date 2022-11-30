
using BlApi;
using BlImplementation;
using BO;
using Dal;
using DalApi;
namespace BlTest
{
    internal class Program
    {
        static IBl bl = new Bl();
        static IDal dal = new Dallist();
        /// <summary>
        /// the product case
        /// </summary>
        static void ProductCase()
        {
            int action;
            bool res;
            //enter action
            Console.WriteLine("enter 1 to add product");
            Console.WriteLine("enter 2 to show product by id");
            Console.WriteLine("enter 3 to show product in cart by id and cart");
            Console.WriteLine("enter 4 to view all products");
            Console.WriteLine("enter 5 to update product");
            Console.WriteLine("enter 6 to  delete product");
            res = int.TryParse(Console.ReadLine(), out action);
            switch (action)
            {
                case 1://add product
                    {
                        Product p = new Product();
                        Console.WriteLine("enter product ID:");
                        p.ID = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter product name:");
                        p.Name = Console.ReadLine();
                        Console.WriteLine("enter product price:");
                        p.Price = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter product category:");
                        p.Category_ = (Category)Enum.Parse(typeof(Category), Console.ReadLine());
                        Console.WriteLine("enter product in stock:");
                        p.InStock = int.Parse(Console.ReadLine());
                        bl.Product.Add(p);
                    }
                    break;
                case 2://get product
                    {
                        int id;
                        Console.WriteLine("enter product ID:");
                        res = int.TryParse(Console.ReadLine(), out id);
                        Console.WriteLine(bl.Product.Get(id));
                    }
                    break;
                case 3://get product in cart by id
                    {
                        int id,cartID;
                        Cart c = new Cart();//check
                        Console.WriteLine("enter product ID:");
                        res = int.TryParse(Console.ReadLine(), out id);
                        Console.WriteLine("enter cart ID:");
                        res = int.TryParse(Console.ReadLine(), out cartID);
                        Console.WriteLine(bl.Product.Get(id,c));
                    }
                    break;
                case 4://get all product
                    {
                        foreach (var item in bl.Product.GetAll())
                        {
                            Console.WriteLine(item);
                        }
                    }
                    break;
                case 5://update product
                    {
                        int id;
                        Console.WriteLine("enter product ID:");
                        res = int.TryParse(Console.ReadLine(), out id);
                        Console.WriteLine(dal.Product.Get(id));
                        Product p = new Product();
                        Console.WriteLine("enter product ID:");
                        p.ID = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter product name:");
                        p.Name = Console.ReadLine();
                        Console.WriteLine("enter product price:");
                        p.Price = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter product category:");
                        p.Category_ = (Category)Enum.Parse(typeof(Category), Console.ReadLine());
                        Console.WriteLine("enter product in stock:");
                        p.InStock = int.Parse(Console.ReadLine());
                        bl.Product.Update(p);
                        break;
                    }
                case 6://delete product
                    {
                        int id;
                        Console.WriteLine("enter product ID:");
                        res = int.TryParse(Console.ReadLine(), out id);
                        bl.Product.Delete(id);
                    }
                    break;

                default://press again product
                    {
                        Console.WriteLine("enter again");
                        res = int.TryParse(Console.ReadLine(), out action);
                    }
                    break;

            }
        }
        static void OrderCase()
        {
            DateTime deliveryD, orderD, shipD;
            int action, id;
            bool res;
            Console.WriteLine("enter 1 to get  order by id");
            Console.WriteLine("enter 2 to get all orders");
            Console.WriteLine("enter 3 to update send date of order");
            Console.WriteLine("enter 4 to update supply date of order");
            Console.WriteLine("enter 5 to track order");
            res = int.TryParse(Console.ReadLine(), out action);
            switch (action)
            {           
                case 1://get order
                    {
                        Console.WriteLine("enter order ID:");
                        res = int.TryParse(Console.ReadLine(), out id);
                        Console.WriteLine(bl.Order.Get(id));
                    }
                    break;

                case 2:  //get all order
                    {
                        foreach (var item in bl.Order.GetAll())
                        {
                            Console.WriteLine(item);
                        }
                    }
                    break;
                case 3://update send date of order 
                    {
                        Console.WriteLine("enter order ID:");
                        res = int.TryParse(Console.ReadLine(), out id);                       
                        bl.Order.UpdateSend(id);
                    }
                    break;

                case 4:// update supply date of order
                    {
                        Console.WriteLine("enter order ID:");
                        res = int.TryParse(Console.ReadLine(), out id);
                        bl.Order.UpdateSupply(id);
                    }
                    break;
                case 5://track order
                    {
                        Console.WriteLine("enter order ID:");
                        res = int.TryParse(Console.ReadLine(), out id);
                        Console.WriteLine(bl.Order.TrackOrder(id));
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
        static void CartCase(Cart c)
        {
            int action;
            bool res;
            //enter action
            Console.WriteLine("enter 1 to add product to cart");
            Console.WriteLine("enter 2 to update product in cart");
            Console.WriteLine("enter 3 to confirm order");
            res = int.TryParse(Console.ReadLine(), out action);
            switch (action)
            {
                case 1://add product to cart
                    {
                        int id;
                        Console.WriteLine("enter product ID:");
                        res = int.TryParse(Console.ReadLine(), out id);
                        bl.Cart.Add(c,id);
                    }
                    break;
                case 2://update product in cart
                    {
                        int amount,id;
                        Console.WriteLine("enter product ID:");
                        res = int.TryParse(Console.ReadLine(), out id);
                        Console.WriteLine("enter product amount:");
                        res = int.TryParse(Console.ReadLine(), out amount);
                        bl.Cart.Update(c,id,amount);
                        break;
                    }
                case 3://confirm order
                    {
                        bl.Cart.OrderConfirmation(c);
                    }
                    break;

                default://press again product
                    {
                        Console.WriteLine("enter again");
                        res = int.TryParse(Console.ReadLine(), out action);
                    }
                    break;

            }
        }
        static void Main(string[] args)
        {
            Cart c = new Cart();
            int entity;
            bool res;
            Console.WriteLine("enter 0 to exit");
            Console.WriteLine("enter 1 to products");
            Console.WriteLine("enter 2 to orders");
            Console.WriteLine("enter 3 to cart");
            res = int.TryParse(Console.ReadLine(), out entity);
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
                    case 3://cart case
                        {
                            try
                            {
                                Console.WriteLine("enter customer name:");
                                c.CustomerName = Console.ReadLine();
                                Console.WriteLine("enter customer adress:");
                                c.CustomerAdress= Console.ReadLine();
                                Console.WriteLine("enter customer email:");
                                c.CustomerEmail= Console.ReadLine();
                                if (Validation.NameAdress(c.CustomerName))

                                    CartCase(c);
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
                            res = int.TryParse(Console.ReadLine(), out entity);
                        }
                        break;

                }
                //enter entity again
                Console.WriteLine("enter 0 to exit");
                Console.WriteLine("enter 1 to products");
                Console.WriteLine("enter 2 to orders");
                Console.WriteLine("enter 3 to cart");
                res = int.TryParse(Console.ReadLine(), out entity);
            }
        
    }
    }
}