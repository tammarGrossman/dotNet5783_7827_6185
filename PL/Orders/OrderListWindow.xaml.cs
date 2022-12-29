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
        private ObservableCollection<BO.OrderForList> orders = new ObservableCollection<BO.OrderForList>();

        public OrderListWindow()
        {
            InitializeComponent();
           // return BO.OrderForList from bl.Orders.gGetAll()
            DataContext =orders;
        }

    }
}
