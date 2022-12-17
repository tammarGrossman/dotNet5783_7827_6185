
using System;
using System.Windows;
using System.Windows.Controls;

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductList.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
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
        private void CategorySelector_SelectionChanged(object sender,SelectionChangedEventArgs e)
        {
          
            var cat = (BO.Category)((ComboBox)sender).SelectedItem;
            if (cat.ToString() != "None")
                ProductListView.ItemsSource = bl.Product.GetAll(x => x?.Category_ == cat);
            else
                ProductListView.ItemsSource = bl.Product.GetAll();
        }
        /// <summary>
        /// open window to add product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addNewProduct_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindow().ShowDialog();
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
                try
                {
                    BO.Product p = bl.Product.Get(id);
                    new ProductWindow(p).ShowDialog();
                    ProductListView.ItemsSource = bl.Product.GetAll();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }    
}