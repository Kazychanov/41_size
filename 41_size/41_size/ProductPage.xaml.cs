using System;
using System.Collections.Generic;
using System.Linq;
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
    List<Product> selectedProduct = new List<Product>();

    public ProductPage()
    {
      InitializeComponent();

      NSPTBlock.Text = "Вы вошли как гость";

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
          RoleTBlock.Text = " Роль: Менеджер"; break;
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

      // Поиск по имени
      currentProducts = currentProducts.Where(x => x.ProductName.ToLower().Contains(SearchTB.Text.ToLower())).ToList();

      // Фильтрация по скидке
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

      // Сортировка по цене
      if (UpRB.IsChecked.Value)
      {
        currentProducts = currentProducts.OrderBy(x => x.ProductCost).ToList();
      }
      if (DownRB.IsChecked.Value)
      {
        currentProducts = currentProducts.OrderByDescending(x => x.ProductCost).ToList();
      }

      ProductListView.ItemsSource = currentProducts;

      // Вывод количества продуктов в ListView из общего количества
      NameCount.Text = "Количество: " + currentProducts.Count.ToString() + " из " + Kazychanov_41Entities.GetContext().Product.ToList().Count.ToString();
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
      Product selectProduct = ProductListView.SelectedItem as Product;
      if (selectProduct != null)
      {
        var existingProduct = selectedProduct.FirstOrDefault(p => p.ProductArticleNumber == selectProduct.ProductArticleNumber);
        if (existingProduct != null)
        {
          existingProduct.Quantity++;
        }
        else
        {
          selectProduct.Quantity = 1;
          selectedProduct.Add(selectProduct);
        }
        BasketBtn.Visibility = Visibility.Visible;
        MessageBox.Show("Товар добавлен к заказу");
      }
      UpdateProduct();
    }

    private void BasketBtn_Click(object sender, RoutedEventArgs e)
    {
      if (selectedProduct.Any())
      {
        var orderProducts = selectedProduct.Select(p => new OrderProduct
        {
          ProductArticleNumber = p.ProductArticleNumber,
          ProductCount = p.Quantity
        }).ToList();

        // Генерация номера следующего заказа
        int nextOrderNumber = Kazychanov_41Entities.GetContext().Order.Any() ?
            Kazychanov_41Entities.GetContext().Order.Max(o => o.OrderID) + 1 : 1;

        Window orderWindow = new OrderWindow(orderProducts, selectedProduct, NSPTBlock.Text, nextOrderNumber);
        orderWindow.ShowDialog();

        // Очистка корзины после оформления заказа
        selectedProduct.Clear();
        BasketBtn.Visibility = Visibility.Collapsed;
      }
      else
      {
        MessageBox.Show("Заказ пуст");
      }
    }
  }
}