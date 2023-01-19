using PL.Products;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {



        public BO.Cart cart
        {
            get { return (BO.Cart)GetValue(cartProperty); }
            set { SetValue(cartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for cart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty cartProperty =
            DependencyProperty.Register("cart", typeof(BO.Cart), typeof(Window), new PropertyMetadata(null));


    

        public CustomerWindow()
        {
            InitializeComponent();
            cart = ProductsCatalog.c;
        }
        


        private void moveToCart_Click(object sender, RoutedEventArgs e)
        {
            if (cart.CustomerEmail != "" && cart.CustomerName != "" && cart.CustomerAdress != "")
            {
                MessageBox.Show($"{cart}");
                ProductsCatalog.c = cart;
                new Carts.CartWindow().Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("one or more details are missing");
            }
        }
    }
}
