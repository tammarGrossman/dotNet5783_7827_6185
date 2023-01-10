using BO;
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

        public ObservableCollection<BO.OrderItem> orderItems
        {
            get { return (ObservableCollection<BO.OrderItem>)GetValue(orderItemsProperty); }
            set { SetValue(orderItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for orderItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty orderItemsProperty =
            DependencyProperty.Register("orderItems", typeof(ObservableCollection<BO.OrderItem>), typeof(Window), new PropertyMetadata(null));


        public CartWindow()
        {
            InitializeComponent();
          


        }
        public CartWindow(List<OrderItem>cartList)
        {
            InitializeComponent();
            orderItems = cartList == null ? new() : new(cartList);
        }

        private void orderConfirmation_Click(object sender, RoutedEventArgs e)
        {
            BO.Order o = new BO.Order()
            {
             
            };

            MessageBox.Show("the cart confirm succesfuly");
        }
    }
}
