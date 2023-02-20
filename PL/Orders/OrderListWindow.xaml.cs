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

namespace PL.Orders
{
    /// <summary>
    /// Interaction logic for OrderListWindow.xaml
    /// </summary>
    public partial class OrderListWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();


        public ObservableCollection<BO.OrderForList?> orders
        {
            get { return (ObservableCollection<BO.OrderForList?>)GetValue(ordersProperty); }
            set { SetValue(ordersProperty, value); }
        }

        // Using a DependencyProperty as the backing store for orders.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ordersProperty =
            DependencyProperty.Register("orders", typeof(ObservableCollection<BO.OrderForList>), typeof(OrderListWindow), new PropertyMetadata(null));


        public OrderListWindow()
        {
            InitializeComponent();
            var help = bl!.Order.GetAll();
            orders = help == null ? new() : new(help);
        }

        private void orderListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lv = sender as ListView;
            if (orderListView.ItemsSource != null)
            {
                try
                {
                    new Order(((BO.OrderForList)lv!.SelectedItem).ID,false).ShowDialog();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
           
        }
    }
}
