﻿using BlApi;


namespace BlImplementation
{
    sealed public class Bl : IBl
    {
        /// <summary>
        /// create order product and cart interface
        /// </summary>
        public IOrder Order => new Order();
        public IProduct Product => new Product();
        public ICart Cart => new Cart();
       
    }
}
