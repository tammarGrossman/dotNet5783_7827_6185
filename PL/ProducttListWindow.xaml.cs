using BlApi;
using BlImplementation;
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
        IBl bl = new Bl();
        public ProductListWindow()
        {
            InitializeComponent();
            ProductListView.ItemsSource = bl.Product.GetAll();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
            foreach (var item in ProductListView.ItemsSource)
            {
                var but = new Button();
                but.Name = "BtnUpdate";
                but.Content = "update";
                //if (item is Product)
                //    but.Click += new Product(((BO.Product)item) ?? null).Show();
            }
        }
        private void CategorySelector_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var cat = ((ComboBox)sender).SelectedValue;
            //ProductListView.ItemsSource = bl.Product.GetAll(x=>x.Category_==cat.ToString());
        }
        private void addNewProduct_Click(object sender, RoutedEventArgs e)
        {
            new Product().ShowDialog();
            ProductListView.ItemsSource = bl.Product.GetAll();
        }
        private void btnUpadate_Click(object sender, RoutedEventArgs e)
        {
            new Product();
        }
    }    
}
 