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
    public Product()
    {
        InitializeComponent();
        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
    }
    public Product(BO.Product p)
    {
        InitializeComponent();
        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
        CategorySelector.SelectedItem = (p.Category_).ToString();
        id.Text =(p.ID).ToString();
        name.Text = p.Name;
        price.Text = (p.Price).ToString();
        inStock.Text = (p.InStock).ToString();
    }
    private void submitProduct_Click(object sender, RoutedEventArgs e)
    {
        BO.Product p = new BO.Product();
        //למלא פרטים מהטופס
        p.ID =Convert.ToInt32(id.Text);
        p.Name = name.Text;
        p.Price =Convert.ToDouble(price.Text);
        p.Category_ = (BO.Category)Enum.Parse(typeof(BO.Category),(CategorySelector.SelectedValue).ToString());//להמיר אינם
        p.InStock = int.Parse(inStock.Text);
            try
            {
                bl.Product.Add(p);
                MessageBox.Show("the product added succesfully");
                
            }
            catch (Exception ex)
            {
                //  MessageBox.Show(ex.Message);
                throw new Exception(ex.Message);
            }
            
            

    }
}
}
