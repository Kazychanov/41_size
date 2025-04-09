using Microsoft.SqlServer.Server;
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
    List<string> PuckUpName = new List<string>();
    private Order currentOrder = new Order();
    private OrderProduct currentOrderProduct = new OrderProduct();

    public OrderWindow(List<OrderProduct> selectedOrderProducts, List<Product> selectedProducts, string NSPTblock, int nextOrderNumber)
    {
      InitializeComponent();

      PickUpCBox.SelectedIndex = 0;
      var currentPickUp = Kazychanov_41Entities.GetContext().PuckUpPoint.ToList();

      foreach (PuckUpPoint item in currentPickUp)
      {
        PuckUpName.Add(item.PupIndex + " " + item.PupCity + " " + item.PupStreet + " " + item.PupHome);
      }
      PickUpCBox.ItemsSource = PuckUpName;

      OrderClientTblock.Text = NSPTblock;

      // Установка номера следующего заказа
      OrderNumberTBlock.Text = "Номер заказа: " + nextOrderNumber.ToString();

      // Установка списка заказанных товаров в ListView
      OrderListView.ItemsSource = selectedProducts;


      OrderDeliveryDate.Text = DateTime.Now.ToString();
      SetDeliveryDate();
    }

    private void PickUpCBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      // Логика обработки изменения выбора пункта выдачи
    }

    private void OrderSaveBtn_Click(object sender, RoutedEventArgs e)
    {
      
    }


    public void SetDeliveryDate()
    {
      bool allInStock = SelectedProducts.All(p => p.ProductQuantityInStock >= 3);
      OrderDeliveryDate.SelectedDate = DateTime.Now.AddDays(allInStock ? 3 : 6);
    }
  }
}
