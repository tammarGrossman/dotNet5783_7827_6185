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

        public int OrderID
        {
            get { return (int)GetValue(OrderIDProperty); }
            set { SetValue(OrderIDProperty, value); }
        }
        // Using a DependencyProperty as the backing store for order.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrderIDProperty =
            DependencyProperty.Register("OrderID", typeof(int), typeof(Window), new PropertyMetadata(0));

        public NumOfOrder()
        {
            InitializeComponent();
            OrderID = 18;
        }

        private void moveToTrackOrder_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"{OrderID} jk");
            new TrackOrderWindow(OrderID).Show();
        }
        

        
    }
}