// See https://aka.ms/new-console-template for more information
namespace stage0
{
   partial  class Program
    {
        static void Main(string[] args)
        {
            welcome6185();
           // welcome7827();
            Console.ReadKey();
        }
        /* static partial void welcome7827()
        {
            string name;
            Console.Write("Enter your name:");
            name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
        }*/
        private static void welcome6185()
        {
            string name;
            Console.Write("Enter your name:");
            name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
        }
    }
}