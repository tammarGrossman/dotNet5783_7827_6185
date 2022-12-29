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

namespace PL.Products
{
    /// <summary>
    /// Interaction logic for ProductsCatalog.xaml
    /// </summary>
    public partial class ProductsCatalog : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        private ObservableCollection<BO.ProductItem> productItems=new ObservableCollection<BO.ProductItem>();
        public ProductsCatalog()
        {
            InitializeComponent();
            DataContext = productItems;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
