﻿using BlApi;
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

        public BO.OrderTracking? trackOrder
        {
            get { return (BO.OrderTracking?)GetValue(trackOrderProperty); }
            set { SetValue(trackOrderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for trackOrders.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty trackOrderProperty =
            DependencyProperty.Register("trackOrder", typeof(BO.OrderTracking), typeof(Window), new PropertyMetadata(null));


        public TrackOrderWindow()
        {
            InitializeComponent();
        }
        public TrackOrderWindow(int id)
        {
            InitializeComponent();
            try 
            {
                trackOrder = bl!.Order.TrackOrder(id);
            }

            catch (Exception ex) 
            {
                trackOrder = null; 
                MessageBox.Show(ex.Message,"invalid id");
            }
        }

        private void showOrderDetails_Click(object sender, RoutedEventArgs e) => new Order(trackOrder?.ID??0,true).Show();

        
    }
}
