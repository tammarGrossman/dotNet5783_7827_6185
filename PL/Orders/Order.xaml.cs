using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for Order.xaml
    /// </summary>
    public partial class Order : Window
    {

        BlApi.IBl? bl = BlApi.Factory.Get();
        public BO.Order order
        {
            get { return (BO.Order)GetValue(orderProperty); }
            set { SetValue(orderProperty, value); }
        }
        // Using a DependencyProperty as the backing store for order.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty orderProperty =
            DependencyProperty.Register("order", typeof(BO.Order), typeof(Window), new PropertyMetadata(null));

        /// <summary>
        /// to add product
        /// </summary>
        public Order()
        {
            InitializeComponent();
        }
        /// <summary>      
        /// to update product 
        /// </summary>
        /// <param name="id"></param>

        public Order(int id, bool watch)
        {
            InitializeComponent();
            try
            {
                if (watch)
                {
                  sentDateUpdateButton.Visibility= Visibility.Hidden;
                  recievedDateUpdateButton.Visibility = Visibility.Hidden;
                }
                else
                {
                    sentDateUpdateButton.Visibility = Visibility.Visible;
                    recievedDateUpdateButton.Visibility = Visibility.Visible;
                }
                var o = bl!.Order.Get(id);

                order = new BO.Order()
                {
                    ID = id,
                    CustomerAdress = o.CustomerAdress,
                    CustomerEmail = o.CustomerEmail,
                    CustomerName = o.CustomerName,
                    PaymentDate = o.PaymentDate,
                    ShipDate = o.ShipDate,
                    DeliveryDate = o.DeliveryDate,
                    Status = o.Status,
                    TotalPrice = o.TotalPrice,
                    Items = o.Items
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
            private void sentDateUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                order = bl!.Order.UpdateSend(order.ID);
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }
        private void recievedDateUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                order = bl!.Order.UpdateSupply(order.ID);
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }
    }
}
