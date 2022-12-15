using BlApi;
using BlImplementation;
using System;
using System.Windows;
namespace PL
{
    /// <summary>
    /// Interaction logic for Product.xaml
    /// </summary>
    public partial class Product : Window
    {
        IBl bl = new Bl();
        private static bool update=false;
        private static bool add = false;

        /// <summary>
        /// to add product
        /// </summary>
        public Product()
        {
            InitializeComponent();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
            add = true;
        }
        /// <summary>      
       /// to update product 
        /// </summary>
        /// <param name="p"></param>
        public Product(BO.Product p)
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
            //למלא פרטים מהטופס
            p.ID = Convert.ToInt32(id.Text);
            p.Name = name.Text;
            p.Price = Convert.ToDouble(price.Text);
            p.Category_ = (BO.Category)Enum.Parse(typeof(BO.Category), (CategorySelector.SelectedItem).ToString());
            p.InStock = int.Parse(inStock.Text);
            if (add)
            {//add case
                try
                {
                    bl.Product.Add(p);
                    MessageBox.Show("the product added succesfully");
                    this.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else if(update)//update case
            {
                try
                {
                    bl.Product.Update(p);
                    MessageBox.Show("the product updated succesfully");
                    this.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
