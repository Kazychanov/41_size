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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _41_size
{
  public partial class ProductPage : Page
  {
    public ProductPage()
    {
      InitializeComponent();
      var currentProducts = Kazychanov_41Entities.GetContext().Product.ToList();
      ProductListView.ItemsSource = currentProducts;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      Manager.MainFrame.Navigate(new AddEditPage());
    }
  }
}
