

using System;
using System.Windows;
namespace PL
{
    /// <summary>
    /// Interaction logic for Product.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        private static bool update = false;
        private static bool add = false;

        public BO.Product product
        {
            get { return (BO.Product)GetValue(productProperty); }
            set { SetValue(productProperty, value); }
        }
        // Using a DependencyProperty as the backing store for order.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty productProperty =
            DependencyProperty.Register("product", typeof(BO.Product), typeof(Window), new PropertyMetadata(null));

     
    /// <summary>
    /// to add product
    /// </summary>
    public ProductWindow()
        {
            product = new BO.Product();
            InitializeComponent();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
            add = true;
        }
        /// <summary>      
        /// to update product 
        /// </summary>
        /// <param name="id"></param>
        public ProductWindow(int id)
        {
            InitializeComponent();
            update = true;
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
            product = bl!.Product.Get(id); 
        }

        /// <summary>
        /// submit product to add or apdate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        private void submitProduct_Click(object sender, RoutedEventArgs e)
        {
           // BO.Product p = new BO.Product();
            //bool missing = false;
            //למלא פרטים מהטופס
            //if (name.Text != "" && price.Text != "" && id.Text != "" && inStock.Text != "" && CategorySelector.SelectedItem.ToString() != "" &&  id.Text.Length >= 6)
            //{
            //    p.ID = Convert.ToInt32(id.Text);
            //    p.Name = name.Text;
            //    p.Price = Convert.ToDouble(price.Text);
            //    p.Category_ = (BO.Category)Enum.Parse(typeof(BO.Category), (CategorySelector.SelectedItem).ToString());
            //    p.InStock = int.Parse(inStock.Text);
            //}
            //else
            //{
            //    missing = true;
            //    MessageBox.Show("there are one or more missing details");
            //}
            //if (!missing)
            //{
            //    if (p.Price > 0 && p.InStock > 0 && CategorySelector.SelectedItem.ToString()!="None")
            //    {
                    if (add)
                    {//add case
                        try
                        {
                            MessageBox.Show($"{product}");
                            bl!.Product.Add(product);
                            add = false;
                            MessageBox.Show("the product added succesfully");
                            
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else if (update)//update case
                    {
                        try
                        {
                            bl!.Product.Update(product);
                            update = false;
                            MessageBox.Show("the product updated succesfully");
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
               // }
            //    else
            //    {
            //        MessageBox.Show("one or more of your details of product is in correct");
            //    }
            //    missing = false;
            //}
        }
    }
}
