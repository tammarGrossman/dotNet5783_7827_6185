﻿

using System;
using System.Windows;
namespace PL
{
    /// <summary>
    /// Interaction logic for Product.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        private static bool update = false;
        private static bool add = false;

        public BO.Product product
        {
            get { return (BO.Product)GetValue(productProperty); }
            set { SetValue(productProperty, value); }
        }
        // Using a DependencyProperty as the backing store for order.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty productProperty =
            DependencyProperty.Register("product", typeof(BO.Product), typeof(Window), new PropertyMetadata(null));


        /// <summary>
        /// to add product
        /// </summary>
        public ProductWindow()
        {
            product = new BO.Product();
            InitializeComponent();
            categorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
            add = true;
        }
        /// <summary>      
        /// to update product 
        /// </summary>
        /// <param name="id"></param>
        public ProductWindow(int id)
        {
            InitializeComponent();
            update = true;
            categorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
            try
            {
                product = bl!.Product.Get(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// submit product to add or apdate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        private void submitProduct_Click(object sender, RoutedEventArgs e)
        {
            
            if (add)
            {//add case
                try
                {
                    if (Convert.ToString(product.Category_ )!= ""&&Convert.ToString(product.Category_) != "None" && Convert.ToString(product.ID)!= ""&& product.ID != 0 && Convert.ToString(product.InStock) != "" && product.InStock != 0 && product.Name != "" && Convert.ToString(product.Price) != "" && product.Price !=0)
                    {
                        bl!.Product.Add(product);
                        add = false;
                        MessageBox.Show("the product added succesfully");

                        this.Close();
                    }

                    else
                        MessageBox.Show("there are one or more missing details");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (update)//update case
            {
                try
                {
                    if (Convert.ToString(product.Category_) != "" && Convert.ToString(product.Category_) != "None" && Convert.ToString(product.ID) != "" && product.ID != 0 && Convert.ToString(product.InStock) != "" && product.InStock != 0 && product.Name != "" && Convert.ToString(product.Price) != "" && product.Price != 0)
                    {
                        bl!.Product.Update(product);
                        update = false;
                        MessageBox.Show("the product updated succesfully");
                        this.Close();
                    }

                    else
                        MessageBox.Show("there are one or more missing details");
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
