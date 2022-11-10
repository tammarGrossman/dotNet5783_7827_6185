namespace DalTest
{
    public class DalTest
    { 
       private DalOrder dalOrder=new DalOrder();
       private   DalOrderItem dalOrderItem=new DalOrderItem();
       private   DalProduct dalProduct=new DalProduct();
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
                                    try()
                                break;
                            case 1:
                                      break;
                                case 2:
                                     break;
                                case 3:
                                       break;

                                case 4:
                                       break;
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
                                break;
                            case 1:
                                      break;
                                case 2:
                                     break;
                                case 3:
                                       break;

                                case 4:
                                       break;
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
                                break;
                            case 1:
                                      break;
                                case 2:
                                     break;
                                case 3:
                                       break;

                                case 4:
                                       break;
                                 default:
                                { 
                                Console.WriteLine("enter again");
                                action=int.Parse( Console.ReadLine());
                                    
                                break;
                                    }
                            
                            }
            }
                  

        static void Main()
        {
            int entity;
            Console.WriteLine("enter 0 to exit");
            Console.WriteLine("enter 1 to products");
            Console.WriteLine("enter 2 to orders");
            Console.WriteLine("enter 3 to order items");
           entity=int.Parse( Console.ReadLine());
            switch (entity)
            {
                case 0:
                    break;
           case 1:
                    try { 
         ProductCase();
                        }
                    catch(Exception ex)
                    {
                       Console.WriteLine(ex);
                    }
                    break;          
            case 2:
                    try { 
                    OrderCase();
                        }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break  ;                  
          case 3:
                    try { 
                 OrderItemCase();
                        }
                    catch(Exception ex)
                    {
                       Console.WriteLine(ex);
                    }
                    break;
            
                default: { 
                         Console.WriteLine("enter again");
                         entity=int.Parse( Console.ReadLine());                                   
                         break;
                    }
            }

       }


    }
    }