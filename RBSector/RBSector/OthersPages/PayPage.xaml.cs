using RBSectorUWPBusinessLogic.JSonTools;
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
    public sealed partial class PayPage : Page
    {
        private OrderService ord_srv;
        private CalculatorService calc_srv;
        public PayPage()
        {
            this.InitializeComponent();
            this.CalculatorFrame.Navigate(typeof(CalculatorPage));
            ord_srv = OrderService.Instance();
            calc_srv = CalculatorService.Instance();
            SetTotalMoney(ord_srv.Products_ORD.Ord_PriceCost.ToString());
            ord_srv.ChangingNumberCalculator += ChangingNumberCalculator_Event;
        }
        private async void ChangingNumberCalculator_Event(object obj, EventArgs e)
        {
            if (obj != null)
            {
                string item = obj.ToString();
                decimal diffMoney;
                if(decimal.TryParse(item, out diffMoney))
                {
                    decimal totalMoney = ord_srv.Products_ORD.Ord_PriceCost;
                    decimal gotMoney = JsonT.ConvertStringToDecimal(item);
                    string restMoney = (gotMoney - totalMoney).ToString();
                    txb_Rest.Text = restMoney;
                }
            }
        }
        private void btn_Pay_Click(object sender, RoutedEventArgs e)
        {
            //Hide();
            ord_srv.Initi_ChangingFrameBilling();
        }

        /*private void btn_Done_Click(object sender, RoutedEventArgs e)
        {
            //Hide();
        }*/
        private void SetTotalMoney(string TotalMoney)
        {
            if (string.IsNullOrEmpty(TotalMoney)) this.txb_total.Text = "0";
            else this.txb_total.Text = TotalMoney;
        }
    }
}
