using BlApi;
using BlImplementation;
using System;
using System.Collections.Generic;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        IBl bl = new Bl();

        public AddProductWindow()
        {
            InitializeComponent();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }

        private void submitProduct_Click(object sender, RoutedEventArgs e)
        {
            BO.Product p=new BO.Product();
            //למלא פרטים מהטופס
            p.ID = this.id.Text;
            p.Name= this.name.Text;
            p.Price = this.price.Text;
            p.Category_ = (this.CategorySelector.SelectedValue);//להמיר אינם
            p.InStock = int.Parse(this.inStock.Text);
            bl.Product.Add(p);
        }
    }
}
