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
        id = (p.ID).ToString();
        name = p.Name;
        price = (p.Price).ToString();
        inStock = (p.InStock).ToString();
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
        bl.Product.Add(p);
    }
}
}
