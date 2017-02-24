using RBSectorUWPBusinessLogic.Service;
using RBSectorUWPBusinessLogic.ViewModel;
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
    public sealed partial class BillDocPage : Page
    {
        private OrderService ord_srv;
        private СalculationService calc_srv;
        private OrderViewModel ordersViewModel;
        public BillDocPage()
        {
            this.InitializeComponent();
            if (ordersViewModel == null)
            {
                ord_srv = OrderService.Instance();
                calc_srv = new СalculationService();
                string result = calc_srv.CreatePdf(ord_srv.Products_ORD).ToString();
                this.BillDocView.NavigateToString(result);
            }
            else
            {
                string result = calc_srv.CreatePdf(ordersViewModel).ToString();
                this.BillDocView.NavigateToString(result);
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is OrderViewModel)
            {
                ordersViewModel = e.Parameter as OrderViewModel;
                string result = calc_srv.CreatePdf(ordersViewModel).ToString();
                this.BillDocView.NavigateToString(result);
            }
        }
    }
}
