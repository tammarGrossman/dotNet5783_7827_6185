﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PL.Products
{
    /// <summary>
    /// Interaction logic for ProductsCatalog.xaml
    /// </summary>
    public partial class ProductsCatalog : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        public static BO.Cart c = new BO.Cart();


        public ObservableCollection<BO.ProductItem> products
        {
            get { return (ObservableCollection<BO.ProductItem>)GetValue(productsProperty); }
            set { SetValue(productsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for products.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty productsProperty =
            DependencyProperty.Register("products", typeof(ObservableCollection<BO.ProductItem>), typeof(Window), new PropertyMetadata(null));

        public ProductsCatalog()
        {
            InitializeComponent();
            var help = bl!.Product.GetAllPI();
            products = help == null ? new() : new(help!);
            categorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }

        private void orderSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (c.Items.Count() == 0)
                MessageBox.Show("there is no items in the cart");
            else
            {
                new Carts.CustomerWindow().Show();
                this.Close();
            }
        }

        private void productsCatalogList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lv = sender as ListView;
            if (products != null)
            {
                try
                {
                    BO.Product p = bl!.Product.Get(((BO.ProductForList)lv!.SelectedItem).ID);
                    new productItemWindow(p.ID).ShowDialog();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void categorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cat = (BO.Category)((ComboBox)sender).SelectedItem;
            if (cat.ToString() != "None")
            {
                var help = bl!.Product.GetAllPI(x => x?.Category_ == cat);
                products = help == null ? new() : new(help!);
            }

            else
            {
                var help = bl!.Product.GetAllPI();
                products = help == null ? new() : new(help!);
            }
        }

        private void addProductToCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = ((BO.ProductItem)((FrameworkElement)sender).DataContext).ID;
                int index = c.Items.FindIndex(x => x?.ProductID== id);
                if (index != -1)//found
                    c = bl!.Cart.Update(c, id, 1);

                else
                    c = bl!.Cart.Add(c, id);

                MessageBox.Show("the product added sucsessfuly to the cart ");
            
            }

            catch (BO.Exceptions.NotExist ex)
            {
                MessageBox.Show(ex.Message);
            }

            catch (BO.Exceptions.NotLegal ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void deleteProductFromCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = ((BO.ProductItem)((FrameworkElement)sender).DataContext).ID;
                int index = c.Items.FindIndex(x => x?.ProductID == id);
                c = bl!.Cart.Update(c, id, -1);
                MessageBox.Show("the product decreased sucsessfuly from the cart ");
            }

            catch (BO.Exceptions.NotExist ex)
            {
                MessageBox.Show(ex.Message);
            }

            catch (BO.Exceptions.NotLegal ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

    }

}
