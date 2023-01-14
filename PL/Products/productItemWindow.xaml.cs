﻿using BO;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Products
{
    /// <summary>
    /// Interaction logic for productItemWindow.xaml
    /// </summary>
    public partial class productItemWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
       
        public BO.ProductItem productItem
        {
            get { return (BO.ProductItem)GetValue(productProperty); }
            set { SetValue(productProperty, value); }
        }
        // Using a DependencyProperty as the backing store for order.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty productProperty =
            DependencyProperty.Register("productItem", typeof(BO.ProductItem), typeof(Window), new PropertyMetadata(null));



        public productItemWindow()
        {
            InitializeComponent();
        }
        public productItemWindow(int id)
        {
            InitializeComponent();
            try
            {
                BO.Product p = bl!.Product.Get(id);
                productItem = new BO.ProductItem()
                {
                    ID = p.ID,
                    Name = p.Name,
                    Price = p.Price,
                    Category_ = p.Category_,
                    InStock = p.InStock > 0 ? true : false,
                    Amount = p.InStock
                };
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
