
using System;
using System.Collections.ObjectModel;
using System.Linq;
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

        public ObservableCollection<BO.ProductForList> products
        {
            get { return (ObservableCollection<BO.ProductForList>)GetValue(productsProperty); }
            set { SetValue(productsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for prods.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty productsProperty =
            DependencyProperty.Register("products", typeof(ObservableCollection<BO.ProductForList>), typeof(ProductListWindow), new PropertyMetadata(null));

        /// <summary>
        /// show all products
        /// </summary>
        public ProductListWindow()
        {
            InitializeComponent();
            var help = bl!.Product.GetAll();
            products = help == null ? new() : new(help);
           // ProductListView.ItemsSource = bl.Product.GetAll();
            categorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }
        /// <summary>
        /// case of filter products by category
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void categorySelector_SelectionChanged(object sender,SelectionChangedEventArgs e)
        {
          
            var cat = (BO.Category)((ComboBox)sender).SelectedItem;
            if (cat.ToString() != "None")
            {
                var help = bl!.Product.GetAll(x => x?.Category_ == cat);
                products = help == null ? new() : new(help!);
            }

            else
            {

                var help = bl!.Product.GetAll();
                products = help == null ? new() : new(help!);
            }
        }
        /// <summary>
        /// open window to add product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addNewProduct_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindow().ShowDialog();

            var help = bl!.Product.GetAll();
            products = help == null ? new() : new(help!);
            categorySelector.SelectedItem = BO.Category.None;

        }
        /// <summary>
        /// open window to update product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void productListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { 
            var lv = sender as ListView;
            BO.ProductForList pl = (BO.ProductForList)lv!.SelectedItem;
            if (productListView.ItemsSource != null&& pl!=null) 
            {               
                int id = pl.ID;
                try
                {
                    new ProductWindow(id).ShowDialog();
                    var help = bl!.Product.GetAll();
                    products = help == null ? new() : new(help!);
                    categorySelector.SelectedItem = BO.Category.None;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void groupingProducts_Click(object sender, RoutedEventArgs e)
        {
            var help = bl!.Product.GroupingProductsByCat();
            products = help == null ? new() : new(help);
            categorySelector.SelectedItem = BO.Category.None;
        }
    }    
}