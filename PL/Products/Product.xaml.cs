using BlApi;
using BlImplementation;
using DO;
using System;
using System.Windows;
namespace PL
{
    /// <summary>
    /// Interaction logic for Product.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        IBl bl = new Bl();
        private static bool update = false;
        private static bool add = false;

        /// <summary>
        /// to add product
        /// </summary>
        public ProductWindow()
        {
            InitializeComponent();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
            add = true;
        }
        /// <summary>      
        /// to update product 
        /// </summary>
        /// <param name="p"></param>
        public ProductWindow(BO.Product p)
        {
            InitializeComponent();
            update = true;
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
            CategorySelector.SelectedItem = p.Category_;
            id.Text = (p.ID).ToString();
            name.Text = p.Name;
            price.Text = (p.Price).ToString();
            inStock.Text = (p.InStock).ToString();
        }
        /// <summary>
        /// submit product to add or apdate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        private void submitProduct_Click(object sender, RoutedEventArgs e)
        {
            BO.Product p = new BO.Product();
            bool missing = false;
            //למלא פרטים מהטופס
            if (name.Text != "" && price.Text != "" && id.Text != "" && inStock.Text != "" && CategorySelector.SelectedItem.ToString() != "" &&  id.Text.Length >= 6)
            {
                p.ID = Convert.ToInt32(id.Text);
                p.Name = name.Text;
                p.Price = Convert.ToDouble(price.Text);
                p.Category_ = (BO.Category)Enum.Parse(typeof(BO.Category), (CategorySelector.SelectedItem).ToString());
                p.InStock = int.Parse(inStock.Text);
            }
            else
            {
                missing = true;
                MessageBox.Show("there are one or more missing details");
            }
            if (!missing)
            {
                if (p.Price > 0 && p.InStock > 0 && CategorySelector.SelectedItem.ToString()!="None")
                {
                    if (add)
                    {//add case
                        try
                        {
                            bl.Product.Add(p);
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
                            bl.Product.Update(p);
                            update = false;
                            MessageBox.Show("the product updated succesfully");
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("one or more of your details of product is in correct");
                }
                missing = false;
            }
        }
    }
}
