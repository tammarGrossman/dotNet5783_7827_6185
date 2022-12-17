using DalApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    sealed internal class Dallist : IDal
    {
        public static IDal Instance { get; } = new Dallist();
        private Dallist() { }
            
        /// <summary>
        /// the order interface
        /// </summary>
        public IOrder Order => new DalOrder();
        /// <summary>
        /// the product interface
        /// </summary>
        public IProduct Product => new DalProduct();
        /// <summary>
        /// the order item interface
        /// </summary>
        public IOrderItem OrderItem => new DalOrderItem();

    }
}
