﻿using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    sealed public class Dallist : IDal
    {
        public IOrder Order => new DalOrder();
        public IProduct Product => new DalProduct();
        public IOrderItem OrderItem => new DalOrderItem();

    }
}
