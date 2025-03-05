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

namespace _41_size
{
  /// <summary>
  /// Логика взаимодействия для OrderWindow.xaml
  /// </summary>
  public partial class OrderWindow : Window
  {
    List<OrderProduct> SelectedOrderProducts = new List<OrderProduct>();
    List<Product> SelectedProducts = new List<Product>();
    private Order currentOrder = new Order();
    private OrderProduct currentOrderProduct = new OrderProduct();
    public OrderWindow(List<OrderProduct> SelectedOrderProducts, List<Product> SelectedProducts, string NSPTblock)
    {
      InitializeComponent();
      PickUpCBox.SelectedIndex = 0;
      var currentPickUp = Kazychanov_41Entities.GetContext().PuckUpPoint.ToList();
      PickUpCBox.ItemsSource = currentPickUp;

      OrderClientTblock.Text = NSPTblock;
      OrderNumberTBlock.Text = SelectedOrderProducts.First().OrderID.ToString();

      OrderListView.ItemsSource = SelectedProducts;
      
    }

    private void PickUpCBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }
    private void OrderSaveBtn_Click(object sender, RoutedEventArgs e)
    {

    }

    private void OrderDeleteBtn_Click(object sender, RoutedEventArgs e)
    {

    }
  }
}
