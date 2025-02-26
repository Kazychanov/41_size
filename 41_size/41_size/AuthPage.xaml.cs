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

  public partial class AuthPage : Page
  {
    public AuthPage()
    {
      InitializeComponent();
    }






    private void GuestBtn_Click(object sender, RoutedEventArgs e)
    {
      Manager.MainFrame.Navigate(new ProductPage());
    }




    private bool IsCaptchaShown = false; // Флаг для проверки, показывается ли капча
    private string CaptchaAnswer = "";   // Сохранённый ответ на капчу

    private async void RoleBtn_Click(object sender, RoutedEventArgs e)
    {
      string login = LoginTBox.Text;
      string password = PasswordTBox.Text;

      if (login == "" || password == "")
      {
        MessageBox.Show("Есть пустые поля!");
        return;
      }

      if (IsCaptchaShown)
      {
        if (CaptchaTBox.Text != CaptchaAnswer)
        {
          MessageBox.Show("Капча введена неверно!");
          CaptchaTBox.Text = "";
          RoleBtn.IsEnabled = false;
          await Task.Delay(10000);
          RoleBtn.IsEnabled = true;
          return;
        }

        IsCaptchaShown = false;
        CaptchaPanel.Visibility = Visibility.Collapsed;
      }


      User user = Kazychanov_41Entities.GetContext().User
          .ToList()
          .Find(x => x.UserLogin == login && x.UserPassword == password);

      if (user != null)
      {
        Manager.MainFrame.Navigate(new ProductPage(user));
        LoginTBox.Text = "";
        PasswordTBox.Text = "";
        CaptchaTBox.Text = "";
      }
      else
      {
        MessageBox.Show("Введены неправильные данные");

        if (!IsCaptchaShown)
        {
          ShowCaptcha();
          IsCaptchaShown = true;
          CaptchaPanel.Visibility = Visibility.Visible;
        }

        RoleBtn.IsEnabled = false;
        await Task.Delay(10000); 
        RoleBtn.IsEnabled = true;
      }
    }

    private void ShowCaptcha()
    {
      Random random = new Random();
      int firstNumber = random.Next(1, 10);
      int secondNumber = random.Next(1, 10);
      int thirdNumber = random.Next(1, 10);
      int firthNumber = random.Next(1, 10);

      captchaFirstWord.Text = firstNumber.ToString();
      captchaSecondWord.Text = secondNumber.ToString();
      captchaThirdWord.Text = thirdNumber.ToString();
      captchaFirthWord.Text = firthNumber.ToString();

      CaptchaAnswer = firstNumber.ToString() + secondNumber.ToString() + thirdNumber.ToString() + firthNumber.ToString();
    }
  }
}
