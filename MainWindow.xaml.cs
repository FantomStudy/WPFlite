using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFlite.Data;

namespace WPFlite
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MyDbContext dbContext;
        Product newProduct = new Product();
        Product selectedProduct = new Product();
        public MainWindow(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
            InitializeComponent();
            GetProducts();
            DGAdd.DataContext = newProduct;
        }
        private void GetProducts()
        {
            DGProduct.ItemsSource = dbContext.Products.ToList();
        }

        private void Btn_Add_Click(object sender, RoutedEventArgs e)
        {
            dbContext.Products.Add(newProduct);
            dbContext.SaveChanges();
            GetProducts();
            newProduct = new Product();
            DGAdd.DataContext = newProduct;
        }
        
        private void UpdateClickForEdit(object sender, RoutedEventArgs e)
        {
            selectedProduct = (sender as FrameworkElement).DataContext as Product;
            DGUpdate.DataContext = selectedProduct;
        }

        private void Btn_Update_Click(object sender, RoutedEventArgs e)
        {
            dbContext.Update(selectedProduct);
            dbContext.SaveChanges();
            GetProducts();
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            var productForDelete = (sender as FrameworkElement).DataContext as Product;
            dbContext.Products.Remove(productForDelete);
            dbContext.SaveChanges();
            GetProducts();
        }

        private void ExtendBtn_Click(object sender, RoutedEventArgs e)
        {
            ExtWindow ext = new ExtWindow(dbContext);
            ext.Show();
            this.Close();
        }
    }
}