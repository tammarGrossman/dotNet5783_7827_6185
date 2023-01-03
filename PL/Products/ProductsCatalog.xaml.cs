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
        
    }
}
