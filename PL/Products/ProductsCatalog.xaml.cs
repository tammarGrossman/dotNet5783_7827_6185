using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ProductsCatalog.xaml
    /// </summary>
    public partial class ProductsCatalog : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public ObservableCollection<BO.ProductForList> products
        {
            get { return (ObservableCollection<BO.ProductForList>)GetValue(productsProperty); }
            set { SetValue(productsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for prods.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty productsProperty =
            DependencyProperty.Register("products", typeof(ObservableCollection<BO.ProductForList>), typeof(Window), new PropertyMetadata(null));

        public ProductsCatalog()
        {
            InitializeComponent();
            var help = bl!.Product.GetAll();
            products = help == null ? new() : new(help);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void orderSubmit_Click(object sender, RoutedEventArgs e) => new Carts.CartWindow().Show();

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lv = sender as ListView;
            if (productsCatalogList.ItemsSource != null )
            {
                try
                {
                            BO.Product p = bl!.Product.Get(((BO.ProductForList)lv!.SelectedItem).ID);
                            BO.ProductItem pi = new BO.ProductItem()
                            {
                                ID = p.ID,
                                Name = p.Name,
                                Price = p.Price,
                                Category_ = p.Category_,
                                InStock = p.InStock,
                                Amount = 0
                            };
                            new productItemWindow(pi).ShowDialog();

                          //  BO.Product p = bl!.order.Get(id);
                   // new ProductWindow(p).ShowDialog();
                    //ProductListView.ItemsSource = bl.Product.GetAll();
                    var help = bl!.Product.GetAll();
                    products = help == null ? new() : new(help);
                   // CategorySelector.SelectedItem = BO.Category.None;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
