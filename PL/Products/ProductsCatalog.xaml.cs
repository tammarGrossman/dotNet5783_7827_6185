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
using System.Xml.Linq;

namespace PL.Products
{
    /// <summary>
    /// Interaction logic for ProductsCatalog.xaml
    /// </summary>
    public partial class ProductsCatalog : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public CustomerDetails custDetails
        {
            get { return (CustomerDetails)GetValue(custDetailsProperty); }
            set { SetValue(custDetailsProperty, value); }
        }

        public static readonly DependencyProperty custDetailsProperty =
            DependencyProperty.Register("custDetails", typeof(CustomerDetails), typeof(Window), new PropertyMetadata(null));


       
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
            categorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
            var help = bl!.Product.GetAll();
            products = help == null ? new() : new(help);
        }

        private void orderSubmit_Click(object sender, RoutedEventArgs e) 
            {
            List<BO.OrderItem> list = new List<BO.OrderItem>();

            new Carts.CartWindow(list).Show();
            }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lv = sender as ListView;
            if (productsCatalogList.ItemsSource != null )
            {
                try
                {
                    BO.Product p = bl!.Product.Get(((BO.ProductForList)lv!.SelectedItem).ID);                        
                    new productItemWindow(p.ID).ShowDialog();
                   // var help = bl!.Product.GetAll();
                    //products = help == null ? new() : new(help);
                    //categorySelector.SelectedItem = BO.Category.None;

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
                var help = bl!.Product.GetAll(x => x?.Category_ == cat);
                products = help == null ? new() : new(help);    
            }

            else
            {
                var help = bl!.Product.GetAll();
                products = help == null ? new() : new(help);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
