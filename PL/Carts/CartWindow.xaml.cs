using BO;
using PL.Products;
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

namespace PL.Carts
{
    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        public BO.Cart fullCart
        {
            get { return (BO.Cart)GetValue(fullCartProperty); }
            set { SetValue(fullCartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for fullCart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty fullCartProperty =
            DependencyProperty.Register("fullCart", typeof(BO.Cart), typeof(Window), new PropertyMetadata(null));


     
        public CartWindow()
        {
            InitializeComponent();
            fullCart = ProductsCatalog.c;
            fullCart.TotalPrice = fullCart.Items.Sum(x => x?.TotalPrice??0);
            ProductsCatalog.c.TotalPrice = fullCart.TotalPrice;
        }
        private void orderConfirmation_Click(object sender, RoutedEventArgs e)
        {
            bl!.Cart.OrderConfirmation(fullCart);
            MessageBox.Show("the cart confirm succesfuly");
            this.Close();
            new MainWindow().Show();    
        }
    }
}
