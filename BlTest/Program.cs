

using BlApi;
using BO;

namespace BlTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IBl bl = new BL;//check if correct
            IEnumerable<ProductForList?> productFL = bl.ProductForList;
        }
    }
}