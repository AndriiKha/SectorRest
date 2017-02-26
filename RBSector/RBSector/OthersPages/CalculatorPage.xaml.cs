using RBSectorUWPBusinessLogic.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace RBSector.OthersPages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CalculatorPage : Page
    {
        private CalculatorService cal_srv;
        private OrderService ord_srv;
        private UserService user_srv;
        public CalculatorPage()
        {
            this.InitializeComponent();
            cal_srv = CalculatorService.Instance();
            ord_srv = OrderService.Instance();
            user_srv = UserService.Instance();
            if (cal_srv.isLogin)
                this.txb_Space.Visibility = Visibility.Collapsed;
            else this.txb_Space_password.Visibility = Visibility.Collapsed;

        }

        private void txb_Space_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            cal_srv.Text = this.txb_Space.Text;
            if (!cal_srv.isLogin)
                ord_srv.Initi_ChangingNumberCalculator(this.txb_Space.Text);
        }
        private void btn_Number_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                Button button = sender as Button;
                if (cal_srv.isLogin)
                {
                    this.txb_Space_password.Password += button.Content;
                    user_srv.WrittenPass = this.txb_Space_password.Password;
                }
                else
                {
                    this.txb_Space.Text += button.Content;
                }
            }
        }
        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (cal_srv.isLogin)
            {
                if (this.txb_Space_password.Password.Length > 0)
                {
                    int length = this.txb_Space_password.Password.Length;
                    this.txb_Space_password.Password = this.txb_Space_password.Password.Substring(0, length - 1);
                }
            }
            else {
                if (this.txb_Space.Text.Length > 0)
                {
                    int length = this.txb_Space.Text.Length;
                    this.txb_Space.Text = this.txb_Space.Text.Substring(0, length - 1);
                }
            }
        }
        private void btn_Koma_Click(object sender, RoutedEventArgs e)
        {
            if (!this.txb_Space.Text.Contains(",") && this.txb_Space.Text.Length > 0)
            {
                this.txb_Space.Text += ",";
            }
        }
    }
}
