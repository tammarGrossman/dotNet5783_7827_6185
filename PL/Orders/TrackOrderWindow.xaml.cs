using BlApi;
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
    /// Interaction logic for TrackOrderWindow.xaml
    /// </summary>
    public partial class TrackOrderWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();


        public ObservableCollection<Tuple<DateTime?, string?>> trackOrders
        {
            get { return (ObservableCollection<Tuple<DateTime?, string?>>)GetValue(trackOrdersProperty); }
            set { SetValue(trackOrdersProperty, value); }
        }

        // Using a DependencyProperty as the backing store for trackOrders.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty trackOrdersProperty =
            DependencyProperty.Register("trackOrders", typeof(ObservableCollection<Tuple<DateTime?, string?>>), typeof(Window), new PropertyMetadata(null));


        public TrackOrderWindow()
        {
            InitializeComponent();
            var help = bl!.Order.TrackOrder(Convert.ToInt32(id.Text)).Tracking;
            trackOrders = help == null ? new() : new(help);
        }
    }
}
