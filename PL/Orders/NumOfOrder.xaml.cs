using BO;
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

namespace PL.Orders
{
    /// <summary>
    /// Interaction logic for NumOfOrder.xaml
    /// </summary>
    public partial class NumOfOrder : Window
    {

        BlApi.IBl? bl = BlApi.Factory.Get();



        public int orderID
        {
            get { return (int)GetValue(orderIDProperty); }
            set { SetValue(orderIDProperty, value); }
        }

        // Using a DependencyProperty as the backing store for orderID.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty orderIDProperty =
            DependencyProperty.Register("orderID", typeof(int), typeof(NumOfOrder), new PropertyMetadata(0));


        public NumOfOrder()
        {
            InitializeComponent();
        }

        private void moveToTrackOrder_Click(object sender, RoutedEventArgs e)
        {
            //change which value
            if (Convert.ToString(orderID)!= "")
            {
                new TrackOrderWindow(orderID).Show();
            }
            else
            {
                MessageBox.Show("id detail is missing");
            }


        }
        

        
    }
}