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

      NSPTBlock.Text = "Вы вошли как Альберт";

      var currentProducts = Kazychanov_41Entities.GetContext().Product.ToList();
      ProductListView.ItemsSource = currentProducts;

      DiscountCB.SelectedIndex = 0;
      UpdateProduct();
    }

    public ProductPage(User user)
    {
      InitializeComponent();

      NSPTBlock.Text = "Вы авторизованы как " + user.UserSurname + " " + user.UserName + " " + user.UserPatronymic;
      switch (user.UserRole)
      {
        case 1:
          RoleTBlock.Text = " Роль: Клиент"; break;
        case 2:
          RoleTBlock.Text = " Роль: Мененджер"; break;
        case 3:
          RoleTBlock.Text = " Роль: Администратор"; break;
      }

      var currentProducts = Kazychanov_41Entities.GetContext().Product.ToList();
      ProductListView.ItemsSource = currentProducts;

      DiscountCB.SelectedIndex = 0;
      UpdateProduct();
    }

    private void UpdateProduct()
    {
      var currentProducts = Kazychanov_41Entities.GetContext().Product.ToList();

      //поиск по имени
      currentProducts = currentProducts.Where(x => x.ProductName.ToLower().Contains(SearchTB.Text.ToLower())).ToList();
      ProductListView.ItemsSource = currentProducts.ToList();



      switch (DiscountCB.SelectedIndex)
      {
        case 1:
          currentProducts = currentProducts.Where(x => Convert.ToInt32(x.ProductDiscountAmount) >= 0 && Convert.ToInt32(x.ProductDiscountAmount) < 10).ToList();
          break;
        case 2:
          currentProducts = currentProducts.Where(x => Convert.ToInt32(x.ProductDiscountAmount) >= 10 && Convert.ToInt32(x.ProductDiscountAmount) < 15).ToList();
          break;
        case 3:
          currentProducts = currentProducts.Where(x => Convert.ToInt32(x.ProductDiscountAmount) >= 15 && Convert.ToInt32(x.ProductDiscountAmount) < 100).ToList();
          break;
        default:
          break;
      }
      ProductListView.ItemsSource = currentProducts;

      //возрастание, убывание
      if (UpRB.IsChecked.Value)
      {
        ProductListView.ItemsSource = currentProducts.OrderBy(x => x.ProductCost).ToList();
      }
      if (DownRB.IsChecked.Value)
      {
        ProductListView.ItemsSource = currentProducts.OrderByDescending(x => x.ProductCost).ToList();
      }

      //вывод количества продуктов в листвью из общего количества
      NameCount.Text = "кол-во " + currentProducts.Count.ToString() + " из " + Kazychanov_41Entities.GetContext().Product.ToList().Count.ToString();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
      {
        Manager.MainFrame.Navigate(new AddEditPage());
      }

      private void DownRB_Checked(object sender, RoutedEventArgs e)
      {
      UpdateProduct();
    }

      private void UpRB_Checked(object sender, RoutedEventArgs e)
      {
      UpdateProduct();
    }

      private void DiscountCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
      {
      UpdateProduct();
    }

      private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
      {
      UpdateProduct();
    }

    private void MenuItem_Click(object sender, RoutedEventArgs e)
    {
      OrderWindow orderWindow = new OrderWindow(null, null, null);
      orderWindow.ShowDialog();
        }
    }
  }
