using BlApi;
using BlImplementation;
using System.Windows;
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
        }
        public ProductListWindow(Product p)
        {
            InitializeComponent();
            ProductListView.ItmesSource = bl.Product.GetAll();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }
        private void addNewProduct_Click(object sender, RoutedEventArgs e) => new AddProductWindow().Show();

    }
}
 