
using PL.Orders;
using PL.Products;
using System.Windows;


namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// open the main window
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        private void managerWindowBut_Click(object sender, RoutedEventArgs e) => new ManagerWindow().Show();

        private void trackOrderWindow_Click(object sender, RoutedEventArgs e)=> new NumOfOrder().Show();

        private void orderWindowBut_Click(object sender, RoutedEventArgs e) => new ProductsCatalog().Show();

        private void startProgress_Click(object sender, RoutedEventArgs e)=> new Simulator().Show();
    }
}