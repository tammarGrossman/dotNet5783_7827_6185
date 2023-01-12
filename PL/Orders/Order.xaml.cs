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
        public Order(int id)
        {
            InitializeComponent();
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
      

       
       

        private void sentDateUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            order = bl!.Order.UpdateSend(order.ID);
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

        /// <summary>
        /// submit product to add or apdate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>

        // private void submitProduct_Click(object sender, RoutedEventArgs e)
        // {
        //    BO.Product p = new BO.Product();
        //    bool missing = false;
        //    //למלא פרטים מהטופס
        //    if (name.Text != "" && price.Text != "" && id.Text != "" && inStock.Text != "" && CategorySelector.SelectedItem.ToString() != "" && id.Text.Length >= 6)
        //    {
        //        p.ID = Convert.ToInt32(id.Text);
        //        p.Name = name.Text;
        //        p.Price = Convert.ToDouble(price.Text);
        //        p.Category_ = (BO.Category)Enum.Parse(typeof(BO.Category), (CategorySelector.SelectedItem).ToString());
        //        p.InStock = int.Parse(inStock.Text);
        //    }
        //    else
        //    {
        //        missing = true;
        //        MessageBox.Show("there are one or more missing details");
        //    }
        //    if (!missing)
        //    {
        //        if (p.Price > 0 && p.InStock > 0 && CategorySelector.SelectedItem.ToString() != "None")
        //        {
        //            if (add)
        //            {//add case
        //                try
        //                {
        //                    bl.Product.Add(p);
        //                    add = false;
        //                    MessageBox.Show("the product added succesfully");
        //                    this.Close();
        //                }
        //                catch (Exception ex)
        //                {
        //                    MessageBox.Show(ex.Message);
        //                }
        //            }
        //            else if (update)//update case
        //            {
        //                try
        //                {
        //                    bl.Product.Update(p);
        //                    update = false;
        //                    MessageBox.Show("the product updated succesfully");
        //                    this.Close();
        //                }
        //                catch (Exception ex)
        //                {
        //                    MessageBox.Show(ex.Message);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("one or more of your details of product is in correct");
        //        }
        //        missing = false;
        //    }
        //}
        //   }
    }
}
