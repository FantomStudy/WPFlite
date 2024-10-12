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
using WPFlite.Data;

namespace WPFlite
{
    /// <summary>
    /// Логика взаимодействия для ExtWindow.xaml
    /// </summary>
    public partial class ExtWindow : Window
    {
        MyDbContext dbContext;
        public ExtWindow(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
            InitializeComponent();
            GetAllData();
        }
        private void GetAllData()
        {
            DGProduct.ItemsSource = dbContext.Products.Join(dbContext.Shops, p => p.ShopId, s => s.ShopId, (p, s) => new {p.ProductId, p.Name, p.Description, p.Price, p.Unit, p.ShopId, s.ShopName, s.Purpose}).ToList();
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(dbContext);
            mainWindow.Show();
            this.Close();
        }
    }
}
