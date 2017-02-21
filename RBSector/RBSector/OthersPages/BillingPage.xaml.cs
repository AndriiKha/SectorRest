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
    public sealed partial class BillingPage : ContentDialog
    {
        private OrderService ord_srv;
        public BillingPage()
        {
            this.InitializeComponent();
            ord_srv = OrderService.Instance();
            this.BillFrame.Navigate(typeof(PayPage));
            ord_srv.ChangingFrameBilling += ChangingFrameBilling_Event;
        }

        private void btn_Done_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
        private async void ChangingFrameBilling_Event(object product, EventArgs e)
        {
            this.BillFrame.Navigate(typeof(BillDocPage));
        }
    }
}
