using BlApi;
using BlImplementation;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductList.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        IBl bl = new Bl();
        /// <summary>
        /// show all products
        /// </summary>
        public ProductListWindow()
        {
            InitializeComponent();
            ProductListView.ItemsSource = bl.Product.GetAll();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }
        /// <summary>
        /// case of filter products by category
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CategorySelector_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
           
            var cat = (BO.Category)((ComboBox)sender).SelectedItem;
            ProductListView.ItemsSource = bl.Product.GetAll(x => x?.Category_ == cat);
        }
        /// <summary>
        /// open window to add product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addNewProduct_Click(object sender, RoutedEventArgs e)
        {
            new Product().ShowDialog();
            ProductListView.ItemsSource = bl.Product.GetAll();
        }
        /// <summary>
        /// open window to update product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lv = sender as ListView;
            BO.ProductForList pl = (BO.ProductForList)lv.SelectedItem;
            if (ProductListView.ItemsSource != null&& pl!=null) 
            {               
                int id = pl.ID;
                BO.Product p = bl.Product.Get(id);
                new Product(p).ShowDialog();
                ProductListView.ItemsSource = bl.Product.GetAll();
            }
        }
    }    
}

